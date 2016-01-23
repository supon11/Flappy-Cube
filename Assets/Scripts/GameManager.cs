using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameStates{
	PREGAME, //State before play
	INGAME, //State when play and player alive
	GAMEOVER ,//State when player dead
}


public class GameManager : MonoBehaviour {
	
	private int score;//For count score
	
	[Header("Text")]
	public Text scoreText; //For show score when playing
	public Text gameoverScoreText; //For show score when gameover
	public Text gameoverBestScoreText; //For show highscore when gameover
	public Text gamesPlayedText; //For show games played

	[Header("Canvas")]
	public Canvas pregameCanvas; //For show Canvas of pregame
	public Canvas gameoverCanvas; //For show Canvas of gameover
	public Canvas ingameCanvas;  //For show Canvas when play (pause button ,scoreText)

	[HideInInspector]
	public GameStates currentState; //State of game
	public static GameManager instance; //Instance
	
	void Awake () {
		instance = this;
		currentState = GameStates.PREGAME; //Begin with PREGAME state
	}

	public void addScore(){
		score++; //Plus score
		scoreText.text = score.ToString (); //Change scoreText to current score
	}

	public void StartGame(){
		currentState = GameStates.INGAME; //Change state to INGAME
		pregameCanvas.gameObject.SetActive (false); //Hide pregameCanvas
		ingameCanvas.gameObject.SetActive (true); //Show ingameCanvas
		PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt ("gamesPlayed", 0) + 1 ); //Save a number of playing
	}
	
	public void GameOver(){
		currentState = GameStates.GAMEOVER; //Change state to GAMEOVER

		CheckHighScore ();  //Call CheckHighScore function
		gamesPlayedText.text += PlayerPrefs.GetInt ("gamesPlayed", 0); //Set gamesPlayed
		gameoverScoreText.text = score.ToString(); //Set gameover scoreText
		gameoverBestScoreText.text += PlayerPrefs.GetInt ("bestscore", 0); //Set best score text
		StartCoroutine (ShowGameoverCanvas ()); //Show Gameover Canvas
	}
	
	IEnumerator ShowGameoverCanvas(){
		yield return new WaitForSeconds (1.5f); //Wait 1.5 second
		gameoverCanvas.gameObject.SetActive (true); //Show gameover's Canvas
		ingameCanvas.gameObject.SetActive (false); //Hide ingameCanvas
	}
	
	public void CheckHighScore(){
		if( score > PlayerPrefs.GetInt("bestscore",0) ){ //If score > last best score
			PlayerPrefs.SetInt("bestscore",score); //Save a new best score
		}
	}

	public void RestartGame(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
