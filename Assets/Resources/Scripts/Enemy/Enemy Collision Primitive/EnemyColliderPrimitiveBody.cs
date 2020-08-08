using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderPrimitiveBody : MonoBehaviour
{
    private SoundController soundController;

    EnemyColliderPrimitiveMovement movement; 

    private void Start()
    {
        movement = GetComponent<EnemyColliderPrimitiveMovement>();
        soundController = GameObject.Find("Sound Controller").GetComponent<SoundController>();
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                Vector3 inDirection = movement.currDirection;
                Vector3 inNormal = col.contacts[0].normal;
                movement.currDirection = Vector3.Reflect(inDirection, inNormal);
                soundController.PlayEnemyWallBounce();
                break;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                //TODO: Change this so that this variable is accessed from a script that would make sense to store it
                col.gameObject.GetComponent<PlayerHealthSystem>().ChangeHealth(-1);
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Slime":
                movement.ChangeMovementState(EnemyColliderPrimitiveMovement.MovementState.Slow);
                break;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Slime":
                movement.ChangeMovementState(EnemyColliderPrimitiveMovement.MovementState.Regular);
                break;
        }
    }
}
