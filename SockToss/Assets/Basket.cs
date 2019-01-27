using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour {
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Transform[] basketPositions;

    private int score;
    private AudioSource scoreSound;

    // Use this for initialization
    void Start () {
        scoreSound = GetComponent<AudioSource>();
        Transform tBucket = basketPositions[Random.Range(0, basketPositions.Length)];
        transform.position = tBucket.position;
        transform.rotation = tBucket.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Throwable") {
            score++;
            scoreSound.Play(0);
            Transform tBucket = basketPositions[Random.Range(0, basketPositions.Length)];
            transform.position = tBucket.position;
            transform.rotation = tBucket.rotation;
            Destroy(other.gameObject);
        }
    }
}
