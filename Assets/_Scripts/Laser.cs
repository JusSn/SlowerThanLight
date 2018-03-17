using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	public float speed;
	public float maxRange;
	private float nextY_;

	// Use this for initialization
	void Start () { GetComponent<Explodable>().explosionScale = 0.3f * transform.localScale.x; }
	
	// Update is called once per frame
	void Update () {
		nextY_ = Time.deltaTime * speed;
		// Move towards enemy 
		transform.position = 
			new Vector3(transform.position.x, transform.position.y + nextY_, transform.position.z);

		// Delete after traveling out of range
		if (nextY_ >= maxRange) { Destroy(this.gameObject); }
	}
}
