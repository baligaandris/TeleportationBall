using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public UIManager ui;

    // Use this for initialization
    void Start()
    {

        ui = GameObject.FindWithTag("ui").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D Ball)

    {

        ui.IncrementScore();
        GameObject.FindGameObjectWithTag("ball").GetComponent<Ball>().ResetPosition();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ResetPosition();
        GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>().ResetPosition();
        GameObject.FindGameObjectWithTag("MatchManager").GetComponent<MatchManager>().ResetAfterGoal();
    }
}
