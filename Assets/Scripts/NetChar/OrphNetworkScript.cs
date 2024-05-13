using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

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
    NetworkLevelManagerScript _manager;
    AudioSource _asource;

    public bool _lyreRaise = false;
    bool _startedJump = false;
    bool _stoppedJump = false;
    bool _facingRight = true;
    bool _climbing = true;
    bool _paused = false;
    float _lastTimegrounded = 0;
    Vector3 _pausedVelocity;

    public AudioClip _clip;
    public float _volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _asource = GetComponent<AudioSource>();
        _manager = FindObjectOfType<NetworkLevelManagerScript>();
        _pausedVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update() //display in update, physics in fixed update
    {
        if (SceneManager.GetActiveScene().buildIndex != 6)
        {
            GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManagerScript>().Orpheus = gameObject;
        }

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

        if (IsLocalPlayer && IsServer)
        {
            //Check for jumping
            if (IsGrounded())
            {
                _lastTimegrounded = Time.time;
            }
            if ((Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Joystick1Button5)) && WasGrounded())
            {
                _startedJump = true;
                setAnimationClientRpc("Jumping", true);
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
                setAnimationClientRpc("Lyre", true);

            }
            if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.Joystick1Button1) || !IsOnFloor())
            {
                _lyreRaise = false;
                setAnimationClientRpc("Lyre", false);
                _asource.Stop();
            }

            //Check for climbing
            if (climbAttempt())
            {
                // Set climbing animations
                if (Mathf.Abs(_rbody.velocity.y) >= 0.2)
                {
                    setAnimationClientRpc("Climbing", true);
                    setAnimationClientRpc("Climb", false);
                }
                else
                {
                    setAnimationClientRpc("Climbing", false);
                    setAnimationClientRpc("Climb", true);
                }
                _climbing = true;

            }
            else
            {
                _climbing = false;
                setAnimationClientRpc("Climbing", false);
                setAnimationClientRpc("Climb", false);
            }

            //Flip character
            float xdir = Input.GetAxis("Horizontal");
            if (xdir < 0 && _facingRight)
            {
                FlipClientRpc();
            }
            else if (xdir > 0 && !_facingRight)
            {
                FlipClientRpc();
            }

            // Set standing and walking animator values
            if (Mathf.Abs(_rbody.velocity.x) >= 0.2)
            {
                setAnimationClientRpc("Walking", true);
                setAnimationClientRpc("Standing", false);
            }
            else
            {
                setAnimationClientRpc("Walking", false);
                setAnimationClientRpc("Standing", true);
            }

            moveOrpheusLogicClientRpc(_startedJump, _stoppedJump, _climbing, _lyreRaise, _facingRight);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        setAnimationClientRpc("Jumping", false);
    }

    private void FixedUpdate()
    {
        //Paused Screen
        if (_manager.pausedGame) { _rbody.constraints = RigidbodyConstraints2D.FreezePosition; return; }

        //Reset constraints
        _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Move Orpheus on all machines
        if(IsLocalPlayer && IsServer)
        {
            //Move Orpheus
            positionFixClientRpc(transform.position);

            float xdir = Input.GetAxis("Horizontal");
            setOrpheusVelocityClientRpc(xdir * playerSpeed, _rbody.velocity.y);

            float ydir = Input.GetAxis("Vertical");
            ClimbClientRpc(ydir, _climbing);


            if (_startedJump)
            {
                _startedJump = false;
                setOrpheusVelocityClientRpc(_rbody.velocity.x, jumpForce);
                moveOrpheusLogicClientRpc(_startedJump, _stoppedJump, _climbing, _lyreRaise, _facingRight);
            }
            if (_stoppedJump)
            {
                _stoppedJump = false;
                if (_rbody.velocity.y > 0)
                {
                    setOrpheusVelocityClientRpc(_rbody.velocity.x, _rbody.velocity.y * 0.1f);
                    moveOrpheusLogicClientRpc(_startedJump, _stoppedJump, _climbing, _lyreRaise, _facingRight);
                }
            }
        }
    }

    [ClientRpc]
    private void positionFixClientRpc(Vector2 posn)
    {
        if (!IsLocalPlayer && Vector2.Distance(transform.position, posn) > 0.1f)
        {
            transform.position = posn;
        }
    }

    [ClientRpc]
    private void moveOrpheusLogicClientRpc(bool startedJump, bool stoppedJump, bool climbing, bool lyreRaise, bool facingRight)
    {
        if (!IsServer)
        {
            _startedJump = startedJump;
            _stoppedJump = stoppedJump;
            _climbing = climbing;
            _lyreRaise = lyreRaise;
            if(_facingRight != facingRight)
            {
                FlipClientRpc();
            }
        }
    }

    [ClientRpc]
    private void setAnimationClientRpc(string name, bool value)
    {
        if(IsServer)
        {
            _OrphView.SetBool(name, value);
        } else
        {
            _EuryView.SetBool(name, value);
        }
    }

    [ClientRpc]
    private void setOrpheusVelocityClientRpc(float xdir, float ydir)
    {
        _rbody.velocity = new Vector2(xdir, ydir);
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

    [ClientRpc]
    private void FlipClientRpc()
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

    [ClientRpc]
    private void ClimbClientRpc(float ydir, bool climbing)
    {
        if (climbing)
        {
            _rbody.gravityScale = 0;
            _rbody.velocity = new Vector2(_rbody.velocity.x, climbingSpeed * ydir);
        }
        else
        {
            _rbody.gravityScale = 1;
        }

    }

    public bool IsOnFloor()
    {
        Vector2 playerVector = transform.position;
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(playerVector.x - 0.2f, playerVector.y), Vector2.down, 1f, floorLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(playerVector.x + 0.2f, playerVector.y), Vector2.down, 1f, floorLayer);
        return (hitLeft.collider != null || hitRight.collider != null);
    }
}

