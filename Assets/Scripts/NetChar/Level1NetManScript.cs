using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Level1NetManScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("playerCount")==3 && PlayerPrefs.GetInt("host")==1)
        {
            NetworkManager.Singleton.StartHost();
            print("Hosting");
        }

        if ((PlayerPrefs.GetInt("playerCount") == 3 && PlayerPrefs.GetInt("host") == 0))
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
