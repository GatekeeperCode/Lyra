using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameObject[] walls;
    public float[] wallTop;
    public float[] wallBottom;
    public bool[] setFalse;

    bool thisPad = false;
    OrpheusScript orpheus;
    ManagerScript manager;

    // Start is called before the first frame update
    void Start()
    {
        orpheus = FindObjectOfType<OrpheusScript>();
        manager = FindObjectOfType<ManagerScript>();
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
        if (manager.pausedGame)
        {
            return;
        }

        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].transform.position.y == wallTop[i] && setFalse[i])
            {
                walls[i].SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (manager.pausedGame)
        {
            return;
        }

        if (orpheus._lyreRaise && thisPad)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                lower(walls[i], wallBottom[i]);
            }
        }
        else if(!orpheus._lyreRaise)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Orpheus"))
        {
            thisPad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Orpheus"))
        {
            thisPad = false;
        }
    }
}
