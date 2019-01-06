using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour {

    Animator animator;

    public GameObject vrHead;
    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    
    private Transform vrLeftWrist;
    private Transform vrRightWrist;

    private bool lockRotation = false;
    private Quaternion neededRotation;
 
    void Start () {
        findLeftWrist();
        findRightWrist();
        animator = GetComponent<Animator>();
    }

    void findLeftWrist() {
        GameObject leftWrist = GameObject.FindWithTag("LeftWrist");
        if (leftWrist) {
            vrLeftWrist = leftWrist.transform;
        }
    }

    void findRightWrist() {
        GameObject rightWrist = GameObject.FindWithTag("RightWrist");
        if (rightWrist) {
            vrRightWrist = rightWrist.transform;
        }
    }
 
	void Update () {
        if (leftHand && vrLeftWrist) {
            leftHand.transform.position = vrLeftWrist.position;
            leftHand.transform.rotation = vrLeftWrist.rotation;
            leftHand.transform.Rotate(90f, 0f, -90f);
        } else {
            findLeftWrist();
        }

        if (rightHand && vrRightWrist) {
            rightHand.transform.position = vrRightWrist.position;
            rightHand.transform.rotation = vrRightWrist.rotation;
            rightHand.transform.Rotate(-90f, 180f, -90f);
        } else {
            findRightWrist();
        }

        if (head && vrHead) {

            float absoluteMovement = (Mathf.Abs(transform.position.x - vrHead.transform.position.x) + Mathf.Abs(transform.position.z - vrHead.transform.position.z)) * Time.deltaTime * 1000f;
            animator.SetFloat("AbsoluteMovement", absoluteMovement);
            transform.position = new Vector3(
                vrHead.transform.position.x,
                transform.position.y,
                vrHead.transform.position.z);


            float a = transform.rotation.eulerAngles.y;
            float b = vrHead.transform.rotation.eulerAngles.y;

            if (!lockRotation) {
                float phi = Mathf.Abs(a - b) % 360f;
                float distance = phi > 180f ? 360f - phi : phi;
                float signedDistance = distance * ((a - b >= 0 && a - b <= 180) || (a - b <= -180 && a - b >= -360) ? 1 : -1);

                if (signedDistance > 50f) {
                    lockRotation = true;
                    StartCoroutine(RunTask());
                    neededRotation = Quaternion.Euler(0f, a - 90f, 0f);
                    animator.SetTrigger("TurnLeft");
                } else if (signedDistance < -50f) {
                    lockRotation = true;
                    StartCoroutine(RunTask());
                    neededRotation = Quaternion.Euler(0f, a + 90f, 0f);
                    animator.SetTrigger("TurnRight");
                }
            } else {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 180f);
            }

            head.transform.rotation = vrHead.transform.rotation;

            head.transform.Rotate(0f, 270f, 270f);
        }
    }

    IEnumerator RunTask() {
        yield return new WaitForSeconds(0.5f);
        lockRotation = false;
    }
}
