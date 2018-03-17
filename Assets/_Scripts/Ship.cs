using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	// ships fire on enemies in their lane
	public int power;
	public int range;
	private SpriteRenderer Exhaust_;

	// Use this for initialization
	virtual protected void Start () {
		Exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>();
		Exhaust_.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void EngineOn() {
		if (!Exhaust_) { Exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>(); }
		Exhaust_.enabled = true;
	}
}
