using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetManagerScript : MonoBehaviour
{
    public Text single;
    public Text local;
    public Text networked;
    public Text netInfo;

    // Start is called before the first frame update
    void Start()
    {
        netInfo.gameObject.SetActive(false);
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
        SceneManager.LoadScene("MenuScene");
    }

    public void onSinglePlayerDown()
    {
        single.text = "Single Player\n-Selected";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer";
        netInfo.gameObject.SetActive(false);

        PlayerPrefs.SetInt("playerCount", 1);
    }

    public void OnLocalDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer\n-Selected";
        networked.text = "Networked Multiplayer";
        netInfo.gameObject.SetActive(false);

        PlayerPrefs.SetInt("playerCount", 2);
    }

    public void onNetworkedDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer\n-Selected";
        netInfo.gameObject.SetActive(true);

        PlayerPrefs.SetInt("playerCount", 3);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}
