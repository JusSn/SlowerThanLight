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
		// TODO: trigger pause for (level) seconds when entering waiting area
	}
}
