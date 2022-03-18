using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Environment")
            soundManager.PlayRandomClip();
    }
}
