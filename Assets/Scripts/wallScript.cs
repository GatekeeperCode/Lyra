using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class wallScript : NetworkBehaviour
{
    public bool _button1Pressed;
    public bool _button2Pressed;
    public bool _stayDown;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.8f;
    Vector3 firstPos;
    public float wallUpSpeed = 0.05f;
    public float wallDownSpeed = 0.05f;
    int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        _asource = GetComponent<AudioSource>();
        firstPos = transform.position;
        playerCount = PlayerPrefs.GetInt("playerCount");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCount!=3)
        {
            if (_button1Pressed && _button2Pressed)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(firstPos.x, firstPos.y - 20), wallDownSpeed);
                if (!_asource.isPlaying)
                {
                    _asource.PlayOneShot(_clip, _volume);
                }
            }
            else if (!_stayDown && (transform.position.y <= firstPos.y - .1) && !_button1Pressed)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(firstPos.x, firstPos.y), wallUpSpeed);
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
        else
        {
            if (_button1Pressed && _button2Pressed)
            {
                //transform.position = Vector2.Lerp(transform.position, new Vector2(firstPos.x, firstPos.y - 20), wallDownSpeed);
                moveWallServerRpc(transform.position, new Vector2(firstPos.x, firstPos.y - 20), wallDownSpeed);
                if (!_asource.isPlaying)
                {
                    _asource.PlayOneShot(_clip, _volume);
                }
            }
            else if (!_stayDown && (transform.position.y <= firstPos.y - .1) && !_button1Pressed)
            {
                //transform.position = Vector2.Lerp(transform.position, new Vector2(firstPos.x, firstPos.y), wallUpSpeed);
                moveWallServerRpc(transform.position, new Vector2(firstPos.x, firstPos.y), wallUpSpeed);
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

    [ServerRpc]
    void moveWallServerRpc(Vector2 posFrom, Vector2 posTo, float speed)
    {
        transform.position = Vector2.Lerp(posFrom, posTo, speed);
        updatePositionClientRpc(transform.position);
    }

    [ClientRpc]
    void updatePositionClientRpc(Vector2 pos)
    {
        transform.position = pos;
    }
}
