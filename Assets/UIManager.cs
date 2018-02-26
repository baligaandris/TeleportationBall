using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static int score1 = 0;
    public static int score2 = 0;
    public Text scoreText;

	// Use this for initialization
	void Start () {

       

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// Add score and determine when player 1 wins
    public void IncrementScore()
    {
        if (gameObject.tag=="ui")
        {
            score1++;
            scoreText.text = "" + score1;
            if (score1 >= 5)
            {
                Application.LoadLevel("Player1Win");
            }
        }
        if (gameObject.tag=="ui2")
        {
            score2++;
            scoreText.text = "" + score2;
            if (score2 >= 5)
            {
                Application.LoadLevel("Player2Win");
            }
        }

    }

}
