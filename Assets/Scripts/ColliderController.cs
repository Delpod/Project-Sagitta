using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        GameObject hit = collision.gameObject;
        if (hit.CompareTag("Head")) {
            GameObject otherPlayer = hit.GetComponentInParent<Transform>().parent.parent.gameObject;
            GameObject thisPlayer = transform.parent.parent.parent.gameObject;
            if (thisPlayer != otherPlayer) {
                otherPlayer.GetComponent<PlayerController>().CmdLose();
                thisPlayer.GetComponent<PlayerController>().CmdWin();
            }
        }
    }
}
