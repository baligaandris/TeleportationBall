using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Win : MonoBehaviour {

	public void Rematch()
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
