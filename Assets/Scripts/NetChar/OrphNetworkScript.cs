using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody2D))]
public class OrphNetworkScript : NetworkBehaviour
{
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public LayerMask floorLayer;
    public float playerSpeed;
    public float climbingSpeed;
    public float jumpForce;
    public float coyoteTime;
    public float max;
    public float min;
    public Animator _OrphView;
    public Animator _EuryView;

    public Rigidbody2D _rbody;
    ManagerScript _manager;
    AudioSource _asource;

    public bool _lyreRaise = false;
    bool _startedJump = false;
    bool _stoppedJump = false;
    bool _facingRight = true;
    bool _climbing = true;
    bool _paused = false;
    int playerCount;
    float _lastTimegrounded = 0;
    Vector3 _pausedVelocity;

    public AudioClip _clip;
    public float _volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _asource = GetComponent<AudioSource>();
        _manager = FindObjectOfType<ManagerScript>();
        _pausedVelocity = Vector3.zero;
        GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManagerScript>().Orpheus = gameObject;

        if (PlayerPrefs.HasKey("playerCount"))
        {
            playerCount = PlayerPrefs.GetInt("playerCount");
        }
        else
        {
            playerCount = 1;
            print("playerCount: " + playerCount);
        }
    }

    // Update is called once per frame
    void Update() //display in update, physics in fixed update
    {

        //Paused Screen
        if (_manager.pausedGame != _paused)
        {
            _paused = _manager.pausedGame;

            if (_paused)
            {
                // During paused screen
                _pausedVelocity = _rbody.velocity;
                _OrphView.speed = 0;
                _EuryView.speed = 0;
            }
            else
            {
                // Reset after paused screen
                _rbody.velocity = _pausedVelocity;
                _OrphView.speed = 1;
                _EuryView.speed = 1;
            }

        }
        if (_paused) return;


        //Check for jumping
        if (IsGrounded())
        {
            _lastTimegrounded = Time.time;
        }
        if ((Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Joystick1Button5)) && WasGrounded())
        {
            _startedJump = true;
            _OrphView.SetBool("Jumping", true);
            _EuryView.SetBool("Jumping", true);
        }
        if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            _stoppedJump = true;
        }

        //Check for lyre
        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.Joystick1Button1)) && IsOnFloor())
        {
            if (!_asource.isPlaying)
            {
                _asource.PlayOneShot(_clip, _volume);
            }
            _lyreRaise = true;
            _OrphView.SetBool("Lyre", true);
            _EuryView.SetBool("Lyre", true);

        }
        if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.Joystick1Button1) || !IsOnFloor())
        {
            _lyreRaise = false;
            _OrphView.SetBool("Lyre", false);
            _EuryView.SetBool("Lyre", false);
            _asource.Stop();
        }

        //Check for climbing
        if (climbAttempt())
        {
            // Set climbing animations
            if (Mathf.Abs(_rbody.velocity.y) >= 0.2)
            {
                _OrphView.SetBool("Climbing", true);
                _EuryView.SetBool("Climbing", true);
                _OrphView.SetBool("Climb", false);
                _EuryView.SetBool("Climb", false);
            }
            else
            {
                _OrphView.SetBool("Climbing", false);
                _EuryView.SetBool("Climbing", false);
                _OrphView.SetBool("Climb", true);
                _EuryView.SetBool("Climb", true);
            }
            _climbing = true;

        }
        else
        {
            _climbing = false;
            _OrphView.SetBool("Climbing", false);
            _EuryView.SetBool("Climbing", false);
            _OrphView.SetBool("Climb", false);
            _EuryView.SetBool("Climb", false);
        }

        //Flip character
        float xdir = 0;
        if (playerCount == 1)
        {
            xdir = Input.GetAxis("Horizontal");
        }
        else
        {
            xdir = Input.GetAxis("HorizontalOnly");
        }
        if (xdir < 0 && _facingRight)
        {
            Flip();
        }
        else if (xdir > 0 && !_facingRight)
        {
            Flip();
        }

        // Set standing and walking animator values
        if (Mathf.Abs(_rbody.velocity.x) >= 0.2)
        {
            _OrphView.SetBool("Walking", true);
            _OrphView.SetBool("Standing", false);
            _EuryView.SetBool("Walking", true);
            _EuryView.SetBool("Standing", false);
        }
        else
        {
            _OrphView.SetBool("Walking", false);
            _OrphView.SetBool("Standing", true);
            _EuryView.SetBool("Walking", false);
            _EuryView.SetBool("Standing", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _OrphView.SetBool("Jumping", false);
        _EuryView.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        //Paused Screen
        if (_manager.pausedGame) { _rbody.constraints = RigidbodyConstraints2D.FreezePosition; return; }

        //Reset constraints
        _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Move Character
        float xdir = 0;
        if (playerCount == 1)
        {
            xdir = Input.GetAxis("Horizontal");
        }
        else
        {
            xdir = Input.GetAxis("HorizontalOnly");
        }
        _rbody.velocity = new Vector2(xdir * playerSpeed, _rbody.velocity.y);

        if (_climbing)
        {
            Climb();
        }
        else
        {
            _rbody.gravityScale = 1;
        }

        if (_startedJump)
        {
            _rbody.velocity = new Vector2(_rbody.velocity.x, jumpForce);
            _startedJump = false;
        }
        if (_stoppedJump)
        {
            if (_rbody.velocity.y > 0)
            {
                _rbody.velocity = new Vector2(_rbody.velocity.x, _rbody.velocity.y * 0.1f);
            }
            _stoppedJump = false;
        }
    }
    private bool IsGrounded()
    {
        Vector2 playerVector = transform.position;
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(playerVector.x - 0.2f, playerVector.y), Vector2.down, 1f, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(playerVector.x + 0.2f, playerVector.y), Vector2.down, 1f, groundLayer);
        return (hitLeft.collider != null || hitRight.collider != null);
    }

    private bool WasGrounded()
    {
        return ((Time.time - _lastTimegrounded) < coyoteTime);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool climbAttempt()
    {
        Vector2 playerVector = transform.position;

        RaycastHit2D hitAbove = Physics2D.Raycast(new Vector2(playerVector.x + 0.1f, playerVector.y + 0.1f), Vector2.down, .3f, ladderLayer);
        RaycastHit2D hitMiddle = Physics2D.Raycast(new Vector2(playerVector.x + 0.1f, playerVector.y), Vector2.left, .3f, ladderLayer);
        RaycastHit2D hitBelow = Physics2D.Raycast(new Vector2(playerVector.x + 0.1f, playerVector.y - 0.1f), Vector2.down, .3f, ladderLayer);

        return (hitAbove.collider != null || hitMiddle.collider != null || hitBelow.collider != null);
    }

    private void Climb()
    {
        _rbody.gravityScale = 0;

        float ydir = 0;
        if (playerCount == 1)
        {
            ydir = Input.GetAxis("Vertical");
        }
        else
        {
            ydir = Input.GetAxis("VerticalOnly");
        }
        _rbody.velocity = new Vector2(_rbody.velocity.x, climbingSpeed * ydir);
    }

    public bool IsOnFloor()
    {
        Vector2 playerVector = transform.position;
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(playerVector.x - 0.2f, playerVector.y), Vector2.down, 1f, floorLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(playerVector.x + 0.2f, playerVector.y), Vector2.down, 1f, floorLayer);
        return (hitLeft.collider != null || hitRight.collider != null);
    }
}

