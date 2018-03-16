using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players { Player1, Player2, Draw };
public class PersistentData : MonoBehaviour {

    #region member variables

    public Players m_winningPlayer = Players.Player1;

    #endregion

    void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
