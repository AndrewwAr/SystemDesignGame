using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameAudioClips{ cardFlip=0 , matching=1 , misMatching=2 , gameOver=3 }

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource sfxAudioSource;

    [Header("Game Audio Clips")]
    [SerializeField]
    AudioClip cardFlipClip;
    [SerializeField]
    AudioClip matchingClip;
    [SerializeField]
    AudioClip misMatchingClip;
    [SerializeField]
    AudioClip gameOverClip;


    public void PlayAudioSoundEffect(GameAudioClips gameAudioClips)
    {
        switch (gameAudioClips)
        {
            case GameAudioClips.cardFlip:
                sfxAudioSource.PlayOneShot(cardFlipClip);
                break;

            case GameAudioClips.matching:
                sfxAudioSource.PlayOneShot(matchingClip);
                break;

            case GameAudioClips.misMatching:
                sfxAudioSource.PlayOneShot(misMatchingClip);
                break;

            case GameAudioClips.gameOver:
                sfxAudioSource.PlayOneShot(gameOverClip);

                break;

            default:
                break;
        }
    }

}
