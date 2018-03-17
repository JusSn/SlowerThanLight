using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCamera : MonoBehaviour {

	public GameObject ship_object_;
	public float camera_offset_;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// align camera with attack ship position
		float ship_camera_pos = ship_object_.transform.position.y + camera_offset_;
		if (transform.position.y != ship_camera_pos) {
			transform.position = new Vector3(transform.position.x, ship_camera_pos, 0);
		}
	}
}
