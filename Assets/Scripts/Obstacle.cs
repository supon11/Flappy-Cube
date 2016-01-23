using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position); //Screen position
		if (screenPosition.x < -1) //If obstacle out off camera (use -1 instead 0 for make more space to destroy)
			Destroy(gameObject); //Destroy obstacle

		if (GameManager.instance.currentState == GameStates.GAMEOVER) {
			Destroy(gameObject , 1.5f);
		}
		
	}
}
