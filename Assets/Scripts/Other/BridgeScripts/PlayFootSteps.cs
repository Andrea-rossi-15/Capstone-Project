using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootSteps : MonoBehaviour
{
    public void PlayFootStep()
    {
        SoundManager.PlaySound(SoundType.FOOTSTEPS);
    }
}
