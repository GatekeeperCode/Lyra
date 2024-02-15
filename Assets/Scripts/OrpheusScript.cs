using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrpheusScript : MonoBehaviour
{
    public LayerMask groundLayer;
    public float playerSpeed;
    public float jumpForce;
    public float coyoteTime;

    Rigidbody2D _rbody;
    Animator _animator;

    bool _startedJump = false;
    bool _stoppedJump = false;
    bool _facingRight = true;
    float _lastTimegrounded = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() //display in update, physics in fixed update
    {
        if (IsGrounded())
        {
            _lastTimegrounded = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && WasGrounded())
        {
            _startedJump = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _stoppedJump = true;
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
        float xdir = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(xdir * playerSpeed, _rbody.velocity.y);

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
}
