using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource playerJump;

    public void PlaySoundEffect(string effect) {
        if (effect == "jump") {
            playerJump.Play();
        }
    }
}
