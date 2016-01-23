using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Transform player;		//target for the camera to follow
	public float xPositionOffset;
	private float targetXPosition; 	//Position of the player
	private float lastedXPosition;	//lasted X Position of the camera

	void Start(){
		lastedXPosition = transform.position.x; // lasted X Position of camera
	}

	void Update()
	{
		if(GameManager.instance.currentState == GameStates.GAMEOVER){
			return;
		}


		targetXPosition = player.transform.position.x - xPositionOffset; //current x position of player

		//If camera position > lasted of player position
		if (targetXPosition > lastedXPosition) {
			lastedXPosition = targetXPosition;
		}

		//Move camera to lasted X Position
		transform.position = new Vector3 (lastedXPosition, transform.position.y , transform.position.z); 
	}
}
