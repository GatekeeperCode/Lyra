using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EuryOrphFlipScript : NetworkBehaviour
{
    public GameObject _orph;
    public GameObject _eury;

    GameObject _orphCam;
    GameObject _euryCam;

    CameraManagerScript cms;
    bool hasDipped;

    private void Start()
    {
        hasDipped = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff;
    }

    // Start is called before the first frame update
    void Update()
    {
        hasDipped = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff;

        if (!hasDipped)
        {
            print("Hello");

            _orphCam = GameObject.FindGameObjectWithTag("MainCamera");
            _euryCam = GameObject.FindGameObjectWithTag("EuryMainCam");
            cms = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManagerScript>();

            //if(IsOwnedByServer)
            //{
            //    print(1);
            //}
            //else
            //{
            //    print(2);
            //}

            if (IsHost)
            {
                if (IsLocalPlayer)
                {
                    print(1);
                    _eury.SetActive(false);
                    _euryCam.SetActive(false);
                    cms.orphView = true;
                }
                else
                {
                    print(2);
                    _orph.SetActive(false);
                    //_eury.GetComponent<EuryNetworkScript>().enabled = false;
                }
            }
            else
            {
                print("Should appear twice");
                if (IsLocalPlayer)
                {
                    print(3);
                    _orph.SetActive(false);
                    _orphCam.SetActive(false);
                    cms.orphView = false;
                }
                else
                {
                    print(4);
                    _eury.SetActive(false);
                    //_orph.GetComponent<OrphNetworkScript>().enabled = false;
                }
            }

            GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff = true;
        }
    }
}
