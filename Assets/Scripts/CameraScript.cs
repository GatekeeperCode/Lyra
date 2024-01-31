using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    OrpheusScript Orpheus;

    Vector3 offset = new Vector3(0, 0, -10);
    // Start is called before the first frame update
    void Start()
    {
        Orpheus = FindObjectOfType<OrpheusScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Orpheus.transform.position.x + offset.x, Orpheus.transform.position.y + offset.y, offset.z);
    }
}
