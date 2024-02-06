using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    OrpheusScript Orpheus;
    EurydiceScript Eurydice;

    int CameraSize = 5;
    Vector3 offset = new Vector3(0, 0, -10);
    // Start is called before the first frame update
    void Start()
    {
        Orpheus = FindObjectOfType<OrpheusScript>();
        Eurydice = FindObjectOfType<EurydiceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float EuryX = Eurydice.transform.position.x;
        float EuryY = Eurydice.transform.position.y;
        float OrphX = Orpheus.transform.position.x;
        float OrphY = Orpheus.transform.position.y;

        // center camera on both characters
        transform.position = new Vector3((OrphX + EuryX) / 2 + offset.x, (OrphY + EuryY) / 2 + offset.y, offset.z);

        // determine camera zoom
        if (OrphX - EuryX > 25 || EuryX - OrphX > 25)
        {
            CameraSize = 8;
        }
        else if (OrphX - EuryX > 20 || EuryX - OrphX > 20)
        {
            CameraSize = 7;
        }
        else if (OrphX - EuryX > 15 || EuryX - OrphX > 15)
        {
            CameraSize = 6;
        }
        else
        {
            CameraSize = 5;
        }

        // set camera orthographic size
        if (Mathf.Abs(gameObject.GetComponent<Camera>().orthographicSize - CameraSize) <= 0.02f)
        {
            gameObject.GetComponent<Camera>().orthographicSize = CameraSize;
        }
        else if (gameObject.GetComponent<Camera>().orthographicSize >= CameraSize)
        {
            gameObject.GetComponent<Camera>().orthographicSize -= 0.01f;
        } else
        {
            gameObject.GetComponent<Camera>().orthographicSize += 0.01f;
        }
    }
}
