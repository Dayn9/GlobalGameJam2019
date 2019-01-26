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

    private List<Vector3> dPositions;

    private bool isTouched;

    private Rigidbody rb;
    private Touch touch;

	// Use this for initialization
	void Start () {
        dPositions = new List<Vector3>();
        isTouched = false;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.touches.Length != 0)
            touch = Input.GetTouch(0);

        // Keep track of previous 4 frames
        if (dPositions.Count < 4){
            dPositions.Add(touch.deltaPosition);
        }
        else {
            dPositions.RemoveAt(0);
            dPositions.Add(touch.deltaPosition);
        }

        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.55f));
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);

            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.CompareTag("Throwable")) {
                    isTouched = true;
                }
            }
        }
    }

    void FixedUpdate() {
        if (isTouched){
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
            transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * followSpeed);
            if (touch.phase == TouchPhase.Ended) {
                Vector3 swipe = avgVec3(dPositions);
                isTouched = false;
                rb.isKinematic = false;
                rb.AddForce(new Vector3(swipe.x, swipe.y, swipe.magnitude) * magMultiplier, ForceMode.Impulse);
            }
        }
    }

    Vector3 avgVec3(List<Vector3> vecList) {
        Vector3 avg = new Vector3(0, 0, 0);

        foreach(Vector3 vec in vecList) {
            avg += vec;
        }
        return avg /= vecList.Count;
    }
}
