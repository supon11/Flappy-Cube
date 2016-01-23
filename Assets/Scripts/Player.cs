using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[Header("Movement of the player")]
	[Tooltip("Jump height of the player")]
	public float jumpHeight;
	[Tooltip("Forward speed of the player")]
	public float forwardSpeed;
	
	private Rigidbody2D mainRigidbody2D;
	private float maxWidth; //Max width of screen for player move

	void Start()
	{
		mainRigidbody2D = GetComponent<Rigidbody2D> ();
		mainRigidbody2D.isKinematic = true; //Player not fall when in PREGAME states

		//Transforms position from screen space into world space.
		Vector2 targetWidth = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height)); 
		float playerWidth = GetComponent<Renderer>().bounds.extents.y; //Get brick width
		maxWidth = targetWidth.y + playerWidth; //Calculate max width
	}
	
	// Update is called once per frame
	void Update () {

		//If Player go out off screen
		if ((transform.position.y > maxWidth || transform.position.y < -maxWidth) && GameManager.instance.currentState == GameStates.INGAME) {
			GameManager.instance.GameOver(); //Call game over
		}


		//When click or touch and in INGAME states
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && GameManager.instance.currentState == GameStates.INGAME){
			Jump();
		}

		//When click or touch and in PREGAME states
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && GameManager.instance.currentState == GameStates.PREGAME){
			mainRigidbody2D.isKinematic = false; //Player can fall
			GameManager.instance.StartGame(); //Start game
		}

	}
	
	void Jump(){
		mainRigidbody2D.velocity = new Vector2(forwardSpeed,jumpHeight);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Obstacle" && GameManager.instance.currentState == GameStates.INGAME) {
			mainRigidbody2D.freezeRotation = false; //Unfreeze rotation
			GameManager.instance.GameOver ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Score" && GameManager.instance.currentState == GameStates.INGAME) {
			GameManager.instance.addScore(); //Add score
			ObstacleSpawner.instance.SpawnObstacle(); //Spawn new obstacle
			Destroy(other.gameObject); //Destroy score checker
		}
	}
}
