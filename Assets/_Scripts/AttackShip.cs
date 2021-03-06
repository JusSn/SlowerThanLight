﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : Ship {
	/* MOTION */
	
	/* EFFECTS */
	public float gameplayPosition;
	public float bossFightPosition;
	private float startPositionY_;
	private float kExhaustScale_ = 1.5f;
	// Use this for initialization
	override protected void Start() {
		base.Start();
		startPositionY_ = transform.position.y;
		StartCoroutine(Embark());
	}

	// Attack ship delays command by [level] seconds
	override protected void AddCommand(State dest) {
		StartCoroutine(DelayedCommand(dest));
	}
	// Attack ship alters exhaust size rather than turning it on/off
	override protected void IncreaseThrust() { base.GetExhaustRenderer().transform.localScale *= kExhaustScale_; }
	override protected void DecreaseThrust() { base.GetExhaustRenderer().transform.localScale /= kExhaustScale_;}
	// Attack ship laser increases damage with level
	override protected int GetLaserDamage() { return Manager.instance.level + 1; }

	public void ArriveAtBoss() {
		StartCoroutine(Arrive());
	}
	
	/* COROUTINES */
	// Move AttackShip to gameplay position
	IEnumerator Embark() {
		// Prevent input
		SetState(State.PAUSED);
		// Fire Engines
	 	EngineOn();
		yield return LerpToYPos(gameplayPosition);
		// Enable controls and start level counter
		SetState(State.CENTER);
		Manager.instance.enabled = true;
	}

	// Move AttackShip to boss fight position
	IEnumerator Arrive() {
		var old_state = GetState();
		SetState(State.PAUSED);
		IncreaseThrust();

		yield return LerpToYPos(bossFightPosition);
		SetState(old_state);
	}

	IEnumerator LerpToYPos(float y) {
		startPositionY_ = transform.position.y;
		float t = 0f;
		while (transform.position.y < y) {
			float y_temp = Mathf.Lerp(startPositionY_, y, t);
			t += 0.4f * Time.deltaTime;

			UpdateYPosition(y_temp);
			yield return null;
		}
		UpdateYPosition(y);
	}

	// Execute command after level seconds
	IEnumerator DelayedCommand(State dest) {
		yield return new WaitForSecondsRealtime(Manager.instance.level * 0.5f);
		base.AddCommand(dest);
	}
	
	void UpdateYPosition(float y) {
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
