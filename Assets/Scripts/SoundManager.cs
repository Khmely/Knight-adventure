using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerAttack, playerHurt, playerJump, playerKill;
    static AudioSource audioSource;
    void Start()
    {
        playerAttack = Resources.Load<AudioClip>("attack");
        playerHurt = Resources.Load<AudioClip>("hurt");
        playerJump = Resources.Load<AudioClip>("jump");
        playerKill = Resources.Load<AudioClip>("kill");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "attack":
                audioSource.PlayOneShot(playerAttack);
                break;
            case "jump":
                audioSource.PlayOneShot(playerJump);
                break;
            case "hurt":
                audioSource.PlayOneShot(playerHurt);
                break;
            case "kill":
                audioSource.PlayOneShot(playerKill);
                break;
        }
    }
}
