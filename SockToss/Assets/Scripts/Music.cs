using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    private AudioSource music;

	// Use this for initialization
	void Start () {
        Debug.Log("Loaded music");
        music = GetComponent<AudioSource>();
        music.Play();
    }
}
