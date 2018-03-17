using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour {
	public int health;
	// Default damage dealt to objects that this collides with 
	public int damage = 1;
	public GameObject explosion;
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (tag == "MISSILE") { 
			Explode(); 
		}
		else {
			TakeDamage(other.gameObject.GetComponent<Explodable>().damage);
		}
	}

	void TakeDamage(int dmg) {
		health -= dmg; 
		if (health <= 0) { Explode(); }
	}

	void Explode() {
		var death_explosion = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(this);
	}
}
