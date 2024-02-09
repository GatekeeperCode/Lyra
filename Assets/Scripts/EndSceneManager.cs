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
        if (!PlayerPrefs.HasKey("TimeSeconds"))
        {
            PlayerPrefs.SetInt("TimeSeconds", 0);
            PlayerPrefs.SetInt("TimeMinutes", 0);
            print("idk2");
        }
        if (!PlayerPrefs.HasKey("BestSeconds"))
        {
            PlayerPrefs.SetInt("BestSeconds", 0);
            PlayerPrefs.SetInt("BestMinutes", 0);
            print("idk3");
        }

        _Time.text = "Time: " + PlayerPrefs.GetInt("TimeMinutes").ToString("00") + ":" + PlayerPrefs.GetInt("TimeSeconds").ToString("00") +
                        "\nBest: " + PlayerPrefs.GetInt("BestMinutes").ToString("00") + ":" + PlayerPrefs.GetInt("BestSeconds").ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartButtonDown()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
