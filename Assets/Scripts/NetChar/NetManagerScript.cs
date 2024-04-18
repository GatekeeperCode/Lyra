using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetManagerScript : MonoBehaviour
{
    public Text single;
    public Text local;
    public Text networked;

    public GameObject _hostButton;
    public GameObject _clientButton;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("playerCount") == 2)
        {
            OnLocalDown();
        }
        else if (PlayerPrefs.GetInt("playerCount") == 3)
        {
            onNetworkedDown();
        } else
        {
            onSinglePlayerDown();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Quit Game
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }


    public void OnBackButtonDown()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void onSinglePlayerDown()
    {
        single.text = "Single Player\n-Selected";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer";
        _hostButton.SetActive(false);
        _clientButton.SetActive(false);

        PlayerPrefs.SetInt("playerCount", 1);
    }

    public void OnLocalDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer\n-Selected";
        networked.text = "Networked Multiplayer";
        _hostButton.SetActive(false);
        _clientButton.SetActive(false);

        PlayerPrefs.SetInt("playerCount", 2);
    }

    public void onNetworkedDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer\n-Selected";

        _hostButton.SetActive(true);
        _clientButton.SetActive(true);
        _clientButton.GetComponentInChildren<Text>().text = "Client (Eurydice) - Selected";
        _hostButton.GetComponentInChildren<Text>().text = "Host (Orpheus)";

        PlayerPrefs.SetInt("playerCount", 3);
        PlayerPrefs.SetInt("host", 0);
    }

    public void becomeClient()
    {
        PlayerPrefs.SetInt("host", 0);
        _clientButton.GetComponentInChildren<Text>().text = "Client (Eurydice) - Selected";
        _hostButton.GetComponentInChildren<Text>().text = "Host (Orpheus)";
    }

    public void becomeHost()
    {
        PlayerPrefs.SetInt("host", 1);
        _hostButton.GetComponentInChildren<Text>().text = "Host (Orpheus) - Selected";
        _clientButton.GetComponentInChildren<Text>().text = "Client (Eurydice)";
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}
