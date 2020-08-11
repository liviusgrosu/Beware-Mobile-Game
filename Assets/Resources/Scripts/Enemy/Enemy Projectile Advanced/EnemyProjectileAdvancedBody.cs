using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAdvancedBody : MonoBehaviour
{
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
