using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBridgeScript : MonoBehaviour
{
    public PlayerShootingAnimator playerAnimator;

    public void OnReloadFinished()
    {
        if (playerAnimator != null)
            playerAnimator.OnReloadFinished();
    }
}
