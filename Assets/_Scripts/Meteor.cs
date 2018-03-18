using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	public float speed;
	private float nextY_;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		nextY_ = Time.deltaTime * speed;
		// Move towards home planet
		transform.position = 
			new Vector3(transform.position.x, transform.position.y - nextY_, transform.position.z);
	}

	public void Freeze() { StartCoroutine(FreezeMotion()); }

	IEnumerator FreezeMotion() {
		var old_speed = speed;
		speed = 0;
		// Delay rocks by [level] seconds
		yield return new WaitForSecondsRealtime(Manager.instance.level);
		speed = old_speed - 0.5f;
	}
}
