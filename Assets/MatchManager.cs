﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    public GameObject countDownText;
	public GameObject matchTimerText;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public static int player1Score;
    public static int player2Score;
    bool matchStarted = false;
    float countDownToMatch = 4;
	float matchTimeLimit = 120;
    GameObject[] obstacles;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        ResetAfterGoal();
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

	
	// Update is called once per frame
	void Update () {
		// Initiate countdown sequence
		if (matchStarted == false) 
		{
			countDownToMatch -= Time.deltaTime;
			countDownText.GetComponent<Text> ().text = Mathf.Floor (countDownToMatch).ToString ();
			if (countDownToMatch <= 0) 
			{
				StartMatch ();
                
			}
		}
		// Activate time limit
		if (matchStarted == true)
		{
			matchTimeLimit -= Time.deltaTime;
            string countdownstr = Mathf.Floor(matchTimeLimit).ToString(); //spacing out the time display so it fits into the UI
            if (countdownstr.Length==3)
            {
                matchTimerText.GetComponent<Text>().text = countdownstr[0] + "   " + countdownstr[1] + "  " + countdownstr[2];
            }
            else if (countdownstr.Length == 2)
            {
                matchTimerText.GetComponent<Text>().text = "     " + countdownstr[0] + "  " + countdownstr[1];
            }
            else if (countdownstr.Length == 1)
            {
                matchTimerText.GetComponent<Text>().text = "        " + countdownstr[0];
            }
            
            // Determine which player has highest score when time equals 0
            if (matchTimeLimit <= 0) 
			{
				if (player1Score > player2Score)
				{
					Application.LoadLevel ("Player1Win");
				}
				if (player2Score > player1Score)
				{
					Application.LoadLevel ("Player2Win");
				}
                else if (player1Score == player2Score)
                {
                    Application.LoadLevel("Match Draw");
                }
			}
		}
	}
    public void ResetAfterGoal() {
        countDownToMatch = 4;
        matchStarted = false;
        player1.GetComponent<PlayerMovement>().enabled = false;
        player2.GetComponent<PlayerMovement>().enabled = false;
        countDownText.SetActive(true);
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].GetComponent<Obstacle>().ResetPosition();
        }
    }
    private void StartMatch() {
        player1.GetComponent<PlayerMovement>().enabled = true;
        player2.GetComponent<PlayerMovement>().enabled = true;
        countDownText.SetActive(false);
        matchStarted = true;
    }


}
