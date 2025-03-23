using System;
using TigerForge;
using UnityEngine;

public class BottomWallCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        EventManager.EmitEvent(EventNames.BallFell);
    }
}
