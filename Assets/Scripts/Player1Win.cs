using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Win : MonoBehaviour {
	public void RestartMatch()
	{
		Application.LoadLevel ("Scene1");
	}
	public void ReturnToMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
