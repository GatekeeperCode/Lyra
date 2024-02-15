using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxResetScript : MonoBehaviour
{
    public GameObject[] boxes;
    public GameObject[] resetPoints;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Eurydice" || collision.gameObject.tag == "Orpheus")
        {
            for(int i=0; i<boxes.Length; i++)
            {
                boxes[i].transform.position = resetPoints[i].transform.position;
            }
        }
    }
}
