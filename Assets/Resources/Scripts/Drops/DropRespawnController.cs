using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRespawnController : MonoBehaviour
{
    private List<DropRespawnPoint> respawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoints = new List<DropRespawnPoint>();
        foreach (Transform spawnPoint in this.transform)
            respawnPoints.Add(spawnPoint.GetComponent<DropRespawnPoint>());
    }

    public void AddDropToController(Transform drop)
    {
        respawnPoints[Random.Range(0, respawnPoints.Count)].RespawnDrop(drop);
    }
}
