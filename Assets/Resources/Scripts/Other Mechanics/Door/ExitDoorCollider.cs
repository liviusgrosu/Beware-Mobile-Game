using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorCollider : MonoBehaviour
{
    [SerializeField] private Material OpenMaterial;
    [SerializeField] private Material ClosedMaterial;
    private Renderer rend;

    private bool doorIsOpenToPlayer;

    [HideInInspector]
    public bool playerFinished;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = ClosedMaterial;
    }

    public void ChangeDoorToOpen()
    {
        rend.material = OpenMaterial;
        doorIsOpenToPlayer = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (doorIsOpenToPlayer)
                playerFinished = true;
        }
    }
}
