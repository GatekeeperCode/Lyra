using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedCharacterScript : MonoBehaviour
{
    public GameObject _euryChar;
    public GameObject _eurySpawn;
    public GameObject _orphChar;
    public GameObject _orphSpawn;
    public GameObject _netChar;
    

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

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
