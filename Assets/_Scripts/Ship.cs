using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	/* MOTION */
	public enum State { LEFT, CENTER, RIGHT, MOVING }
	public State state_ = State.CENTER;
	public State destination_;
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
		/* Movement state machine */
		switch (state_) {
			case State.LEFT:
				if (Input.GetKeyDown(RightKey)) { 
					destination_ = State.CENTER;
					Move(GameData.kCenterLaneX); 
				}
				break;
			case State.CENTER:
				if (Input.GetKeyDown(RightKey)) { 
					destination_ = State.RIGHT;
					Move(GameData.kRightLaneX); 
				}
				if (Input.GetKeyDown(LeftKey)) { 
					destination_ = State.LEFT;
					Move(GameData.kLeftLaneX); 
				}
				break;
			case State.RIGHT:
				if (Input.GetKeyDown(LeftKey)) { 
					destination_ = State.CENTER;
					Move(GameData.kCenterLaneX); 
				}
				break;
			case State.MOVING:
				CommandWhileMoving();
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

	// Move ship to target lane denoted by lane_x. AttackShip overrides this to implement delay and queue
	virtual protected void Move(float lane_x) {
		state_ = State.MOVING;
		StartCoroutine(MoveAnimation(new Vector3(lane_x, transform.position.y, transform.position.z)));
	}

	// Fat interface for input behavior when state is MOVING
	virtual protected void CommandWhileMoving() {}

	// Protected accessors
	protected State GetState() { return state_; }
	protected State GetDest() { return destination_; }

	// Coroutines
	IEnumerator MoveAnimation(Vector3 dest_vector) {
		var orig_pos = transform.position;
		float t = 0.0f;
		while (transform.position != dest_vector) {
			Debug.Log(t);
			transform.position = Vector3.Lerp(orig_pos, dest_vector, t);
			t += Time.deltaTime;
			yield return null;
		}
		// Done moving
		state_ = destination_;
	}
}
