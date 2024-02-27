using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public bool _button1Pressed;
    public bool _button2Pressed;
    public bool _stayDown;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.8f;
    Vector3 firstPos;
    Vector3 currPos;

    // Start is called before the first frame update
    void Start()
    {
        _asource = GetComponent<AudioSource>();
        firstPos = transform.position;
        currPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_button1Pressed && _button2Pressed)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(firstPos.x, firstPos.y - 20), .002f);
            if(!_asource.isPlaying)
            {
                _asource.PlayOneShot(_clip, _volume);
            }
        }
        else if(!_stayDown && (transform.position.y <= firstPos.y - .1) && !_button1Pressed)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(firstPos.x, firstPos.y), .02f);
            if (!_asource.isPlaying)
            {
                _asource.PlayOneShot(_clip, _volume);
            }
        }
        else
        {
            _asource.Stop();
        }
    }
}
