using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {
    private AudioSource music;

	// Use this for initialization
	void Start () {
        music = GetComponent<AudioSource>();
        music.Play(0);
	}
}
