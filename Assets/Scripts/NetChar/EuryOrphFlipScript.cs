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
                _orph.SetActive(true);
            }
            else
            {
                _eury.SetActive(true);
            }
        }
        else
        {
            if (IsLocalPlayer)
            {
                _eury.SetActive(true);
            }
            else
            {
                _orph.SetActive(true);
            }
        }
    }
}
