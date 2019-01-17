using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {
    public void CmdPlayerWin(GameObject player) {
        if (isServer) {
            player.GetComponent<PlayerController>().RpcWin();
        }
    }
    
    public void CmdPlayerLose(GameObject player) {
        if (isServer) {
            player.GetComponent<PlayerController>().RpcLose();
        }
    }
}
