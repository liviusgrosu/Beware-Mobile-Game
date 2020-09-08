using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRespawnCollider : MonoBehaviour
{
    private DropRespawnController respawnController;

    private void Start()
    {
        GameObject respawnControllerObj = GameObject.Find("Drop Respawn Controller");
        if (respawnControllerObj != null)
            respawnController = GameObject.Find("Drop Respawn Controller").GetComponent<DropRespawnController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (respawnController != null && collision.gameObject.tag == "Drop")
            respawnController.AddDropToController(collision.gameObject.transform);
    }
}
