using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    public GameObject countDownText;
	public GameObject matchTimerText;
    bool matchStarted = false;
    float countDownToMatch = 4;
	float matchTimeLimit = 10;
    GameObject[] obstacles;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        ResetAfterGoal();
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
			matchTimerText.GetComponent<Text> ().text = Mathf.Floor (matchTimeLimit).ToString ();
			// Determine which player has highest score when time equals 0
			if (matchTimeLimit <= 0) 
			{
				if (GetComponent<UIManager> ().scoreText.text.Length > GetComponent<UIManager2> ().scoreText.text.Length) 
				{
					Application.LoadLevel ("Player1Win");
				}
				if (GetComponent<UIManager2> ().scoreText.text.Length > GetComponent<UIManager> ().scoreText.text.Length) 
				{
					Application.LoadLevel ("Player2Win");
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
