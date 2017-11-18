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
    public ApplyForce ball;
    public float force;

    private int state;
    private float clickTime;
    private float elipse;
    private float length;
    private float angle;
    private Vector3 forceAngle;

    private const float PI = 3.14f;
    Vector3 prevPos;

    private void Awake() {

        state = 0;
        length = 5;
        end.SetActive(false);

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
                head.SetActive(false);
                end.SetActive(true);
            }
            else if (state == 1) {
                state = 2;
                changeRotation(mousePos);
                end.SetActive(false);
            }
            else if (state == 2) {
                clickTime = Time.time;
            }
        }
        if (state == 2 && Input.GetMouseButtonUp(0)) {
            elipse = Time.time - clickTime;
            ball.force(elipse * force, -forceAngle);

            state = 0;
            head.SetActive(true);
        }
    }

    void changePosition(Vector3 des) {
        Vector3 real_des = new Vector3(des.x, transform.position.y, des.z - length / 2);
        transform.position = real_des;
    }

    // TODO: Test other axises
    // end position is the mouse position
    void changeRotation(Vector3 endPos) {
        Debug.Log(endPos);
        prevPos = head.transform.position;
        float hor = endPos.x - head.transform.position.x;
        float ver = endPos.z - head.transform.position.z;
        angle = Mathf.Atan(hor / ver);
        forceAngle = new Vector3(hor, 0, ver);
        Vector3.Normalize(forceAngle);
        float rotateAngle = angle / PI * 180;
        transform.Rotate(0, rotateAngle, 0);
        transform.Translate(prevPos.x - head.transform.position.x, 0, head.transform.position.z - prevPos.z);
    }
}
