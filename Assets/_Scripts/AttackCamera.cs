using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCamera : MonoBehaviour {
	public GameObject ShipObject;
	public float CameraOffsetMax;
	private float offset = 0; 
	private float t_ = 0.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(LerpCameraOffset());
	}
	
	// Update is called once per frame
	void Update () {
		if (ShipObject) {
			// align camera with attack ship position
			float ship_camera_pos = ShipObject.transform.position.y + offset;
			if (transform.position.y != ship_camera_pos) {
				transform.position = new Vector3(transform.position.x, ship_camera_pos, transform.position.z);
			}
		}
	}

	IEnumerator LerpCameraOffset() {
		while (offset < CameraOffsetMax) {
			offset = Mathf.Lerp(0, CameraOffsetMax, t_);
			t_ += 0.3f * Time.deltaTime;
			yield return null;
		}
		offset = CameraOffsetMax;
	}
}
