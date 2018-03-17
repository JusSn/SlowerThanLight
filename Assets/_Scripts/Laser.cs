using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	public float speed;
	public float maxRange;
	private float startY_;

	// Use this for initialization
	void Start () { 
		startY_ = transform.position.y;
		GetComponent<Explodable>().explosionScale = 0.3f * transform.localScale.x; 
	}
	
	// Update is called once per frame
	void Update () {
		// Move towards enemy 
		transform.position = 
			new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);

		// Delete after traveling out of range
		if (transform.position.y - startY_ >= maxRange) { Destroy(this.gameObject); }
	}
}
