using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalText : MonoBehaviour {

    public string text;
    public bool display = false;
    GUIStyle Font;
    public float targetTime = 1.0f;
    bool Var;

    // Use this for initialization
    void Start()
    {

        Font = new GUIStyle();

        Font.fontSize = 32;

    }

    // Update is called once per frame
    void Update()
    {

        if (Var)
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

        display = true;
        Var = true;
        targetTime = 1.0f;

    }

    void timerEnded()
    {

        display = false;

    }

    void OnGUI()
    {

        if (display == true)

        {

            GUI.Label(new Rect(460, 80, 200, 80), text, Font);

        }

    }
}

