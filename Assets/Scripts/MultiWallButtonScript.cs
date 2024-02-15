using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWallButtonScript : MonoBehaviour
{
    public wallScript _wall1;
    public wallScript _wall2;
    public bool _isButton1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Orpheus"||collision.tag=="Eurydice")
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
        if (collision.tag == "Orpheus" || collision.tag == "Eurydice")
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
