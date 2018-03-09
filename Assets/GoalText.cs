using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalText : MonoBehaviour {

    public string text;
    public bool display = false;
    GUIStyle Font;
    public float targetTime = 0.5f;
    bool var;


    public GameObject goalText;
    // Use this for initialization
    void Start()
    {

        Font = new GUIStyle();

        Font.fontSize = 32;

    }

    // Update is called once per frame
    void Update()
    {

        if (var)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D Ball)
    {

        if (Ball.gameObject.tag == "ball")
            {

            goalText.SetActive(true);
            var = true;
        }
    }

    void timerEnded()
    {

        goalText.SetActive(false);
        targetTime = 0.5f;
        var = false;
    }

    void OnGUI()
    {

        //if (display == true)

        //{

        //    GUI.Label(new Rect(460, 80, 200, 80), text, Font);

        //}

    }
}

