using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedCharacterScript : MonoBehaviour
{
    public GameObject _euryChar;
    public GameObject _eurySpawn;
    public GameObject _orphChar;
    public GameObject _orphSpawn;

    bool p1Spawn;
    bool p2Spawn;

    // Start is called before the first frame update
    void Start()
    {
        p1Spawn = false;
        p2Spawn = false;

        if(PlayerPrefs.GetInt("playerCount") != 3)
        {
            _euryChar.SetActive(true);
            _orphChar.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!p2Spawn && PlayerPrefs.GetInt("playerCount") == 3)
        {
            print("running");
            GameObject[] g = GameObject.FindGameObjectsWithTag("NetCharacter");

            if (g.Length == 1)
            {
                if (!p1Spawn)
                {
                    g[0].transform.position = _orphSpawn.transform.position;
                    g[0].transform.GetChild(0).localPosition = Vector2.zero;
                    g[0].transform.GetChild(1).localPosition = Vector2.zero;
                    p1Spawn = true;
                }
            }
            else if (g.Length == 2)
            {
                if(!p2Spawn)
                {
                    g[1].transform.position = _eurySpawn.transform.position;
                    g[1].transform.GetChild(0).localPosition = Vector2.zero;
                    g[1].transform.GetChild(1).localPosition = Vector2.zero;
                    p2Spawn = true;
                }
                if(!p1Spawn)
                {
                    g[0].transform.position = _orphSpawn.transform.position;
                    g[0].transform.GetChild(0).localPosition = Vector2.zero;
                    g[0].transform.GetChild(1).localPosition = Vector2.zero;
                    p1Spawn = true;
                }
            }
            
        }
    }
}
