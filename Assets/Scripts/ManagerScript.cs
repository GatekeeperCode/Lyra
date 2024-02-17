using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public Text _timer;
    public string _nextScene;
    float startTime;
    int minutes;
    int seconds;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int currentTime = (int)(Time.time - startTime);
        minutes = currentTime / 60;
        seconds = currentTime % 60;
        _timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + " ";
    }

    public void Nextlevel()
    {
        if(_nextScene == "EndScene")
        {
            EndGame();
        } else
        {
            PlayerPrefs.SetFloat("currentMinutes", minutes);
            PlayerPrefs.SetFloat("currentSeconds", seconds);

            SceneManager.LoadScene(_nextScene);
        }
    }
    private void EndGame()
    {
        PlayerPrefs.SetInt("TimeMinutes", minutes);
        PlayerPrefs.SetInt("TimeSeconds", seconds);

        if (PlayerPrefs.HasKey("BestSeconds"))
        {
            if(PlayerPrefs.GetInt("BestMinutes") > minutes)
            {
                PlayerPrefs.SetInt("BestMinutes", minutes);
                PlayerPrefs.SetInt("BestSeconds", seconds);
            } else if (PlayerPrefs.GetInt("BestMinutes") == minutes)
            {
                if (PlayerPrefs.GetInt("BestSeconds") > seconds)
                {
                    PlayerPrefs.SetInt("BestSeconds", seconds);
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestMinutes", minutes);
            PlayerPrefs.SetInt("BestSeconds", seconds);
        }

        SceneManager.LoadScene(_nextScene);

    }
}
