using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : Ship {
	/* MOTION */
	
	/* EFFECTS */
	public float gameplayPosition;
	private float startPosition_;
	private float kExhaustScale_ = 1.5f;
	// Use this for initialization
	override protected void Start() {
		base.Start();
		startPosition_ = transform.position.y;
		StartCoroutine(Embark());
	}

	// Attack ship alters exhaust size rather than turning it on/off
	override protected void IncreaseThrust() { base.GetExhaustRenderer().transform.localScale *= kExhaustScale_; }
	override protected void DecreaseThrust() { base.GetExhaustRenderer().transform.localScale /= kExhaustScale_;}
	
	// Move AttackShip to gameplay position
	private IEnumerator Embark() {
		// Fire Engines
	 	EngineOn();
		float t = 0f;
		while (transform.position.y < gameplayPosition) {
			float y_temp = Mathf.Lerp(startPosition_, gameplayPosition, t);
			t += 0.4f * Time.deltaTime;

			UpdateYPosition(y_temp);
			yield return null;
		}
		UpdateYPosition(gameplayPosition);
	}
	
	private void UpdateYPosition(float y) {
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
