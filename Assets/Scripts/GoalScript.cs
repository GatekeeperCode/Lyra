using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    bool OrpheusGoal = false;
    bool EurydiceGoal = false;

    ManagerScript manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //dev key -> delete later
        {
            manager.EndGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Orpheus")
        {
            OrpheusGoal = true;
        } else if(collision.gameObject.tag == "Eurydice")
        {
            EurydiceGoal = true;
        }


        if(OrpheusGoal && EurydiceGoal)
        {
            manager.EndGame();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus")
        {
            OrpheusGoal = false;
        }
        else if (collision.gameObject.tag == "Eurydice")
        {
            EurydiceGoal = false;
        }
    }

}
