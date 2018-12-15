using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour {

    public GameObject vrHead;
    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    
    private Transform vrLeftWrist;
    private Transform vrRightWrist;

    // Use this for initialization
    void Start () {
        findLeftWrist();
        findRightWrist();
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

	// Update is called once per frame
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
            head.transform.position = vrHead.transform.position;
            head.transform.rotation = vrHead.transform.rotation;
            head.transform.Rotate(0f, 270f, 270f);
        }
    }
}
