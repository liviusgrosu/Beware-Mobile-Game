using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDrop : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float shimmerStartTime;

    private void Awake()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                PlayerHealthSystem playerHealth = col.gameObject.GetComponent<PlayerHealthSystem>();
                if (!playerHealth.IsHealthFull())
                {
                    playerHealth.ChangeHealth(1);
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
