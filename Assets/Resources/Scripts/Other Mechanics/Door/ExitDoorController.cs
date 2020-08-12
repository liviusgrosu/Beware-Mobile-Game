using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorController : MonoBehaviour
{
    private bool doorStuckOpen;

    public ExitDoorCollider doorCollider;

    [SerializeField]
    private Animator anim;

    public void TriggerFullOpen()
    {
        if (!doorStuckOpen)
        {
            anim.SetTrigger("Open Door");
            PlayOpenDoorSound();
            doorCollider.ChangeDoorToOpen();
            doorStuckOpen = true;
        }
    }

    public void TriggerMomentOpen()
    {
        StartCoroutine(OpenDoorMomentarily());
    }

    private void PlayOpenDoorSound()
    {
        GameObject.Find("Sound Controller").GetComponent<SoundController>().PlayerDoorOpenSound();
    }

    IEnumerator OpenDoorMomentarily()
    {
        anim.SetTrigger("Open Door");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("Close Door");
    }
}