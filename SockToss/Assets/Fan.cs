using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    [SerializeField]
    private float forceStrength;

    [SerializeField]
    private float threshold;

    private BoxCollider blowZone;

	// Use this for initialization
	void Start () {
		blowZone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other) {
        Rigidbody Sock = other.gameObject.GetComponent<Rigidbody>();
        float distanceToFan = (Sock.transform.position - transform.position).magnitude;
        float dForce = threshold / distanceToFan;

        Sock.AddForce(-transform.right * dForce * forceStrength);
    }
}
