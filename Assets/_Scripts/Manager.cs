using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton manager decides current level, etc.
public class Manager : MonoBehaviour {
	// Static constants and variables shared by multiple classes
	static public float kRightLaneX = 2.5f;
	static public float kCenterLaneX = 0;
	static public float kLeftLaneX = -2.5f;
	// Singleton
	static public Manager instance;
	// current game level dictates meteor spawn types and attack ship power
	public int level = 0;
	private float kTimeBetweenLevels = 15;
	public MeteorSpawner spawner;
	public AttackShip ship;

	// Use this for initialization
	void Start () {
		instance = this;
		enabled = false;
		StartCoroutine(LevelProgression());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator LevelProgression() {
		// Level up every few seconds
		while (level < 3) {
			yield return new WaitForSecondsRealtime(kTimeBetweenLevels);
			++level;
		}
		// Level now 3; wait one last time
		yield return new WaitForSecondsRealtime(kTimeBetweenLevels);
		// Disable rock spawns for a few sec before moving attack ship
		spawner.enabled = false;
		yield return new WaitForSecondsRealtime(5);
		// Engage boss level by moving attack ship in range
		ship.ArriveAtBoss();
		// Reenable spawner after a few sec
		yield return new WaitForSecondsRealtime(5);
		spawner.enabled = true;
	}
}
