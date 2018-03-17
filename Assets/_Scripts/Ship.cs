using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	/* MOTION */
	protected enum State { LEFT, CENTER, RIGHT, MOVING }
	private State state_ = State.CENTER;
	private State destination_;
	public KeyCode LeftKey;
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
		switch (state_) {
			case State.LEFT:
				if (Input.GetKeyDown(RightKey)) { 
					destination_ = State.CENTER;
					Move(Manager.kCenterLaneX); 
				}
				break;
			case State.CENTER:
				if (Input.GetKeyDown(RightKey)) { 
					destination_ = State.RIGHT;
					Move(Manager.kRightLaneX); 
				}
				if (Input.GetKeyDown(LeftKey)) { 
					destination_ = State.LEFT;
					Move(Manager.kLeftLaneX); 
				}
				break;
			case State.RIGHT:
				if (Input.GetKeyDown(LeftKey)) { 
					destination_ = State.CENTER;
					Move(Manager.kCenterLaneX); 
				}
				break;
			case State.MOVING:
				CommandWhileMoving();
				break;
			default:
				break;
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
	virtual protected void Move(float lane_x) {
		state_ = State.MOVING;
		IncreaseThrust();
		StartCoroutine(MoveAnimation(new Vector3(lane_x, transform.position.y, transform.position.z)));
	}

	// Fat interface for input behavior when state is MOVING
	virtual protected void CommandWhileMoving() {}
	// Turn up thrust when moving
	virtual protected void IncreaseThrust() { EngineOn(); }
	// Turn down thrust when stopped
	virtual protected void DecreaseThrust() { EngineOff(); }
	// Laser power for this ship depends on type
	virtual protected int GetLaserDamage() { return 1; }

	// Protected accessors
	protected State GetState() { return state_; }
	protected void SetState(State s) { state_ = s; }
	protected State GetDest() { return destination_; }
	protected SpriteRenderer GetExhaustRenderer() { return exhaust_; }

	// Coroutines
	IEnumerator MoveAnimation(Vector3 dest_vector) {
		var orig_pos = transform.position;
		float t = 0.0f;
		while (transform.position != dest_vector) {
			transform.position = Vector3.Lerp(orig_pos, dest_vector, t);
			t += 2 * Time.deltaTime;
			yield return null;
		}
		// Done moving
		state_ = destination_;
		DecreaseThrust();
	}
}
