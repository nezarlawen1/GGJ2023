using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public enum SoundType
    {
        None,
        TurnToLight,
        TurnToHuman,
        Purify,
        MazeStep,
        GrassStep
    }

    //public SoundType soundToPlay;
    [SerializeField] private AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] private AudioClip TurnToLightClip;
    [SerializeField] private AudioClip TurnToHumanClip;
    [SerializeField] private AudioClip PurifyClip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //audioSource = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();

        //ControllersSwapManager.Instance.PlaySoundEvent += SoundManager_PlaySoundEvent;

    }

    private void SoundManager_PlaySoundEvent(SoundType soundType)
    {
        PlaySound(soundType);
    }

    public void PlaySound(SoundType soundToPlay)
    {

        switch (soundToPlay)
        {
            case SoundType.None:
                audioSource.Stop();
                break;
            case SoundType.TurnToLight:
                audioSource.loop = false;
                audioSource.PlayOneShot(TurnToLightClip);
                break;
            case SoundType.TurnToHuman:
                audioSource.loop = false;
                audioSource.PlayOneShot(TurnToHumanClip);
                break;
            case SoundType.Purify:
                audioSource.loop = false;
                audioSource.PlayOneShot(PurifyClip);
                break;
        }

    }
}
