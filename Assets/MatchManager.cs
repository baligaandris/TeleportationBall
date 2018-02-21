using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    public GameObject countDownText;
    bool matchStarted = false;
    float countDownToMatch = 4;
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
        if (matchStarted == false)
        {
            countDownToMatch -= Time.deltaTime;
            countDownText.GetComponent<Text>().text = Mathf.Floor(countDownToMatch).ToString();
            if (countDownToMatch<=0)
            {
                StartMatch();
                
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
