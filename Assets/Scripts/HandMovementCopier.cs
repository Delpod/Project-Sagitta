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

    void Start () {
        if (isLocalPlayer) {
            vrLeftHand = GameObject.FindWithTag("LeftHand");
            vrRightHand = GameObject.FindWithTag("RightHand");
            getChildren(leftHandNodes, leftHand.transform);
            getChildren(vrLeftHandNodes, vrLeftHand.transform);
            getChildren(rightHandNodes, rightHand.transform);
            getChildren(vrRightHandNodes, vrRightHand.transform);

            Debug.Log(leftHandNodes.Count);
        }
    }

    void getChildren(List<Transform> list, Transform start) {
        foreach (Transform child in start) {
            list.Add(child);
            getChildren(list, child);
        }
    }
    
    void Update () {
        if (isLocalPlayer) {
            for (int i = 0; i < leftHandNodes.Count; ++i) {
                leftHandNodes[i].position = vrLeftHandNodes[i].position;
                leftHandNodes[i].rotation = vrLeftHandNodes[i].rotation;
            }

            for (int i = 0; i < rightHandNodes.Count; ++i) {
                rightHandNodes[i].position = vrRightHandNodes[i].position;
                rightHandNodes[i].rotation = vrRightHandNodes[i].rotation;
            }
        }
    }
}
