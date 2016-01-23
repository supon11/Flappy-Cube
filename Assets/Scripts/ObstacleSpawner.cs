using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public static ObstacleSpawner instance; //Hold a reference to this script

	[Header("Obstacle")]
	public GameObject obstacle; //GameObject of obstacle

	[Header("Spawn Position")]
	public float RangeYPosition; //Range of y position for spawn
	public float lastedXPosition; //X position for spawn
	public float distanceXPosition; //Distance between obstacle

	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		for(int i = 0 ; i < 3 ; i++){
			SpawnObstacle(); //Begin with 3 obstacle
		}
	}
	
	public void SpawnObstacle(){
		float yPosition = Random.Range(-RangeYPosition, RangeYPosition); //Random y position
		float xPosition = lastedXPosition += distanceXPosition; //Calculate x position
		Vector3 spawnPosition = new Vector3 (xPosition, yPosition, 0.0f); //Position for spawn
		Instantiate (obstacle, spawnPosition , Quaternion.identity); //Create obstacle to game
	}
}
