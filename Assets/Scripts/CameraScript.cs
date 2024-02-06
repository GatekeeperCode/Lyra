using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    OrpheusScript Orpheus;
    EurydiceScript Eurydice;

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
        transform.position = new Vector3((Orpheus.transform.position.x + Eurydice.transform.position.x)/2 + offset.x, (Orpheus.transform.position.y+ Eurydice.transform.position.y) /2 + offset.y, offset.z);

        if (Orpheus.transform.position.x - Eurydice.transform.position.x > 25 || Eurydice.transform.position.x - Orpheus.transform.position.x > 25)
        {
            gameObject.GetComponent<Camera>().orthographicSize = 8;
        }
        else if (Orpheus.transform.position.x - Eurydice.transform.position.x > 20 || Eurydice.transform.position.x - Orpheus.transform.position.x > 20)
        {
            gameObject.GetComponent<Camera>().orthographicSize = 7;
        }
        else if (Orpheus.transform.position.x - Eurydice.transform.position.x > 15 || Eurydice.transform.position.x - Orpheus.transform.position.x > 15)
        {
            gameObject.GetComponent<Camera>().orthographicSize= 6;
        }
        else
        {
            gameObject.GetComponent<Camera>().orthographicSize = 5;
        }
    }
}
