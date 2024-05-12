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
    public GameObject _orphSpawn;
    public GameObject _eurySpawn;

    AudioSource _audio;
    float startTime;
    float pausedStartTime;
    float previousLevelTime;
    //float currentTime;
    NetworkVariable<float> currentTime = new NetworkVariable<float>();
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

        _audio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("TimeCut"))
        {
            _audio.time = PlayerPrefs.GetFloat("TimeCut");
        }
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            onMuteButtonDown();
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("NetCharacter");

        for(int i=0; i<players.Length; i++)
        {
            players[i].GetComponent<EuryOrphFlipScript>().alreadyRun = false;
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
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            pauseGameServerRpc(pausedGame);  
        }

        if (!pausedGame && IsServer)
        {
            currentTime.Value = (Time.time + previousLevelTime - startTime);
        }

        float minutes = (int)currentTime.Value / 60;
        float seconds = (int)currentTime.Value % 60;
        _timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + " ";
    }

    [ServerRpc(RequireOwnership = false)]
    private void pauseGameServerRpc(bool oldPaused)
    {
        if (!oldPaused) //entering paused screen
        {
            pausedStartTime = Time.time;
        }
        else //leaving paused screen
        {
            float pausedEndTime = Time.time;
            startTime = (startTime + (pausedEndTime - pausedStartTime));
        }

        pausedGame = !oldPaused;
        _paused.gameObject.SetActive(!_paused.gameObject.activeSelf);

        setPausedClientRpc(pausedGame);
    }

    [ClientRpc]
    private void setPausedClientRpc(bool newPaused)
    {
        if(!IsServer)
        {
            pausedGame = newPaused;
            _paused.gameObject.SetActive(!_paused.gameObject.activeSelf);
        }
    }

    public void Nextlevel()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime.Value);

        if (_nextScene == "EndScene")
        {
            EndGame();
        }
        else
        {
            PlayerPrefs.SetFloat("TimeCut", _audio.time);
            NetworkManager.Singleton.SceneManager.LoadScene(_nextScene, LoadSceneMode.Single);
        }
    }

    private void EndGame()
    {
        PlayerPrefs.SetFloat("ThisTime", currentTime.Value);
        if (PlayerPrefs.HasKey("BestTime"))
        {
            if (PlayerPrefs.GetFloat("BestTime") / 60 > currentTime.Value / 60)
            {
                PlayerPrefs.SetFloat("BestTime", currentTime.Value);
            }
            else if ((PlayerPrefs.GetFloat("BestTime") / 60 == currentTime.Value / 60) && (PlayerPrefs.GetFloat("BestTime") % 60 > currentTime.Value % 60))
            {
                PlayerPrefs.SetFloat("BestTime", currentTime.Value);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", currentTime.Value);
        }

        PlayerPrefs.SetFloat("TimeCut", _audio.time);

        NetworkManager.Singleton.SceneManager.LoadScene(_nextScene, LoadSceneMode.Single);
    }

    public void OnRestartDown()
    {
        PlayerPrefs.SetFloat("TimeCut", _audio.time);

        GameObject[] g = GameObject.FindGameObjectsWithTag("NetCharacter");

        if(g.Length == 1)
        {
            g[0].transform.position = _orphSpawn.transform.position;
            g[0].transform.GetChild(0).localPosition = Vector2.zero;
            g[0].transform.GetChild(1).localPosition = Vector2.zero;
        }
        else
        {
            g[0].transform.position = _orphSpawn.transform.position;
            g[0].transform.GetChild(0).localPosition = Vector2.zero;
            g[0].transform.GetChild(1).localPosition = Vector2.zero;

            g[1].transform.position = _orphSpawn.transform.position;
            g[1].transform.GetChild(0).localPosition = Vector2.zero;
            g[1].transform.GetChild(1).localPosition = Vector2.zero;
        }
    }

    public void onQuitDown()
    {
        quitServerRpc();
    }

    [ServerRpc (RequireOwnership = false)]
    private void quitServerRpc()
    {
        quitClientRpc();
        NetworkManager.Singleton.Shutdown();
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }

    [ClientRpc]
    private void quitClientRpc()
    {
        PlayerPrefs.SetFloat("TimeCut", _audio.time);
        SceneManager.LoadScene("MenuScene");
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

    [ClientRpc]
    void changeSceneClientRpc()
    {
        PlayerPrefs.SetFloat("TimeCut", _audio.time);
        NetworkManager.SceneManager.LoadScene(_nextScene, LoadSceneMode.Additive);
        NetworkManager.SceneManager.UnloadScene(SceneManager.GetActiveScene());
    }
}
