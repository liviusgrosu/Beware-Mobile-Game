using Packages.Rider.Editor.PostProcessors;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropExplosion : MonoBehaviour
{
    // Turn on gravity
    // Turn off kinematics
    // Disable spinning script

    // Figure out random direction and force
    // Apply force
    // 

    private Rigidbody rb;

    private SpinAndBob spinningScript;
    private Collider sphereCollider;

    private Vector3 projectionDir;
    private float projectionForce = 15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        spinningScript = GetComponent<SpinAndBob>();
        spinningScript.enabled = false;

        sphereCollider = GetComponent<Collider>();
        sphereCollider.isTrigger = false;
    }

    private void Start()
    {
        transform.RotateAround(transform.position, Vector3.up, Random.Range(0, 360));
        projectionDir = (transform.forward + transform.up).normalized;
        rb.AddForce(projectionDir * 5f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            Vector3 inDirection = transform.forward;
            Vector3 inNormal = col.contacts[0].normal;
            rb.AddForce(Vector3.Reflect(inDirection, inNormal) * 2f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grass")
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            spinningScript.enabled = true;
            sphereCollider.isTrigger = true;

            this.enabled = false;
        }
    }

    public void RespawnDrop(Vector3 newDir)
    {
        projectionDir = (newDir + transform.up).normalized;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(projectionDir * 2f, ForceMode.Impulse);
    }
}
