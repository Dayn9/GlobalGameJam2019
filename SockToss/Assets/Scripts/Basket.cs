using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour {
    [SerializeField]
    private Text scoreText;

    private int score;
    private AudioSource scoreSound;

    // Use this for initialization
    void Start () {
        scoreSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Throwable") {
            score++;
            scoreSound.Play(0);
        }
    }
}
