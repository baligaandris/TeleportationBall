using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static int score = 0;
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

        score++;
        scoreText.text = "" + score;
		if (score >= 5) 
		{
			Application.LoadLevel("Player1Win");
		}
    }

}
