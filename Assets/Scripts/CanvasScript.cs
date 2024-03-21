using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasScript : MonoBehaviour
{
    public GameObject _button1;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_button1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //A Bad fix, but the code is kept for reference.
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_button1);
        }
    }
}
