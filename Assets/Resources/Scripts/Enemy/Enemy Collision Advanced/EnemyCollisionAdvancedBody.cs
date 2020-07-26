using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionAdvancedBody : MonoBehaviour
{
    EnemyCollisionAdvancedMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyCollisionAdvancedMovement>();
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
                movement.ChangeMovementState(EnemyCollisionAdvancedMovement.MovementState.Slow);
                break;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Slime":
                movement.ChangeMovementState(EnemyCollisionAdvancedMovement.MovementState.Regular);
                break;
        }
    }
}
