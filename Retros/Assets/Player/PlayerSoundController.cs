using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] SoundManager FootSounds;

    void Step()
    {
        FootSounds.PlayRandomClip();
    }
}