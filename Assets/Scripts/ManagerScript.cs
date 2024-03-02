using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public Text _timer;
    public Text _paused;
    public string _nextScene;
    public bool pausedGame = false;
    float startTime;
    float pausedStartTime;
    float previousLevelTime;
    int currentTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        pausedStartTime = startTime;
        if (_nextScene == "Level2")
        {
            previousLevelTime = 0;
        } else
        {
            previousLevelTime = (float)PlayerPrefs.GetInt("CurrentTime");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Quit Game
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.DeleteKey("CurrentTime");
            PlayerPrefs.DeleteKey("ThisTime");
            PlayerPrefs.DeleteKey("BestTime");
            PlayerPrefs.DeleteKey("playerCount");

            Application.Quit();
        }

        //Paused Screen
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pausedGame) //entering paused screen
            {
                pausedStartTime = (int)currentTime;
            } else //leaving paused screen
            {
                startTime = (startTime + (Time.time-pausedStartTime));
            }

            pausedGame = !pausedGame;
            _paused.gameObject.SetActive(!_paused.gameObject.activeSelf);
        }

        if (pausedGame)
        {
            currentTime = (int)pausedStartTime;
        } else
        {
            currentTime = (int)(Time.time + previousLevelTime - startTime);
        }

        int minutes = currentTime / 60;
        int seconds = currentTime % 60;
        _timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + " ";
    }

    public void Nextlevel()
    {
        PlayerPrefs.SetInt("CurrentTime", currentTime);

        if (_nextScene == "EndScene")
        {
            EndGame();
        } else
        {
            SceneManager.LoadScene(_nextScene);
        }
    }
    private void EndGame()
    {
        PlayerPrefs.SetInt("ThisTime", currentTime);
        if (PlayerPrefs.HasKey("BestTime"))
        {
            if(PlayerPrefs.GetInt("BestTime")/60 > currentTime/60)
            {
                PlayerPrefs.SetInt("BestTime", currentTime);
            }
            else if ((PlayerPrefs.GetInt("BestTime") / 60 == currentTime / 60) && (PlayerPrefs.GetInt("BestTime") % 60 > currentTime % 60))
            {
                PlayerPrefs.SetInt("BestTime", currentTime);
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestTime", currentTime);
        }

        SceneManager.LoadScene(_nextScene);

    }
}
