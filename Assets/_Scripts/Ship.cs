using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	/* MOTION */
	protected enum State { LEFT, CENTER, RIGHT, MOVING }
	[SerializeField]
	private State state_ = State.CENTER;
	[SerializeField]
	private State destState_;
	private Vector3 destPos_;
	private Vector3 origPos_;
	private float t = 0;
	public KeyCode LeftKey;
	public KeyCode CenterKey;
	public KeyCode RightKey;
	
	/* FIRING */
	public int range;
	public float maxCooldown;
	public GameObject laser;
	public LayerMask enemyMask;
	private bool onCooldown_ = false;
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
		/* Movement state machine */
		if (Input.GetKeyDown(LeftKey)) { AddCommand(State.LEFT); }
		if (Input.GetKeyDown(CenterKey)) { AddCommand(State.CENTER); }
		if (Input.GetKeyDown(RightKey)) { AddCommand(State.RIGHT); }

		if (state_ == State.MOVING) {
			if (transform.position != destPos_) {
				transform.position = Vector3.Lerp(origPos_, destPos_, t);
				t += 2 * Time.deltaTime;
			}
			else {
				// Done moving
				state_ = destState_;
				DecreaseThrust();
			}
		}

		/* Firing code */
		if (onCooldown_) {
			currentCooldown_ += Time.deltaTime;
			if (currentCooldown_ >= maxCooldown) {
				onCooldown_ = false;
			}
			return;
		}
		// Check for target in range
		var scan_hit = Physics2D.Raycast(transform.position, Vector2.up, range, enemyMask);
		if (scan_hit.collider != null) {
			var laser_shot = Instantiate(laser, transform.position, Quaternion.identity);
			laser_shot.GetComponent<Explodable>().damage = GetLaserDamage();
			laser_shot.transform.localScale *= GetLaserDamage();
			onCooldown_ = true;
			currentCooldown_ = 0;
		}
	}

	// Turns on engine
	protected void EngineOn() {
		if (!exhaust_) { exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>(); }
		exhaust_.enabled = true;
	}
	void EngineOff() { exhaust_.enabled = false; }

	// Move ship to target lane denoted by lane_x. AttackShip overrides this to implement delay and queue
	virtual protected void Move(State next_dest) {
		if (state_ != State.MOVING) { 
			IncreaseThrust(); 
			state_ = State.MOVING;
		}

		float lane_x = 0;
		switch (next_dest) {
			case State.LEFT:
				lane_x = Manager.kLeftLaneX;
				break;
			case State.CENTER:
				lane_x = Manager.kCenterLaneX;
				break;
			case State.RIGHT:
				lane_x = Manager.kRightLaneX;
				break;
		}
		// Reset lerp function
		t = 0;
		origPos_ = transform.position;
		destState_ = next_dest;
		destPos_ = new Vector3(lane_x, transform.position.y, transform.position.z);
	}

	// Add a command to the queue, taking final destination of the current queue into account
	// Base ship just executes the command immediately
	virtual protected void AddCommand(State dest) {
		if (dest != destState_) { Move(dest); }
	}
	// Turn up thrust when moving
	virtual protected void IncreaseThrust() { EngineOn(); }
	// Turn down thrust when stopped
	virtual protected void DecreaseThrust() { EngineOff(); }
	// Laser power for this ship depends on type
	virtual protected int GetLaserDamage() { return 1; }

	// Protected accessors
	protected State GetState() { return state_; }
	protected void SetState(State s) { state_ = s; }
	protected Vector3 GetDest() { return destPos_; }
	protected SpriteRenderer GetExhaustRenderer() { return exhaust_; }
}
