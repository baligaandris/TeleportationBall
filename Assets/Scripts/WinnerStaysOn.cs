using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerStaysOn : MonoBehaviour {

    PersistentData persistentData;
    public Text winnerText, 
        championText, 
        streak,
        streakLabel;

    public string[] levels;


	// Use this for initialization
	void Start () {
        
        persistentData = FindObjectOfType<PersistentData>();
        if (persistentData.firstMatch)
        {
            winnerText.enabled = false;
            persistentData.firstMatch = false;
        }
        else {
            winnerText.enabled = true;

        }
        if (persistentData.winningPlayer == Players.Draw && persistentData.champion == Players.Draw)
        {
            championText.text = "No Champion Yet!";
            winnerText.text = "Draw!!";
            streakLabel.enabled = false;
            streak.enabled = false;
        }
        else if (persistentData.winningPlayer != persistentData.champion && persistentData.winningPlayer != Players.Draw)
        {
            championText.text = "New Champion: " + persistentData.winningPlayer.ToString();
            persistentData.champion = persistentData.winningPlayer; 
            winnerText.text = "Winner: " + persistentData.winningPlayer.ToString();
            streakLabel.enabled = true;
            persistentData.streak = 1;
            streak.text = persistentData.streak.ToString();
        }
        else if (persistentData.winningPlayer == persistentData.champion)
        {
            championText.text = "The Champion is still " + persistentData.winningPlayer.ToString();
            winnerText.text = "Winner: " + persistentData.winningPlayer.ToString();
            streakLabel.enabled = true;
            persistentData.streak++;
            streak.text = persistentData.streak.ToString();
        }
        else if (persistentData.winningPlayer == Players.Draw && persistentData.champion != Players.Draw)
        {
            championText.text = "The Champion is still " + persistentData.winningPlayer.ToString();
            winnerText.text = "Draw!!";
            streakLabel.enabled = true;
            streak.text = persistentData.streak.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadNextLevel() {
        persistentData.nextLevel++;
        if (persistentData.nextLevel >= levels.Length)
        {
            persistentData.nextLevel = 0;
        }
        SceneManager.LoadScene(levels[persistentData.nextLevel]);

    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
