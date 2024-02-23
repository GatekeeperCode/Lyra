using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    public Text _Time;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BestTime"))
        {
            PlayerPrefs.SetInt("BestTime", 0);
        }if (!PlayerPrefs.HasKey("ThisTime"))
        {
            PlayerPrefs.SetInt("ThisTime", 0);
        }

        _Time.text = "Time: " + (PlayerPrefs.GetInt("ThisTime")/60).ToString("00") + ":" + (PlayerPrefs.GetInt("ThisTime") % 60).ToString("00") +
                        "\nBest: " + (PlayerPrefs.GetInt("BestTime") / 60).ToString("00") + ":" + (PlayerPrefs.GetInt("BestTime") % 60).ToString("00");
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
    }


    public void OnRestartButtonDown()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
