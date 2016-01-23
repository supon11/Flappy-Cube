using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.currentState == GameStates.GAMEOVER) {
			Destroy(gameObject , 1.5f);
		}
	}
}
