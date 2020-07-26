using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePrimitiveBody : MonoBehaviour
{
    EnemyProjectilePrimitiveMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyProjectilePrimitiveMovement>();
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
}
