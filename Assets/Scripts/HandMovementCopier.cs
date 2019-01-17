using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HandMovementCopier : NetworkBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    private GameObject vrLeftHand;
    private GameObject vrRightHand;

    List<Transform> leftHandNodes = new List<Transform>();
    List<Transform> vrLeftHandNodes = new List<Transform>();
    List<Transform> rightHandNodes = new List<Transform>();
    List<Transform> vrRightHandNodes = new List<Transform>();

    private void Start () {
        if (isLocalPlayer) {
            vrLeftHand = GameObject.FindWithTag("LeftHand");
            vrRightHand = GameObject.FindWithTag("RightHand");

            GetChildren(leftHandNodes, leftHand.transform);
            GetChildren(vrLeftHandNodes, vrLeftHand.transform);
            GetChildren(rightHandNodes, rightHand.transform);
            GetChildren(vrRightHandNodes, vrRightHand.transform);
        }
    }

    private void GetChildren(List<Transform> list, Transform start) {
        foreach (Transform child in start) {
            list.Add(child);
            GetChildren(list, child);
        }
    }
    
    private void Update () {
        if (isLocalPlayer) {
            for (int i = 0; i < leftHandNodes.Count; ++i) {
                leftHandNodes[i].position = vrLeftHandNodes[i].position;
                leftHandNodes[i].rotation = vrLeftHandNodes[i].rotation;
                if (i == 0) {
                    leftHandNodes[i].Rotate(130f, 0f, 190f);
                }
            }

            for (int i = 0; i < rightHandNodes.Count; ++i) {
                rightHandNodes[i].position = vrRightHandNodes[i].position;
                rightHandNodes[i].rotation = vrRightHandNodes[i].rotation;
                if (i == 0) {
                    rightHandNodes[i].Rotate(130f, 0f, 190f);
                }
            }
        }
    }
}
