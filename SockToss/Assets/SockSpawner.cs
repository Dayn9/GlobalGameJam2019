using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject sock;

    private bool touching;
    private Touch touch;

	// Use this for initialization
	void Start () {
        touching = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touches.Length != 0) {
            touch = Input.GetTouch(0);
            if (!touching) {
                Instantiate(sock, Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.55f)), Quaternion.identity);
                touching = true;
            } else if(touch.phase == TouchPhase.Ended)
            touching = false;
        }
    }
}
