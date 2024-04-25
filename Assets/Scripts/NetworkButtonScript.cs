using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtonScript : MonoBehaviour
{
    public Button _host;
    public Button _client;
    public Button _local;
    public Button _back;
    public Button _single;
    Navigation btnNav;

    // Start is called before the first frame update
    void Start()
    {
        btnNav = GetComponent<Button>().navigation;
        btnNav.mode = Navigation.Mode.Explicit;
        btnNav.selectOnDown = _single.gameObject.GetComponent<Button>();
        btnNav.selectOnRight = _back.gameObject.GetComponent<Button>();
        btnNav.selectOnUp = _local.gameObject.GetComponent<Button>();
        btnNav.selectOnLeft = _back.gameObject.GetComponent<Button>();
        GetComponent<Button>().navigation = btnNav;
    }

    // Update is called once per frame
    void Update()
    {
        if(_host.gameObject.activeSelf)
        {
            btnNav.selectOnDown = _client.gameObject.GetComponent<Button>();
            btnNav.selectOnRight = _host.gameObject.GetComponent<Button>();
        }    
        else
        {
            btnNav.selectOnDown = _single.gameObject.GetComponent<Button>();
            btnNav.selectOnRight = _back.gameObject.GetComponent<Button>();
        }

        GetComponent<Button>().navigation = btnNav;
    }
}
