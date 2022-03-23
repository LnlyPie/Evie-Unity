using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static AudioSource audioSrc;

    public static AudioClip playerJump;
    public static AudioClip playerJumpSec;

    void Start() {
        playerJump = Resources.Load<AudioClip>("player_jump");
        playerJumpSec = Resources.Load<AudioClip>("player_jump_sec");
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(string effect) {
        if (effect == "jump") {
            audioSrc.PlayOneShot(playerJump);
        } else if (effect == "jump2") {
            audioSrc.PlayOneShot(playerJumpSec);
        }
    }
}