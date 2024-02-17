using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public GameObject wall5;
    public float w1Top;
    public float w1Bottom;
    public float w2Top;
    public float w2Bottom;
    public float w3Top;
    public float w3Bottom;
    public float w4Top;
    public float w4Bottom;
    public float w5Top;
    public float w5Bottom;


    OrpheusScript orpheus;
    bool wall1Exists = false;
    bool wall2Exists = false;
    bool wall3Exists = false;
    bool wall4Exists = false;
    bool wall5Exists = false;

    // Start is called before the first frame update
    void Start()
    {
        orpheus = FindObjectOfType<OrpheusScript>();
        if(wall1 != null) { wall1Exists = true; startingPosition(wall1, w1Top); }
        if(wall2 != null) { wall2Exists = true; startingPosition(wall2, w2Top); }
        if(wall3 != null) { wall3Exists = true; startingPosition(wall3, w3Top); }
        if(wall4 != null) { wall4Exists = true; startingPosition(wall4, w4Top); }
        if(wall5 != null) { wall5Exists = true; startingPosition(wall5, w5Top); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (orpheus._lyreRaise)
        {
            if (wall1Exists)
            {
                lower(wall1, w1Bottom);
            }
            if(wall2Exists)
            {
                lower(wall2, w2Bottom);
            }
            if (wall3Exists)
            {
                lower(wall3, w3Bottom);
            }
            if (wall4Exists)
            {
                lower(wall4, w4Bottom);
            }
            if (wall5Exists)
            {
                lower(wall5, w5Bottom);
            }
        }
        else
        {
            if (wall1Exists)
            {
                raise(wall1, w1Top);
            }
            if (wall2Exists)
            {
                raise(wall2, w2Top);
            }
            if (wall3Exists)
            {
                raise(wall3, w3Top);
            }
            if (wall4Exists)
            {
                raise(wall4, w4Top);
            }
            if (wall5Exists)
            {
                raise(wall5, w5Top);
            }
        }
    }

    private void lower(GameObject wall, float bottom)
    {
        if (wall.transform.position.y > bottom)
        {
            wall.transform.position = new Vector2(wall.transform.position.x, wall.transform.position.y - .1f);
        }
    }

    private void raise(GameObject wall, float top)
    {
        if (wall.transform.position.y < top)
        {
            wall.transform.position = new Vector2(wall.transform.position.x, wall.transform.position.y + .1f);
        }
    }

    private void startingPosition(GameObject wall, float top)
    {
        wall.transform.position = new Vector2(wall.transform.position.x, top);
    }
}
