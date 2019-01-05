using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour {

    Animator animator;

    public float ikWeight = 1;
    public Transform leftHandIKTarget;
    public Transform rightHandtIKTarget;
    
    public Transform hintLeftHand;
    public Transform hintRightHand;

    private void Start () {
        animator = GetComponent<Animator>();
	}

    private void Update () {
		    
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
