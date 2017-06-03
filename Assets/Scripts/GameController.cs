using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool isGameOver = true;
	public bool played = false;
	public Text scoreTxt;
	public Text VRScoreTxt;
    public Text gameOverTxt;
	public Canvas gameOverCanvas;
	public Canvas VRGameOverCanvas;
	public Canvas infoCanvas;
	public Text VRGameOverTxt;
	public Text VRTextBtn;
	public Text exit;
	public Button exitBtn;
	public Button exitYes;
	public Button exitNo;
	public Button infoPlay;
    public Canvas htpCanvas;

    private playerControl _playerControl;


	private int _currScore;
    public int bossLife = 2;
    private int _scoreToWin = 20;
	private bool _didIWin;

	/// <summary>
	/// Start a new game.
	/// </summary>
	void NewGame() {
		ResetGame();
	}

	public void Quit() {
		exitBtn.enabled = false;
		exitYes.enabled = true;
		exitNo.enabled = true;
		exitYes.gameObject.SetActive(true);
		exitNo.gameObject.SetActive(true);
//		float value = 0;
//		exitBtn.transform.position.z = value;
//		exitYes.transform.position.z = value;
	}
	public void QuitReset() {
		exitBtn.enabled = true;
		exitYes.enabled = false;
		exitNo.enabled = false;
		exitYes.gameObject.SetActive(false);
		exitNo.gameObject.SetActive(false);
//		float value = 0.1;
//		exitBtn.transform.position.z = value;
//		exitYes.transform.position.z = value;
	}

	public void QuitGame() {
		Application.Quit();
	}
	/// <summary>
	/// Got an enemy! Increment the score and see if we win.
	/// </summary>
	public void GotOne() {
		_currScore = _currScore + 1;
		scoreTxt.text =  "" + _currScore;
		VRScoreTxt.text = "" + _currScore;
		if (_currScore >= _scoreToWin) {
			GameOver(true);
		}
	}
    public void GotOneBoss()
    {
        _currScore = _currScore + 2;
        scoreTxt.text = "" + _currScore;
        VRScoreTxt.text = "" + _currScore;
        if (_currScore >= _scoreToWin)
        {
            GameOver(true);
        }
    }

    /// <summary>
    /// Game's over. 
    /// </summary>
    /// <param name="didIWin">Did the playeer win?</param>	
    public void GameOver(bool didIWin) {
    isGameOver = true;
    _didIWin = didIWin;
	infoCanvas.enabled = false;
    htpCanvas.enabled = false;
		VRScoreTxt.enabled = true;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy(enemy);
		}

		GameObject[] boss = GameObject.FindGameObjectsWithTag("Mini Boss");
		foreach (GameObject enemy in boss)
		{
			Destroy(enemy);
		}

    string finalTxt = (_didIWin) ? "You won!" : "Game Over";
		finalTxt = finalTxt + "\n Your Score is " + _currScore;
    if (GvrViewer.Instance.VRModeEnabled) {
    	Debug.Log("Game is over. I am showing the VR Canvas");
        VRGameOverCanvas.enabled = true;
		VRGameOverTxt.text = (played) ? "" : "SAVE THE LAND";
		//VRGameOverTxt.fontSize = (played) ? 30 : 36;
		VRTextBtn.text = (played) ? "Play Again" : "Let's Start";
		VRScoreTxt.text = (played) ? finalTxt : "";
    } else {
        gameOverCanvas.enabled = true;
		gameOverTxt.text = (played) ? finalTxt : "Save The Land";
    }
	}
	
	public void RefreshGameOver() {
    gameOverCanvas.enabled = false;
    VRGameOverCanvas.enabled = false;
	infoCanvas.enabled = false;
    htpCanvas.enabled = false;
	exitYes.enabled = false;
	exitNo.enabled = false;
    if (isGameOver) {
        GameOver(_didIWin);
    }
	}

	public void infoGame(){
		infoCanvas.enabled = true;
		VRGameOverCanvas.enabled = false;
		VRScoreTxt.enabled = false;
	}

    public void htpGame()
    {
        htpCanvas.enabled = true;
        VRGameOverCanvas.enabled = false;
		VRScoreTxt.enabled = false;
    }
	/// <summary>
	/// Resets the interface, removes remaining game objects. Basically gets us to a point
	/// where we're ready to play again.
	/// </summary>
	public void ResetGame() {
		// Reset the interface
		VRGameOverCanvas.enabled = false;
		gameOverCanvas.enabled = false;
		isGameOver = false;
		_currScore = 0;
		scoreTxt.text = "--";
		VRScoreTxt.text = "--";

        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
        	Destroy(enemy);
        }

        GameObject[] boss = GameObject.FindGameObjectsWithTag("Mini Boss");
        foreach (GameObject enemy in boss)
        {
            Destroy(enemy);
        }

        GameObject[] ninjaStars = GameObject.FindGameObjectsWithTag("NinjaStar");
        foreach (GameObject ninjaStar in ninjaStars) {
        	Destroy (ninjaStar);
        }

		played = true;
	}
	
	void Start () {
		//NewGame();
		GameOver(true);
		QuitReset ();
	}
	
}
