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
            PlayerPrefs.SetFloat("BestTime", 0);
        }if (!PlayerPrefs.HasKey("ThisTime"))
        {
            PlayerPrefs.SetFloat("ThisTime", 0);
        }

        _Time.text = "Time: " + (PlayerPrefs.GetFloat("ThisTime")/60).ToString("00") + ":" + (PlayerPrefs.GetFloat("ThisTime")%60).ToString("00") +
                        "\nBest: " + (PlayerPrefs.GetFloat("BestTime")/60).ToString("00") + ":" + (PlayerPrefs.GetFloat("BestTime")%60).ToString("00");
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
        SceneManager.LoadScene("MenuScene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
