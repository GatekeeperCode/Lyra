using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;

public class EndSceneManager : NetworkBehaviour
{
    public Text _Time;
    //public GameObject _muteButton;
    public Sprite _muteImage;
    public Sprite _unMuteImage;

    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", 0);
        }if (!PlayerPrefs.HasKey("ThisTime"))
        {
            PlayerPrefs.SetFloat("ThisTime", 0);
        }

        _Time.text = "Time: " + (PlayerPrefs.GetFloat("ThisTime")/60).ToString("00") + ":" + (PlayerPrefs.GetFloat("ThisTime")%60).ToString("00") +
                        "\nBest: " + (PlayerPrefs.GetFloat("BestTime")/60).ToString("00") + ":" + (PlayerPrefs.GetFloat("BestTime")%60).ToString("00");

        _audio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("TimeCut"))
        {
            _audio.time = PlayerPrefs.GetFloat("TimeCut");
        }
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            onMuteButtonDown();
            StartCoroutine(postStart());
        }
    }

    private IEnumerator postStart()
    {
        yield return new WaitForSeconds(.0000001f);
        if (_audio.isPlaying)
        {
            onMuteButtonDown();
            print("Initial mute did not work");
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


    public void OnRestartButtonDown()
    {
        PlayerPrefs.SetFloat("TimeCut", _audio.time);
        NetworkManager.Singleton.Shutdown();
        if(NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
        SceneManager.LoadScene("MenuScene");
    }

    public void onMuteButtonDown()
    {
        if (_audio.isPlaying)
        {
            _audio.Pause();
            PlayerPrefs.SetInt("Muted", 1);
            //_muteButton.GetComponent<Image>().sprite = _muteImage;
        }
        else
        {
            _audio.Play();
            PlayerPrefs.SetInt("Muted", 0);
            //_muteButton.GetComponent<Image>().sprite = _unMuteImage;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
