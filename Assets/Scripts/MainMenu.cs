using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    PersistentData persistentData;

	public void LoadLevelSelecter()
	{
        SceneManager.LoadScene("LevelSelecter");
	}

    public void LoadWinnerStaysMenu() {
        persistentData.WinnerStaysOnMode = true;
        SceneManager.LoadScene("WinnerStaysOnMenu");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
	{
		Application.Quit ();
	}


	// Use this for initialization
	void Start () {
		if (!FindObjectOfType<PersistentData>())
        {
            GameObject go = new GameObject("PData");
            go.AddComponent<PersistentData>();
            persistentData = go.GetComponent<PersistentData>();
        }
        else
        {
            persistentData = FindObjectOfType<PersistentData>();
        }
        persistentData.WinnerStaysOnMode = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetNameOfPlayer1(string name)
    {
        persistentData.player1Name = name;
    }
    public void SetNameOfPlayer2(string name)
    {
        persistentData.player2Name = name;
    }
}
