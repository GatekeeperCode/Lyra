using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public GameObject _singlePlayerCam1;
    public GameObject _singlePlayerCam2;

    // Start is called before the first frame update
    void Start()
    {
        _singlePlayerCam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _singlePlayerCam2.SetActive(!_singlePlayerCam2.activeSelf);
            _singlePlayerCam1.SetActive(!_singlePlayerCam1.activeSelf);
        }
    }
}
