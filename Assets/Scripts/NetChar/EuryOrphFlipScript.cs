using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class EuryOrphFlipScript : NetworkBehaviour
{
    public GameObject _orph;
    public GameObject _eury;

    GameObject _orphCam;
    GameObject _euryCam;

    CameraManagerScript cms;
    bool hasDipped;
    public bool alreadyRun;

    private void Start()
    {   
        if(SceneManager.GetActiveScene().buildIndex != 6)
        {
            hasDipped = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff;
            alreadyRun = false;
        }
        else
        {
            hasDipped = false;
            alreadyRun = true;
        }
    }

    // Start is called before the first frame update
    void Update()
    {
        if(!alreadyRun)
        {
            hasDipped = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff;

            if (IsHost)
            {
                if (IsLocalPlayer && !hasDipped)
                {
                    _orphCam = GameObject.FindGameObjectWithTag("MainCamera");
                    _euryCam = GameObject.FindGameObjectWithTag("EuryMainCam");
                    cms = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManagerScript>();

                    _eury.SetActive(false);
                    _euryCam.SetActive(false);
                    cms.orphView = true;
                    GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff = true;
                }
                else
                {
                    _orph.SetActive(false);
                    //_eury.GetComponent<EuryNetworkScript>().enabled = false;
                }
            }
            else
            {
                if (IsLocalPlayer && !hasDipped)
                {
                    _orphCam = GameObject.FindGameObjectWithTag("MainCamera");
                    _euryCam = GameObject.FindGameObjectWithTag("EuryMainCam");
                    cms = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManagerScript>();

                    _orph.SetActive(false);
                    _orphCam.SetActive(false);
                    cms.orphView = false;
                    GameObject.FindGameObjectWithTag("CamManager").GetComponent<CamDissapearVariable>()._camOff = true;
                }
                else
                {
                    _eury.SetActive(false);
                    //_orph.GetComponent<OrphNetworkScript>().enabled = false;
                }
            }

            alreadyRun = true;
        }
    }
}
