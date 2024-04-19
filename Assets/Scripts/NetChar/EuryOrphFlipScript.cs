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

    // Start is called before the first frame update
    void Start()
    {
        _orphCam = GameObject.FindGameObjectWithTag("MainCamera");
        _euryCam = GameObject.FindGameObjectWithTag("EuryMainCam");
        cms = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManagerScript>();

        if(IsHost)
        {
            if(IsLocalPlayer)
            {
                _eury.SetActive(false);
                _euryCam.SetActive(false);
                cms.orphView = true;
            }
            else
            {
                _orph.SetActive(false);
                //_eury.GetComponent<EuryNetworkScript>().enabled = false;
            }
        }
        else
        {
            if (IsLocalPlayer)
            {
                _orph.SetActive(false);
                _orphCam.SetActive(false);
                cms.orphView = false;
            }
            else
            {
                _eury.SetActive(false);
                //_orph.GetComponent<OrphNetworkScript>().enabled = false;
            }
        }
    }
}
