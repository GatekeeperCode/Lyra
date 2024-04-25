using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    public GameObject _muteButton;
    public Sprite _muteImage;
    public Sprite _unMuteImage;

    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
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

    public void OnStartButtonDown()
    {
        PlayerPrefs.SetFloat("TimeCut", _audio.time);
        SceneManager.LoadScene("Level1");
    }

    public void onNetworkedDown()
    {
        PlayerPrefs.SetFloat("TimeCut", _audio.time);
        SceneManager.LoadScene("NetworkScene");
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

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
