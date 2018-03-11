using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private float goalTime;
    public float goalTimeMax = 0.3f;
    private bool itIsGoalTime = false;
    public AudioSource sound;

    public UIManager ui;
    // Use this for initialization
    void Start()
    {
        goalTime = goalTimeMax;
        ui = GameObject.FindWithTag("ui").GetComponent<UIManager>();
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (itIsGoalTime)
        {
            Time.timeScale = 0.1f;
            goalTime -= Time.deltaTime;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            if (goalTime <= 0)
            {
                GameObject.FindGameObjectWithTag("ball").GetComponent<Ball>().ResetPosition();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ResetPosition();
                GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>().ResetPosition();
                GameObject.FindGameObjectWithTag("MatchManager").GetComponent<MatchManager>().ResetAfterGoal();
                itIsGoalTime = false;
                goalTime = goalTimeMax;
            }

        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
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
