﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;

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
	public float matchTimeLimit = 120;
    GameObject[] obstacles;

    PersistentData persistentData;

    public AudioSource timerSound;
    public AudioClip airHornSound;

    public Canvas tutorialCanvas;

    // Use this for initialization
    void Start () {
        persistentData = FindObjectOfType<PersistentData>();
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        ResetAfterGoal();
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
        GameObject gameData = GameObject.Find("PersistentData");
        if (gameData != null)
        {
            PersistentData persistentData = gameData.GetComponent<PersistentData>();
            player1ScoreText.text = persistentData + player1Score.ToString();
            player2ScoreText.text = persistentData + player2Score.ToString();
        }

        timerSound = GetComponent<AudioSource>();

    }

	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            GamePadState state2 = GamePad.GetState(PlayerIndex.Two);
            if (state1.Triggers.Right == 1 && state2.Triggers.Right ==1)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                tutorialCanvas.enabled = false;
            }
        }
        // Initiate countdown sequence
        //print(timerSound.isPlaying);
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
                matchTimerText.GetComponent<Text>().text = countdownstr[0] + "  " + countdownstr[1] + "  " + countdownstr[2];
            }
            else if (countdownstr.Length == 2)
            {
                matchTimerText.GetComponent<Text>().text = "    " + countdownstr[0] + "  " + countdownstr[1];
            }
            else if (countdownstr.Length == 1)
            {
                matchTimerText.GetComponent<Text>().text = "       " + countdownstr[0];
            }

            
            // Determine which player has highest score when time equals 0
            if (matchTimeLimit <= 0) 
			{
                player1Score = GameObject.FindGameObjectWithTag("ui").GetComponent<UIManager>().score1;
                player2Score = GameObject.FindGameObjectWithTag("ui2").GetComponent<UIManager>().score2;
                if (player1Score > player2Score)
				{
                    persistentData.winningPlayer = Players.Player1;
                    if (persistentData.WinnerStaysOnMode)
                    {
                        StartCoroutine(WaitAndLoadlevel("WinnerStaysOnMenu"));
                    }
                    else {
                        StartCoroutine(WaitAndLoadlevel("EndScene"));
                    }

				}
				if (player2Score > player1Score)
				{
                    FindObjectOfType<PersistentData>().winningPlayer = Players.Player2;
                    if (persistentData.WinnerStaysOnMode)
                    {
                        StartCoroutine(WaitAndLoadlevel("WinnerStaysOnMenu"));
                    }
                    else
                    {
                        StartCoroutine(WaitAndLoadlevel("EndScene"));
                    }
                }
                else if (player1Score == player2Score)
                {
                    FindObjectOfType<PersistentData>().winningPlayer = Players.Draw;
                    if (persistentData.WinnerStaysOnMode)
                    {
                        StartCoroutine(WaitAndLoadlevel("WinnerStaysOnMenu"));
                    }
                    else
                    {
                        StartCoroutine(WaitAndLoadlevel("EndScene"));
                    }
                }
			}

            if (matchTimeLimit <= 10)
            {
                if (!timerSound.isPlaying)
                {
                    timerSound.Play();
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
            obstacles[i].GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int j = 0; j < bullets.Length; j++)
        {
            Destroy(bullets[j]);
        }
        player1.GetComponent<PlayerMovement>().canFire2 = false;
        player1.GetComponent<PlayerMovement>().StopTeleportation();
        player1.GetComponent<LineRenderer>().enabled = false;
        player1.GetComponent<PlayerMovement>().teleportsLeft = player1.GetComponent<PlayerMovement>().maxTeleports;
        player1.GetComponent<PlayerMovement>().getinput = false;
        player2.GetComponent<PlayerMovement>().canFire2 = false;
        player2.GetComponent<PlayerMovement>().StopTeleportation();
        player2.GetComponent<LineRenderer>().enabled = false;
        player2.GetComponent<PlayerMovement>().teleportsLeft = player2.GetComponent<PlayerMovement>().maxTeleports;
        player2.GetComponent<PlayerMovement>().getinput = false;
    }
    private void StartMatch() {
        player1.GetComponent<PlayerMovement>().enabled = true;
        player2.GetComponent<PlayerMovement>().enabled = true;
        countDownText.SetActive(false);
        matchStarted = true;
    }

    private IEnumerator WaitAndLoadlevel(string level)
    {
        timerSound.PlayOneShot(airHornSound);
        yield return new WaitForSeconds(airHornSound.length);
        SceneManager.LoadScene(level);
    }
}
