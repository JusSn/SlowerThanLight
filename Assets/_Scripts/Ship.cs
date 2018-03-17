using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	// ships fire on enemies in their lane
	public int power;
	public int range;
	public float maxCooldown;
	private float currentCooldown_ = 0;
	public GameObject laser;
	private SpriteRenderer exhaust_;
	
	public LayerMask enemyMask;
	private bool onCooldown = false;

	// Use this for initialization
	virtual protected void Start () {
		exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>();
		exhaust_.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
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

	protected void EngineOn() {
		if (!exhaust_) { exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>(); }
		exhaust_.enabled = true;
	}
}
