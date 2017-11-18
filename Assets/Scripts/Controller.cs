using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * States:
     0: Ready for head setting;
     1: Ready for end setting;
     2: Ready for push;
     3: play push, couldn't operate;
         */

public class Controller : MonoBehaviour {

    public GameObject head;
    public GameObject end;
    private int state;
    private float clickTime;
    private float elipse;
    private float length;
    private const float PI = 3.14f;
    Vector3 prevPos;

    private void Awake() {

        state = 0;
        length = 5;

        if (System.Object.ReferenceEquals(head, null)) {
            head = gameObject.transform.GetChild(0).Find("Head").gameObject;
            Debug.Log("Head of the stick is empty.");
        }

        if (System.Object.ReferenceEquals(end, null)) {
            end = gameObject.transform.GetChild(1).Find("End").gameObject;
            Debug.Log("End of the stick is empty.");
        }

    }

    void Update() {
        if (Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (state == 0) {
                state = 1;               
                changePosition(mousePos);
            }
            else if (state == 1) {
                state = 2;
                changeRotation(mousePos);
            }
            else if (state == 2) {
                clickTime = Time.time;
            }
        }
        if (state == 2 && Input.GetMouseButtonUp(0)) {
            elipse = Time.time - clickTime;
            //Debug.Log(elipse);
        }
    }

    void changePosition(Vector3 des) {
        Vector3 real_des = new Vector3(des.x, transform.position.y, des.z - length / 2);
        transform.position = real_des;
    }

    // TODO: Test other axises
    void changeRotation(Vector3 endPos) {
        prevPos = head.transform.position;
        float angle = Mathf.Atan(Mathf.Abs(endPos.x - head.transform.position.x) / Mathf.Abs(endPos.z - head.transform.position.z)) / PI * 180;
        transform.Rotate(0, -angle, 0);
        float vertical =  length / 2 * (1 - Mathf.Cos(angle));
        float horizental = length / 2 * Mathf.Sin(angle);
        transform.Translate(prevPos.x - head.transform.position.x, 0, head.transform.position.z - prevPos.z);
    }
}
