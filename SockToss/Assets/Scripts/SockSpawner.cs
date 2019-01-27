using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject sock;

    [SerializeField]
    private int poolSize;

    private Queue<GameObject> pool;
    private bool touching;
    private Touch touch;

	// Use this for initialization
	void Start () {
        pool = new Queue<GameObject>();
        touching = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touches.Length != 0) {
            touch = Input.GetTouch(0);
            if (!touching) {
                if(pool.Count < poolSize)
                    pool.Enqueue(Instantiate(sock, Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.55f)), new Quaternion(0,90,0,90)));
                else {
                    Destroy(pool.Dequeue());
                    pool.Enqueue(Instantiate(sock, Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.55f)), new Quaternion(0, 90, 0, 90)));
                }
                touching = true;
            } else if(touch.phase == TouchPhase.Ended)
            touching = false;
        }
    }
}
