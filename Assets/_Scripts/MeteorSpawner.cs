using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {
	public GameObject[] meteors;
	public int level = 0;
	public float spawnRate = 4;
	private float timeSinceLastSpawn_ = 0;
	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnRocks());
	}

	IEnumerator SpawnRocks() {
		while (true) {
			timeSinceLastSpawn_ += Time.deltaTime;
			if (timeSinceLastSpawn_ > spawnRate) {
				timeSinceLastSpawn_ = 0;
				Instantiate(meteors[level], transform.position, Quaternion.identity);
			}
			yield return null;
		}
	}
}
