using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal2 : MonoBehaviour
{
    private float goalTime;
    public float goalTimeMax = 0.3f;
    private bool itIsGoalTime = false;
    public AudioSource sound;


    public bool Goal1 = false;
    public UIManager ui;

    // Use this for initialization
    void Start()
    {
        goalTime = goalTimeMax;
        sound = GetComponent<AudioSource>();
        if (Goal1)
        {
            ui = GameObject.FindGameObjectWithTag("ui").GetComponent<UIManager>();
        }
        else
        {
            ui = GameObject.FindGameObjectWithTag("ui2").GetComponent<UIManager>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (itIsGoalTime)
        {
            Time.timeScale = 0.1f;
            goalTime -= Time.deltaTime;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            if (goalTime<=0)
            {
                GameObject.FindGameObjectWithTag("ball").GetComponent<Ball>().ResetPosition();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ResetPosition();
                GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>().ResetPosition();
                GameObject.FindGameObjectWithTag("MatchManager").GetComponent<MatchManager>().ResetAfterGoal();
                itIsGoalTime = false;
                goalTime = goalTimeMax;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                sound.Stop();
            }
            
        }
        else
        {
            
        }
    }

    void OnTriggerEnter2D(Collider2D Ball)

    {

        if (Ball.gameObject.tag == "ball")
        {
            ui.IncrementScore();
            itIsGoalTime = true;
            sound.Play();
        }
    }
}
