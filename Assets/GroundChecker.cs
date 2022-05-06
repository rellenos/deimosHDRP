using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public PlayerController playerMove;

    private void OnTriggerStay(Collider other)
    {
        playerMove.inGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerMove.inGround = false;
    }
}
