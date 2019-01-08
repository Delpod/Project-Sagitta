using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : NetworkBehaviour {

    Animator animator;

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;

    public Transform vrHead;
    public Transform vrLeftWrist;
    public Transform vrRightWrist;

    private bool lockRotation = false;
    private Quaternion neededRotation;
 
    void Start () {
        if (isLocalPlayer) {
            findLeftWrist();
            findRightWrist();
            findVRHead();
        }
        animator = GetComponent<Animator>();
    }

    void findVRHead() {
        GameObject head = GameObject.FindWithTag("VRCamera");
        if (head) {
            vrHead = head.transform;
        }
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

            float absoluteMovement = (Mathf.Abs(transform.position.x - vrHead.position.x) + Mathf.Abs(transform.position.z - vrHead.position.z)) * Time.deltaTime * 1000f;
            animator.SetFloat("AbsoluteMovement", absoluteMovement);
            transform.position = new Vector3(
                vrHead.position.x,
                transform.position.y,
                vrHead.position.z);


            float a = transform.rotation.eulerAngles.y;
            float b = vrHead.rotation.eulerAngles.y;

            if (!lockRotation) {
                float phi = Mathf.Abs(a - b) % 360f;
                float distance = phi > 180f ? 360f - phi : phi;
                float signedDistance = distance * ((a - b >= 0 && a - b <= 180) || (a - b <= -180 && a - b >= -360) ? 1 : -1);

                if (signedDistance > 30f) {
                    lockRotation = true;
                    StartCoroutine(RunFinishTurnTask());
                    neededRotation = Quaternion.Euler(0f, a - 45f, 0f);
                    animator.SetBool("TurnLeft", true);
                } else if (signedDistance < -30f) {
                    lockRotation = true;
                    StartCoroutine(RunFinishTurnTask());
                    neededRotation = Quaternion.Euler(0f, a + 45f, 0f);
                    animator.SetBool("TurnRight", true);
                }
            } else {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 180f);
            }

            head.transform.rotation = vrHead.rotation;
            head.transform.Rotate(0f, 270f, 270f);

            if (head.transform.rotation.eulerAngles.x > 60f && head.transform.rotation.eulerAngles.x < 180f) {
                head.transform.rotation = Quaternion.Euler(
                    60f,
                    head.transform.rotation.eulerAngles.y,
                    head.transform.rotation.eulerAngles.z);
            }


            if (head.transform.rotation.eulerAngles.z > 300f) {
                head.transform.rotation = Quaternion.Euler(
                    head.transform.rotation.eulerAngles.x,
                    head.transform.rotation.eulerAngles.y,
                    300f);
            } else if (head.transform.rotation.eulerAngles.z < 235f) {
                head.transform.rotation = Quaternion.Euler(
                    head.transform.rotation.eulerAngles.x,
                    head.transform.rotation.eulerAngles.y,
                    235f);
            }
        }
    }

    IEnumerator RunFinishTurnTask() {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("TurnLeft", false);
        animator.SetBool("TurnRight", false);
        lockRotation = false;
    }
}
