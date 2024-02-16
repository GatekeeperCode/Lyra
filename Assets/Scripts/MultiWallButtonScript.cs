using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWallButtonScript : MonoBehaviour
{
    public wallScript _wall1;
    public wallScript _wall2;
    public bool _isButton1;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.5f;

    private void Start()
    {
        _asource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _asource.PlayOneShot(_clip, _volume);
        if (collision.tag=="Orpheus"||collision.tag=="Eurydice" || collision.tag=="Box")
        {
            if (_isButton1)
            {
                _wall1._button1Pressed = true;
                _wall2._button1Pressed = true;
            }
            else
            {
                _wall1._button2Pressed = true;
                _wall2._button2Pressed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Left");
        _asource.PlayOneShot(_clip, _volume);
        if (collision.tag == "Orpheus" || collision.tag == "Eurydice" || collision.tag == "Box")
        {
            if (_isButton1)
            {
                _wall1._button1Pressed = false;
                _wall2._button1Pressed = false;
            }
            else
            {
                _wall1._button2Pressed = false;
                _wall2._button2Pressed = false;
            }
        }   
    }
}
