using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundController : MonoBehaviour
{
    private AudioSource audioSrc;

    [SerializeField]
    private AudioClip buttonPress;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        float volumeLevel = SaveSystem.LoadSettings().soundLevel;
        ChangeAudioSrcVolume(volumeLevel);
    }

    public void PlayButtonPress()
    {
        audioSrc.PlayOneShot(buttonPress);
    }

    public void ChangeAudioSrcVolume(float volumeLevel)
    {
        audioSrc.volume = volumeLevel;
    }
}
