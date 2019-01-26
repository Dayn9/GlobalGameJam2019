using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    
    [SerializeField]
    private float followSpeed;

    private bool isMoving;

	// Use this for initialization
	void Start () {
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.CompareTag("Throwable")) {
                    isMoving = true;
                }
            }
        }

        if(isMoving) {
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
            transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * followSpeed);
            if (touch.phase == TouchPhase.Ended){
                isMoving = false;
            }
        }
    }
}
