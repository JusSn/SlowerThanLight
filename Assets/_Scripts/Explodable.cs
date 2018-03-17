using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour {
	public int health;
	// Default damage dealt to objects that this collides with 
	public int damage = 1;
	public GameObject explosion;
	public float explosionScale = 1;
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
			var explodable = other.gameObject.GetComponent<Explodable>();
			if (explodable) { 
				TakeDamage(explodable.damage); 
			}
			else { 
				TakeDamage(1);
			}
		}
	}

	void TakeDamage(int dmg) {
		health -= dmg; 
		if (health <= 0) { Explode(); }
	}

	virtual protected void Explode() {
		var explosion_copy = Instantiate(explosion, transform.position, Quaternion.identity);
		explosion_copy.transform.localScale = new Vector3(explosionScale, explosionScale, 1);
		Destroy(this.gameObject);
	}
}
