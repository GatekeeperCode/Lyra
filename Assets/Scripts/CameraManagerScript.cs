using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public GameObject _singlePlayerCam1;
    public GameObject _singlePlayerCam2;
    bool isSinglePlayer;
    public GameObject _splitCam1;
    public GameObject _splitCam2;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("playerCount")==2)
        {
            _singlePlayerCam2.SetActive(false);
            _singlePlayerCam1.SetActive(false);
            isSinglePlayer = false;
            _splitCam1.SetActive(true);
            _splitCam1.SetActive(true);
        }
        else
        {
            _singlePlayerCam2.SetActive(false);
            isSinglePlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isSinglePlayer)
        {
            _singlePlayerCam2.SetActive(!_singlePlayerCam2.activeSelf);
            _singlePlayerCam1.SetActive(!_singlePlayerCam1.activeSelf);
        }
    }
}
