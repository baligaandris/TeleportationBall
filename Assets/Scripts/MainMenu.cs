using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    string nameText = "";

	public void LoadMainLevel()
	{
		Application.LoadLevel ("Scene1");
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
        }
        // Store player names (do this for every scene except for end scene where a name change will take place depending on match result)
        GameObject gameData = GameObject.Find("PersistentData");
        if (gameData != null)
        {
            PersistentData persistentData = gameData.GetComponent<PersistentData>();
            nameText = persistentData.player1Name;
            nameText = persistentData.player2Name;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetName(string name)
    {
        nameText = name;
    }
}
