using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("TimeCut"))
        {
            _audio.time = PlayerPrefs.GetFloat("TimeCut");
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
    private void onMuteButtonDown()
    {
        if(_audio.isPlaying)
        {
            _audio.Pause();
        } else
        {
            _audio.Play();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
