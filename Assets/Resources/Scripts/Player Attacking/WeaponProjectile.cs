using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class WeaponProjectile : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag) 
        {
            case "Enemy":
                col.gameObject.GetComponent<EnemyHealthSystem>().ChangeHealth(-damage);
                break;
        }
        Destroy(this.gameObject);
    }
}
