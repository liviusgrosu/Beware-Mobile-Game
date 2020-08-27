using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFootSoundController : MonoBehaviour
{
    public float maxWaitCycleTime = 100.0f;
    private float curWaitCycleTime = 0f;

    private PlayerMovement movement;
    private SoundController gameSound;

    public EnumDefinitions.MovementSoundTypes movementSound;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        gameSound = GameObject.Find("Sound Controller").GetComponent<SoundController>();
    }

    void Update()
    {
        curWaitCycleTime += movement.GetMovingSpeed();
        if(curWaitCycleTime >= maxWaitCycleTime)
        {
            curWaitCycleTime = 0f;
            gameSound.PlayMovementSound(movementSound);
        }
    }

    public void ChangeMovementSoundType(EnumDefinitions.MovementSoundTypes sound)
    {
        movementSound = sound;
    }
}
