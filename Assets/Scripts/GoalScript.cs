using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    public LayerMask OrphLayer;
    public LayerMask EuryLayer;
    ManagerScript manager;
    AudioSource _asource;
    public AudioClip _clip;
    public float _volume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ManagerScript>();
        _asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharactersInGoal() || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Joystick1Button13)) //dev key -> delete later
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

        RaycastHit2D OrphFromLeft = Physics2D.Raycast(new Vector2(GoalVector.x - (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.right, 5f, OrphLayer);
        RaycastHit2D OrphFromRight = Physics2D.Raycast(new Vector2(GoalVector.x + (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.left, 5f, OrphLayer);
        RaycastHit2D EuryfromLeft = Physics2D.Raycast(new Vector2(GoalVector.x - (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.right, 5f, EuryLayer);
        RaycastHit2D EuryFromRight = Physics2D.Raycast(new Vector2(GoalVector.x + (GoalSize.x*5f), GoalVector.y - (GoalSize.y*2f)), Vector2.left, 5f, EuryLayer);

        if (OrphFromLeft.collider != null && OrphFromRight.collider != null) { OrphInGoal = true; }
        if (EuryfromLeft.collider != null && EuryFromRight.collider != null){ EuryInGoal = true; }

        return OrphInGoal && EuryInGoal;
    }

}
