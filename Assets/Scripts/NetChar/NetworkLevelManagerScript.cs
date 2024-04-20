using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkLevelManagerScript : NetworkBehaviour
{
    public Text _timer;
    public GameObject _paused;
    public GameObject _muteButton;
    public Sprite _muteImage;
    public Sprite _unMuteImage;
    public string _thisScene;
    public string _nextScene;
    public bool pausedGame = false;
    bool muted = false;
    float startTime;
    float pausedStartTime;
    float previousLevelTime;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        if (IsServer)
        {
            startTime = Time.time;
            pausedStartTime = startTime;
            if (_nextScene == "Level2")
            {
                previousLevelTime = 0;
            }
            else
            {
                previousLevelTime = (float)PlayerPrefs.GetFloat("CurrentTime");
            }
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

        //Paused Screen
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button7) || (PlayerPrefs.GetInt("playerCount") == 2 && (Input.GetKeyDown(KeyCode.Joystick2Button7))))
        {
            if (!pausedGame) //entering paused screen
            {
                pausedStartTime = Time.time;
            }
            else //leaving paused screen
            {
                float pausedEndTime = Time.time;
                startTime = (startTime + (pausedEndTime - pausedStartTime));
            }

            pausedGame = !pausedGame;
            _paused.gameObject.SetActive(!_paused.gameObject.activeSelf);
        }

        if (!pausedGame)
        {
            currentTime = (Time.time + previousLevelTime - startTime);
        }

        float minutes = (int)currentTime / 60;
        float seconds = (int)currentTime % 60;
        _timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + " ";
    }

    public void Nextlevel()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime);

        if (_nextScene == "EndScene")
        {
            EndGame();
        }
        else
        {
            SceneManager.LoadScene(_nextScene);
        }
    }
    private void EndGame()
    {
        PlayerPrefs.SetFloat("ThisTime", currentTime);
        if (PlayerPrefs.HasKey("BestTime"))
        {
            if (PlayerPrefs.GetFloat("BestTime") / 60 > currentTime / 60)
            {
                PlayerPrefs.SetFloat("BestTime", currentTime);
            }
            else if ((PlayerPrefs.GetFloat("BestTime") / 60 == currentTime / 60) && (PlayerPrefs.GetFloat("BestTime") % 60 > currentTime % 60))
            {
                PlayerPrefs.SetFloat("BestTime", currentTime);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
        }

        SceneManager.LoadScene(_nextScene);
    }

    public void OnRestartDown()
    {
        SceneManager.LoadScene(_thisScene);
        print("restart");
    }

    public void onQuitDown()
    {
        SceneManager.LoadScene("MenuScene");
        print("quit");
    }

    public void onMuteDown()
    {
        //Pause/play audioclip here

        print("muted");
        if (muted)
        {
            _muteButton.GetComponent<Image>().sprite = _unMuteImage;
        }
        else
        {
            _muteButton.GetComponent<Image>().sprite = _muteImage;
        }
        muted = !muted;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
