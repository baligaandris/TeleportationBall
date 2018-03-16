using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1Win : MonoBehaviour {

    private void Start()
    {
        Text txt = GameObject.Find("DisplayText").GetComponent<Text>();

        switch(FindObjectOfType<PersistentData>().m_winningPlayer)
        {
            case Players.Draw:
                txt.text = "Draw!";
                break;

            case Players.Player1:
                txt.text = "Player 1 won";
                break;

            case Players.Player2:
                txt.text = "Player 2 won";
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
