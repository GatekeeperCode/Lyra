using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    public LayerMask OrphLayer;
    public LayerMask EuryLayer;
    ManagerScript manager;
    NetworkLevelManagerScript networkManager;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.5f;
    int playerCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerCount = PlayerPrefs.GetInt("playerCount");
        networkManager = FindObjectOfType<NetworkLevelManagerScript>();
        manager = FindObjectOfType<ManagerScript>();
        _asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharactersInGoal() || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Joystick1Button6)) //dev key -> delete later
        {
            if(playerCount == 3)
            {
                networkManager.Nextlevel();
            } else
            {
                manager.Nextlevel();
            }
        } else if(PlayerPrefs.GetInt("playerCount") == 2 && Input.GetKeyDown(KeyCode.Joystick2Button6)) //also a dev key
        {
            manager.Nextlevel();
        }
    }


    private bool CharactersInGoal()
    {
        bool OrphInGoal = false;
        bool EuryInGoal = false;
        Vector2 GoalVector = transform.position;
        Vector2 GoalSize = transform.localScale;

        RaycastHit2D OrphFromLeft = Physics2D.Raycast(new Vector2(GoalVector.x - (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.right, 4f, OrphLayer);
        RaycastHit2D OrphFromRight = Physics2D.Raycast(new Vector2(GoalVector.x + (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.left, 4f, OrphLayer);
        RaycastHit2D EuryfromLeft = Physics2D.Raycast(new Vector2(GoalVector.x - (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.right, 4f, EuryLayer);
        RaycastHit2D EuryFromRight = Physics2D.Raycast(new Vector2(GoalVector.x + (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.left, 4f, EuryLayer);

        if (OrphFromLeft.collider != null && OrphFromRight.collider != null) { OrphInGoal = true; }
        if (EuryfromLeft.collider != null && EuryFromRight.collider != null){ EuryInGoal = true; }

        return OrphInGoal && EuryInGoal;
    }

}
