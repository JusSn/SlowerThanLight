using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour {
	public float maxSpeed;
	public float minSpeed;
	private SpriteRenderer renderer_;
	private float rotateSpeed_;
	// Use this for initialization
	void Start () {
		renderer_ = GetComponentInChildren<SpriteRenderer>();
		rotateSpeed_ = Random.Range(minSpeed, maxSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		renderer_.transform.Rotate(Vector3.back * rotateSpeed_ * Time.deltaTime);
	}
}
