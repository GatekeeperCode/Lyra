using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameObject[] walls;
    public float[] wallTop;
    public float[] wallBottom;
    public bool[] setFalse;
    bool onFloor = false;

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

    // Start is called before the first frame update
    void Start()
    {
        orpheus = FindObjectOfType<OrpheusScript>();
        for(int i = 0; i < walls.Length; i++)
        {
            GameObject wall = walls[i];
            walls[i].transform.position = new Vector2(wall.transform.position.x, wallTop[i]);
            if (setFalse[i])
            {
                walls[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!orpheus.IsOnFloor())
        {
            return;
        }

        for(int i = 0; i < walls.Length; i++)
        {
            if (walls[i].transform.position.y == wallTop[i] && setFalse[i])
            {
                walls[i].SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!orpheus.IsOnFloor())
        {
            return;
        }
        else if (orpheus._lyreRaise)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                lower(walls[i], wallBottom[i]);
            }
        }
        else
        {
            for (int i = 0; i < walls.Length; i++)
            {
                raise(walls[i], wallTop[i]);
            }
        }
    }

    private void lower(GameObject wall, float bottom)
    {
        wall.SetActive(true);
        wall.GetComponent<Collider2D>().enabled = true;
        if (wall.transform.position.y > bottom)
        {
            wall.transform.position = new Vector2(wall.transform.position.x, wall.transform.position.y - .1f);
        }
    }

    private void raise(GameObject wall, float top)
    {
        wall.GetComponent<Collider2D>().enabled = false;
        if (wall.transform.position.y < top)
        {
            wall.transform.position = new Vector2(wall.transform.position.x, wall.transform.position.y + .1f);
        }
    }
}
