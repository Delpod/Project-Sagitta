using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IKHandling : NetworkBehaviour {
    
    public float ikWeight = 1;
    public Transform leftHandIKTarget;
    public Transform rightHandtIKTarget;
    
    public Transform hintLeftHand;
    public Transform hintRightHand;

    private Animator animator;

    private void Start() {
        if (isLocalPlayer) {
            FindLeftWrist();
            FindRightWrist();
        }
        animator = GetComponent<Animator>();
	}

    void FindLeftWrist() {
        GameObject leftWrist = GameObject.FindWithTag("LeftWrist");
        if (leftWrist) {
            leftHandIKTarget = leftWrist.transform;
        }
    }

    void FindRightWrist() {
        GameObject rightWrist = GameObject.FindWithTag("RightWrist");
        if (rightWrist) {
            rightHandtIKTarget = rightWrist.transform;
        }
    }

    private void OnAnimatorIK(int layerIndex) {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandtIKTarget.position);

        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);

        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandtIKTarget.rotation);

        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, ikWeight);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, ikWeight);

        animator.SetIKHintPosition(AvatarIKHint.LeftElbow, hintLeftHand.position);
        animator.SetIKHintPosition(AvatarIKHint.RightElbow, hintRightHand.position);
    }
}
