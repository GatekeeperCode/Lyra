using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void OnStartButtonDown()
    {
        SceneManager.LoadScene("Level1");
    }

    public void onNetworkedDown()
    {
        SceneManager.LoadScene("NetworkScene");
    }
}
