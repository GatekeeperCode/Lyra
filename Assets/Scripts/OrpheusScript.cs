using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrpheusScript : MonoBehaviour
{
    public float playerSpeed;
    
    Rigidbody2D _rbody;
    private bool local = true; //later get from manager
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        localMoveCharacter("Orpheus");
        localMoveCharacter("Eurydice");
    }

    public void localMoveCharacter(string name)
    {
        if (name.Equals("Orpheus"))
        {
            float xdir = Input.GetAxis("Horizontal");
            _rbody.velocity = new Vector2(xdir * playerSpeed, _rbody.velocity.y);
        }
        else
        {
            float xdir = Input.GetAxis("EuroHorizontal");
            _rbody.velocity = new Vector2(xdir * playerSpeed, _rbody.velocity.y);
        }

        print("velocity: " + _rbody.velocity);
        
    }
}
