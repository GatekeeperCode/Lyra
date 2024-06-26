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
    bool haveScript = false;
    OrpheusScript orpheus;
    OrphNetworkScript netOrpheus;
    ManagerScript manager;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("playerCount") == 3)
        {
            Invoke("FindNetworkedScript", 5);
        }
        else
        {
            Invoke("FindScript", 5);
        }
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
        if (manager.pausedGame || !haveScript)
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
        if (manager.pausedGame || !haveScript)
        {
            return;
        }

        if(PlayerPrefs.GetInt("playerCount") == 3)
        {
            if (netOrpheus._lyreRaise && thisPad)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    lower(walls[i], wallBottom[i]);
                }
            }
            else if (!netOrpheus._lyreRaise)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    raise(walls[i], wallTop[i]);
                }
            }
        } else
        {
            if (orpheus._lyreRaise && thisPad)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    lower(walls[i], wallBottom[i]);
                }
            }
            else if (!orpheus._lyreRaise)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    raise(walls[i], wallTop[i]);
                }
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

    private void FindScript()
    {
        orpheus = FindObjectOfType<OrpheusScript>();
        haveScript = true;
    }

    private void FindNetworkedScript()
    {
        netOrpheus = FindObjectOfType<OrphNetworkScript>();
        haveScript = true;
    }
}
