using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeBoxScript : MonoBehaviour
{
    public Transform _respawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Transform>().position = _respawnLocation.position;
    }
}
