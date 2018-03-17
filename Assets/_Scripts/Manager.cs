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
	private float timeSinceLevel = 0;
	private float kTimeBetweenLevels = 5;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		// Level up every few seconds
		timeSinceLevel += Time.deltaTime;
		if (timeSinceLevel > kTimeBetweenLevels) {
			timeSinceLevel = 0;
			++level;
		}
	}
}
