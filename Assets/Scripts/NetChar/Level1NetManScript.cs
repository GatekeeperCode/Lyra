using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;


public class Level1NetManScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("playerCount") == 3 && PlayerPrefs.GetInt("host") == 1)
        {
            if(PlayerPrefs.GetString("ipAddress")!="")
            {
                UnityTransport transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
                transport.SetConnectionData(PlayerPrefs.GetString("ipAddress"), 7777);
            }

            NetworkManager.Singleton.StartHost();
            print("Hosting");
        }

        if ((PlayerPrefs.GetInt("playerCount") == 3 && PlayerPrefs.GetInt("host") == 0))
        {
            if (PlayerPrefs.GetString("ipAddress") != "")
            {
                UnityTransport transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
                transport.SetConnectionData(PlayerPrefs.GetString("ipAddress"), 7777);
            }

            NetworkManager.Singleton.StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
