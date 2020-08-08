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
    }

    public void PlayButtonPress()
    {
        audioSrc.PlayOneShot(buttonPress);
    }
}
