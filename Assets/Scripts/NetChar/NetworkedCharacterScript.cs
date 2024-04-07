using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedCharacterScript : MonoBehaviour
{
    public GameObject _euryChar;
    public GameObject _eurySpawn;
    public GameObject _orphChar;
    public GameObject _orphSpawn;

    GameObject[] g;
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
        if(!p2Spawn)
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("NetCharacter");

            if(g.Length>0)
            {
                if(!p1Spawn)
                {
                    g[0].transform.position = _orphSpawn.transform.position;
                    p1Spawn = true;
                }
                else
                {
                    if(g[0].activeSelf)
                    {
                        if(g.Length>1)
                        {
                            g[1].transform.position = _eurySpawn.transform.position;
                            p2Spawn = true;
                        }
                    }
                    else
                    {
                        g[0].transform.position = _eurySpawn.transform.position;
                        p2Spawn = true;
                    }
                }
            }
        }
    }
}
