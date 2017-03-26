using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_ground : MonoBehaviour {
    private AudioSource hit_ground_sound;
	// Use this for initialization
	void Start () {
        hit_ground_sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            hit_ground_sound.Play();
        }
    }
}
