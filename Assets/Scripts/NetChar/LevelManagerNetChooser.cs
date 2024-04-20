using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerNetChooser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("playerCount") ==3)
        {
            GetComponent<ManagerScript>().enabled = false;
        }
        else
        {
            GetComponent<NetworkLevelManagerScript>().enabled = false;
        }
    }
}
