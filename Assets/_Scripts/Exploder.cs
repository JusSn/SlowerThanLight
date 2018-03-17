using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {
	public Sprite[] frames;
	private SpriteRenderer renderer_;
	private int frame_num_ = 0;
	private float frame_counter_ = 0f;

	// Use this for initialization
	void Start () {
		renderer_ = GetComponent<SpriteRenderer>();
		renderer_.sprite = frames[frame_num_++];
	}
	
	// Update is called once per frame
	void Update () {
		if (frame_num_ < frames.Length) {
			frame_counter_ += Time.deltaTime * 10;
			if (frame_counter_ > 1f) {
				frame_counter_ = 0;
				renderer_.sprite = frames[frame_num_++];
			}
		}
		else { 
			Destroy(this.gameObject);
		}
	}
}
