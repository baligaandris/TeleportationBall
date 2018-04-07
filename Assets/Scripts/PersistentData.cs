using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players { Player1, Player2, Draw };
public class PersistentData : MonoBehaviour {

    #region member variables

    public Players winningPlayer = Players.Draw;
    public Players champion = Players.Draw;
    public int streak = 0;
    public int nextLevel = -1;

    public bool WinnerStaysOnMode = false;
    public bool firstMatch = true;

    #endregion

    public string player1Name = "";
    public string player2Name = "";

    void Start ()
    {
        DontDestroyOnLoad(this);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
