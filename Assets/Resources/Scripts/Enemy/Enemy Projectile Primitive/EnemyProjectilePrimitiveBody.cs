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

}
