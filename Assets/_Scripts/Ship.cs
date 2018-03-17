using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	/* MOTION */
	protected enum State { LEFT, CENTER, RIGHT, MOVING }
	private State state_;
	private State destination_;
	public KeyCode LeftKey;
	public KeyCode RightKey;
	
	/* FIRING */
	public int power;
	public int range;
	public float maxCooldown;
	public GameObject laser;
	public LayerMask enemyMask;
	private bool onCooldown = false;
	private float currentCooldown_ = 0;
	/* EFFECTS */
	private SpriteRenderer exhaust_;

	// Use this for initialization
	virtual protected void Start () {
		exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>();
		exhaust_.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Input L/R checking 
		/* Movement state machine */
		switch (state_) {
			case State.LEFT:
				if (Input.GetKeyDown(RightKey)) { Move(State.CENTER); }
				break;
			case State.CENTER:
				if (Input.GetKeyDown(RightKey)) { }
			break;
			case State.RIGHT:
			break;
			case State.MOVING:
			break;
			default:
			break;
		}

		/* Firing code */
		if (onCooldown) {
			currentCooldown_ += Time.deltaTime;
			if (currentCooldown_ >= maxCooldown) {
				onCooldown = false;
			}
			return;
		}
		// Check for target in range
		var scan_hit = Physics2D.Raycast(transform.position, Vector2.up, range, enemyMask);
		if (scan_hit.collider != null) {
			var laserShot = Instantiate(laser, transform.position, Quaternion.identity);
			laserShot.transform.localScale *= power;
			onCooldown = true;
			currentCooldown_ = 0;
		}
	}

	// Turns on engine
	protected void EngineOn() {
		if (!exhaust_) { exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>(); }
		exhaust_.enabled = true;
	}

	// Move ship to target state. AttackShip overrides this to implement delay and queue
	virtual protected void Move(State destination) {

	}

	// Fat interface for input behavior when state is MOVING
	virtual protected void CommandWhileMoving() {}

	// Protected accessors
	protected State GetState() { return state_; }
	protected State GetDest() { return destination_; }
}
