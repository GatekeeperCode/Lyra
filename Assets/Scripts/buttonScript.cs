using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public wallScript _wall;
    public bool _isButton1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isButton1)
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
