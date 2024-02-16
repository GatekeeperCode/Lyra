using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    bool OrpheusGoal = false;
    bool EurydiceGoal = false;

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
            _asource.PlayOneShot(_clip, _volume);
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
