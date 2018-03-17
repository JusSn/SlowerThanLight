using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {
	public GameObject[] meteors;
	public float spawnRate = 4;
	private float timeSinceLastSpawn_ = 0;
	// Use this for initialization
	void Start () {
	}

	void Update() {
		timeSinceLastSpawn_ += Time.deltaTime;
		if (timeSinceLastSpawn_ > spawnRate) {
			timeSinceLastSpawn_ = 0;
			SpawnRandomMeteor();
			
		}
	}

	void SpawnRandomMeteor() {
		// Determine lane
		float spawn_x;
		switch (Random.Range(0, 3)) {
			case 0: 
				spawn_x = Manager.kLeftLaneX;
				break;
			case 1:
				spawn_x = Manager.kCenterLaneX;
				break;
			case 2:
				spawn_x = Manager.kRightLaneX;
				break;
			default:
				spawn_x = 0;
				break;
		}
		Vector3 spawn_pos = new Vector3(spawn_x, transform.position.y, transform.position.z);
		// Maximum possible level depends on game level
		int meteor_level = Random.Range(0, Manager.instance.level + 1);
		Instantiate(meteors[meteor_level], spawn_pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
	}
}
