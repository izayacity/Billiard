using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hole")) {
            GameManager.instance.AddScore(10);
            Destroy(gameObject);
        }
    }
}
