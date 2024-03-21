using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EurydiceScript : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public float playerSpeed;
    public float climbingSpeed;
    public float jumpForce;
    public float coyoteTime;
    public Animator _EuryView;
    public Animator _OrphView;

    Rigidbody2D _rbody;
    ManagerScript _manager;


    bool _startedJump = false;
    bool _stoppedJump = false;
    bool _facingRight = true;
    bool _climbing = true;
    bool _paused = false;
    int playerCount;
    float _lastTimegrounded = 0;
    Vector3 _pausedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _manager = FindObjectOfType<ManagerScript>();
        _pausedVelocity = Vector3.zero;
        if (PlayerPrefs.HasKey("PlayerCount"))
        {
            playerCount = PlayerPrefs.GetInt("playerCount");
        } else
        {
            playerCount = 1;
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
                _EuryView.speed = 0;
                _OrphView.speed = 0;
            }
            else
            {
                // Reset after paused screen
                _rbody.velocity = _pausedVelocity;
                _EuryView.speed = 1;
                _OrphView.speed = 1;
            }

        }
        if (_paused) return; 


        //Check for jumping
        if (IsGrounded())
        {
            _lastTimegrounded = Time.time;
            _rbody.gravityScale = 1;
        }
        if (playerCount == 2)
        {
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick2Button4)) && WasGrounded())
            {
                _startedJump = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Joystick2Button4))
            {
                _stoppedJump = true;
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button4)) && WasGrounded())
            {
                _startedJump = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Joystick1Button4))
            {
                _stoppedJump = true;
            }
        }

        //Check for climbinng
        if (climbAttempt()) { 
            _climbing = true;
        }
        else { 
            _climbing = false;
        }

        //Flip character
        float xdir = Input.GetAxis("EuroHorizontal");
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
        float xdir = Input.GetAxis("EuroHorizontal");
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
                _rbody.gravityScale = .5f;
            }
            _stoppedJump = false;
        }
    }
    private bool IsGrounded()
    {
        Vector2 playerVector = transform.position;
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(playerVector.x - 0.08f, playerVector.y), Vector2.down, 1f, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(playerVector.x + 0.08f, playerVector.y), Vector2.down, 1f, groundLayer);
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

        float ydir = Input.GetAxis("EuroVertical");
        _rbody.velocity = new Vector2(_rbody.velocity.x, climbingSpeed * ydir);

    }

}
