using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderPrimitiveBody : MonoBehaviour
{
    EnemyColliderPrimitiveMovement movement; 

    private void Start()
    {
        movement = GetComponent<EnemyColliderPrimitiveMovement>();
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                Vector3 inDirection = movement.currDirection;
                Vector3 inNormal = col.contacts[0].normal;
                movement.currDirection = Vector3.Reflect(inDirection, inNormal);
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
