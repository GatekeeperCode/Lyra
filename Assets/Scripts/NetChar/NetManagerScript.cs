using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class NetManagerScript : MonoBehaviour
{
    public Text single;
    public Text local;
    public Text networked;

    public GameObject _hostButton;
    public GameObject _clientButton;
    public InputField _ipAddress;

    public GameObject _muteButton;
    public Sprite _muteImage;
    public Sprite _unMuteImage;

    AudioSource _audio;

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

        _audio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("TimeCut"))
        {
            _audio.time = PlayerPrefs.GetFloat("TimeCut");
        }
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            _audio.Pause();
            _muteButton.GetComponent<Image>().sprite = _muteImage;
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
        PlayerPrefs.SetFloat("TimeCut", _audio.time);
        PlayerPrefs.SetString("ipAddress", _ipAddress.text);
        SceneManager.LoadScene("MenuScene");
    }

    public void onMuteButtonDown()
    {
        if (_audio.isPlaying)
        {
            _audio.Pause();
            PlayerPrefs.SetInt("Muted", 1);
            _muteButton.GetComponent<Image>().sprite = _muteImage;
        }
        else
        {
            _audio.Play();
            PlayerPrefs.SetInt("Muted", 0);
            _muteButton.GetComponent<Image>().sprite = _unMuteImage;
        }
    }

    public void onSinglePlayerDown()
    {
        single.text = "Single Player\n-Selected";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer";
        _hostButton.SetActive(false);
        _clientButton.SetActive(false);
        _ipAddress.gameObject.SetActive(false);

        PlayerPrefs.SetInt("playerCount", 1);
    }

    public void OnLocalDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer\n-Selected";
        networked.text = "Networked Multiplayer";
        _hostButton.SetActive(false);
        _clientButton.SetActive(false);
        _ipAddress.gameObject.SetActive(false);

        PlayerPrefs.SetInt("playerCount", 2);
    }

    public void onNetworkedDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer\n-Selected";

        _hostButton.SetActive(true);
        _clientButton.SetActive(true);
        _ipAddress.gameObject.SetActive(true);

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
        _ipAddress.gameObject.SetActive(true);
    }

    public void becomeHost()
    {
        PlayerPrefs.SetInt("host", 1);
        _hostButton.GetComponentInChildren<Text>().text = "Host (Orpheus) - Selected";
        _clientButton.GetComponentInChildren<Text>().text = "Client (Eurydice)";
        _ipAddress.gameObject.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}
