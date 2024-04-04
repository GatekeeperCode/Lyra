using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedCharacterScript : MonoBehaviour
{
    public GameObject _euryChar;
    public GameObject _eurySpawn;
    public GameObject _orphChar;
    public GameObject _orphSpawn;
    

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("playerCount") != 3)
        {
            _euryChar.SetActive(true);
            _orphChar.SetActive(true);
        }
        else
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("NetCharacter");

            g[0].GetComponent<Transform>().position = _orphSpawn.transform.position;
            g[1].GetComponent<Transform>().position = _eurySpawn.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
