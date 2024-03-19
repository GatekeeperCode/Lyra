using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxResetScript : MonoBehaviour
{
    public GameObject[] boxes;
    public GameObject[] resetPoints;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.25f;

    private void Start()
    {
        _asource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _asource.PlayOneShot(_clip, _volume);
        if(collision.gameObject.tag == "Eurydice" || collision.gameObject.tag == "Orpheus")
        {
            for(int i=0; i<boxes.Length; i++)
            {
                boxes[i].transform.position = resetPoints[i].transform.position;
            }
        }
    }
}
