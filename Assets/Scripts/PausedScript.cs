using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PausedScript : MonoBehaviour
{
    public GameObject _pauseButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_pauseButton);
    }

    private void OnDisable()
    {
        //EventSystem.current.SetSelectedGameObject(null);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
