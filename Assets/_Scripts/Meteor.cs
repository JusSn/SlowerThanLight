using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	public float speed;
	private float next_y_;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		next_y_ = Time.deltaTime * speed;
		// Move towards home planet
		transform.position = 
			new Vector3(transform.position.x, transform.position.y - next_y_, transform.position.z);
		// TODO: trigger pause for (level) seconds when entering waiting area
	}
}
