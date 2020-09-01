using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRespawnPoint : MonoBehaviour
{
    public Transform respawnPoint;

    public void RespawnDrop(Transform drop)
    {
        drop.position = respawnPoint.position;
        drop.GetComponent<DropExplosion>().RespawnDrop(respawnPoint.forward);
    }
}
