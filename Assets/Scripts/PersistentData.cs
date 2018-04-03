using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players { Player1, Player2, Draw };
public class PersistentData : MonoBehaviour {

    #region member variables

    public Players m_winningPlayer = Players.Player1;

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
