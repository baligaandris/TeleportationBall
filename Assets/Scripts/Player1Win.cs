using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1Win : MonoBehaviour {
    string nameText = "";
    public GameObject player1Input;
    public GameObject player2Input;

    private void Start()
    {
        Text txt = GameObject.Find("DisplayText").GetComponent<Text>();
        // Generate name input for losing player
        GameObject gameData = GameObject.Find("PersistentData");
        if (gameData != null)
        {
            PersistentData persistentData = gameData.GetComponent<PersistentData>();
            nameText = persistentData.player1Name;
            nameText = persistentData.player2Name;
        }

        switch (FindObjectOfType<PersistentData>().winningPlayer)
        {
            case Players.Draw:
                txt.text = "Draw!";
                break;

            case Players.Player1:
                txt.text = "Player 1 won";
                player2Input.SetActive(true);
                break;

            case Players.Player2:
                txt.text = "Player 2 won";
                player1Input.SetActive(true);
                break;
        }
    }

    public void RestartMatch()
	{
        SceneManager.LoadScene("Scene1");
		
	}
	public void ReturnToMenu()
	{
        SceneManager.LoadScene("MainMenu");
	}
}
