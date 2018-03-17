using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : Ship {
	public float GameplayPosition;
	private float StartPosition_;
	private float t_ = 0.0f;
	// Use this for initialization
	override protected void Start() {
		base.Start();
		StartPosition_ = transform.position.y;
		StartCoroutine(Embark());
	}
	
	// Move AttackShip to gameplay position
	IEnumerator Embark() {
		// Fire Engines
	 	EngineOn();
		while (transform.position.y < GameplayPosition) {
			float y_temp = Mathf.Lerp(StartPosition_, GameplayPosition, t_);
			t_ += 0.4f * Time.deltaTime;

			UpdateYPosition(y_temp);
			yield return null;
		}
		UpdateYPosition(GameplayPosition);
	}
	
	void UpdateYPosition(float y) {
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
