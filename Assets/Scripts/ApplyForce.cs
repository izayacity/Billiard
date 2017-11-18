using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour {

    private Vector3 movement;
    private Vector3 norm;
    private float acceleration;


    public void force(float force, Vector3 forceAngle) {
        norm = forceAngle;
        acceleration = force;
        movement = forceAngle * force;
    }

    private void FixedUpdate() {
        //gameObject.GetComponent<Rigidbody>().velocity = movement;        
        if (acceleration >= 0) {
            movement = norm * acceleration;
            gameObject.transform.position += movement;
            acceleration -= 0.3f;
            Debug.Log("Acceleration: " + acceleration);
        }        
    }
}
