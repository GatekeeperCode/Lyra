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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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

        PlayerPrefs.SetInt("playerCount", 1);
    }

    public void OnLocalDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer\n-Selected";
        networked.text = "Networked Multiplayer";

        PlayerPrefs.SetInt("playerCount", 2);
    }

    public void onNetworkedDown()
    {
        single.text = "Single Player";
        local.text = "Local Multiplayer";
        networked.text = "Networked Multiplayer\n-Selected";

        PlayerPrefs.SetInt("playerCount", 2); //change to 3 later
    }


}
