using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    
    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private float endForce;

    [SerializeField]
    private float magMultiplier;

    private bool isMoving;

    private Rigidbody rb;
    private Touch touch;

	// Use this for initialization
	void Start () {
        isMoving = false;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
	}

    class MouseTouch {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.touches.Length != 0)
            touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.55f));
            rb.velocity = new Vector3(0,0,0);
            rb.angularVelocity = new Vector3(0, 0, 0);

            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.CompareTag("Throwable")) {
                    isMoving = true;
                }
            }
        }

        
    }

    void FixedUpdate() {
        if (isMoving){
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
            transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * followSpeed);
            if (touch.phase == TouchPhase.Ended) {
                Vector3 swipe = touch.deltaPosition;
                isMoving = false;
                rb.isKinematic = false;
                rb.AddForce(new Vector3(swipe.x, swipe.y, touch.deltaPosition.magnitude) * magMultiplier, ForceMode.Impulse);
            }
        }
    }
}
