using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EuryOrphFlipScript : NetworkBehaviour
{
    public GameObject _orph;
    public GameObject _eury;

    // Start is called before the first frame update
    void Start()
    {
        if(IsHost)
        {
            if(IsLocalPlayer)
            {
                _eury.SetActive(false);
            }
            else
            {
                _orph.SetActive(false);
            }
        }
        else
        {
            if (IsLocalPlayer)
            {
                _orph.SetActive(false);
            }
            else
            {
                _eury.SetActive(false);
            }
        }
    }
}
