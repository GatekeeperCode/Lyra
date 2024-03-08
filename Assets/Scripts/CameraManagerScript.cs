using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public GameObject Orpheus;
    public GameObject Eurydice;
    public GameObject _singlePlayerCam1;
    public GameObject _singlePlayerCam2;
    public GameObject _splitCam1;
    public GameObject _splitCam2;
    public bool orphView = true;
    bool isSinglePlayer;
    Vector3 camOffset = new Vector3(2, 2, -10);

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("playerCount")==2)
        {
            _singlePlayerCam2.SetActive(false);
            _singlePlayerCam1.SetActive(false);
            isSinglePlayer = false;
            _splitCam1.SetActive(true);
            _splitCam2.SetActive(true);
        }
        else
        {
            _singlePlayerCam2.SetActive(false);
            _singlePlayerCam1.SetActive(true);
            isSinglePlayer = true;
            _splitCam1.SetActive(false);
            _splitCam2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for camera switching
        if(Input.GetKeyDown(KeyCode.Space) && isSinglePlayer)
        {
            orphView = !orphView;
            _singlePlayerCam2.SetActive(!_singlePlayerCam2.activeSelf);
            _singlePlayerCam1.SetActive(!_singlePlayerCam1.activeSelf);
        }

        // Center cameras on players
        if(isSinglePlayer)
        {
            if (orphView)
            {
                characterCenter(Orpheus, _singlePlayerCam1);
            }
            else
            {
                characterCenter(Eurydice, _singlePlayerCam2);
            }
        }
        
    }

    private void characterCenter(GameObject character, GameObject camera)
    {
        // Determine Camera X Value
        float camX = camera.transform.position.x;
        if (camera.transform.position.x - character.transform.position.x >= camOffset.x) //character to left
        {
            camX = character.transform.position.x + camOffset.x;
        } else if (character.transform.position.x - camera.transform.position.x >= camOffset.x) //character to right
        {
            camX = character.transform.position.x - camOffset.x;
        }

        // Determine Camera Y Value
        float camY = camera.transform.position.y;
        if (camera.transform.position.y - character.transform.position.y >= camOffset.y) //character below
        {
            camY = character.transform.position.y + camOffset.y;
        }
        else if (character.transform.position.y - camera.transform.position.y >= camOffset.y) //character above
        {
            camY = character.transform.position.y - camOffset.y;
        }

        camera.transform.position = new Vector3(camX, camY, camOffset.z);
    }
}
