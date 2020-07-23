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
