using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public wallScript _wall;
    public bool _isButton1;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.25f;

    private void Start()
    {
        _asource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_isButton1)
        {
            _wall._button1Pressed = true;
        }
        else
        {
            _wall._button2Pressed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _asource.PlayOneShot(_clip, _volume);
        if (_isButton1)
        {
            _wall._button1Pressed = true;
        }
        else
        {
            _wall._button2Pressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //_asource.PlayOneShot(_clip, _volume);
        if (_isButton1)
        {
            _wall._button1Pressed = false;
        }
        else
        {
            _wall._button2Pressed = false;
        }
    }
}
