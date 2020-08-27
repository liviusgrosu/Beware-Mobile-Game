using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    PlayerMovement movement;
    PlayerFootSoundController footController;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        footController = GetComponent<PlayerFootSoundController>();
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Slime":
                movement.ChangeMovementState(EnumDefinitions.MovementState.Slow);
                footController.ChangeMovementSoundType(EnumDefinitions.MovementSoundTypes.Slime);
                break;
            case "Grass":
                movement.ChangeMovementState(EnumDefinitions.MovementState.Regular);
                footController.ChangeMovementSoundType(EnumDefinitions.MovementSoundTypes.Grass);
                break;
        }
    }
}
