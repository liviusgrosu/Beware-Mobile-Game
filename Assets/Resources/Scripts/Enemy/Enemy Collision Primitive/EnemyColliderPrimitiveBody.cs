using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderPrimitiveBody : MonoBehaviour
{
    private SoundController soundController;

    EnemyColliderPrimitiveMovement movement; 

    private bool hasCollided;
    private List<Collision> collidedObjects;

    private void Start()
    {
        movement = GetComponent<EnemyColliderPrimitiveMovement>();
        soundController = GameObject.Find("Sound Controller").GetComponent<SoundController>();
        collidedObjects = new List<Collision>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 inDirection = movement.currDirection;
        Vector3 inNormal = col.GetContact(0).normal;
        Vector3 outDirection = Vector3.Reflect(inDirection, inNormal);

        if (Vector3.Angle(inNormal, outDirection) > 90f)
        {
            return;
        }

        movement.currDirection = outDirection;
        soundController.PlayEnemyWallBounce();

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealthSystem>().ChangeHealth(-1);
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
