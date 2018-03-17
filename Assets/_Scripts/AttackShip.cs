using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : MonoBehaviour {
	public float GameplayPosition;
	private float StartPosition_;
	private float t_ = 0.0f;
	private SpriteRenderer Exhaust_;
	// Use this for initialization
	void Start () {
		StartPosition_ = transform.position.y;
		Exhaust_ = transform.Find("ExhaustSprite").GetComponent<SpriteRenderer>();
		Exhaust_.enabled = false;
		StartCoroutine(Embark());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Move AttackShip to gameplay position
	IEnumerator Embark() {
		// Fire Engines
		Exhaust_.enabled = true;
		while (transform.position.y < GameplayPosition) {
			float y_temp = Mathf.Lerp(StartPosition_, GameplayPosition, t_);
			t_ += 0.4f * Time.deltaTime;

			UpdateYPosition(y_temp);
			yield return null;
		}
		UpdateYPosition(GameplayPosition);
	}
	
	void UpdateYPosition(float y) {
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
