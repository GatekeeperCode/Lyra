using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrpheusScript : MonoBehaviour
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
    //public Animator _OrphView;
    //public Animator _EuryView;

    Rigidbody2D _rbody;
    ManagerScript _manager;
    AudioSource _asource;

    public bool _lyreRaise = false;
    bool _startedJump = false;
    bool _stoppedJump = false;
    bool _facingRight = true;
    bool _climbing = true;
    float _lastTimegrounded = 0;

    public AudioClip _clip;
    public float _volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _asource = GetComponent<AudioSource>();
        _manager = FindObjectOfType<ManagerScript>();
    }

    // Update is called once per frame
    void Update() //display in update, physics in fixed update
    {
        //Paused Screen
        if (_manager.pausedGame) {
            //_OrphView.speed = 0;
            //_EuryView.speed = 0; 
            return; 
        }

        //Reset animator
        //_OrphView.speed = 1;
        //_EuryView.speed = 1;

        //Check for jumping
        if (IsGrounded())
        {
            _lastTimegrounded = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.RightShift) && WasGrounded())
        {
            _startedJump = true;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            _stoppedJump = true;
        }

        //Check for lyre
        if (Input.GetKey(KeyCode.RightControl) && IsOnFloor())
        {
            if(!_asource.isPlaying)
            {
                _asource.PlayOneShot(_clip, _volume);
            }
            _lyreRaise = true;

        }
        if (Input.GetKeyUp(KeyCode.RightControl))
        {
            _lyreRaise = false;
            _asource.Stop();
        }

        //Check for climbing
        if (climbAttempt()) { 
            _climbing = true;
        }
        else {
            _climbing = false; 
        }

        //Flip character
        float xdir = Input.GetAxis("Horizontal");
        if (xdir < 0 && _facingRight)
        {
            Flip();
        }
        else if (xdir > 0 && !_facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        //Paused Screen
        if (_manager.pausedGame) { _rbody.constraints = RigidbodyConstraints2D.FreezePosition; return; }

        //Reset constraints
        _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Move Character
        float xdir = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(xdir * playerSpeed, _rbody.velocity.y);

        if (_climbing)
        {
            Climb();
        } else
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

        float ydir = Input.GetAxis("Vertical");
        _rbody.velocity = new Vector2(_rbody.velocity.x, climbingSpeed * ydir);
    }

    private bool IsOnFloor()
    {
        Vector2 playerVector = transform.position;
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(playerVector.x - 0.2f, playerVector.y), Vector2.down, 1f, floorLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(playerVector.x + 0.2f, playerVector.y), Vector2.down, 1f, floorLayer);
        return (hitLeft.collider != null || hitRight.collider != null);
    }
}
