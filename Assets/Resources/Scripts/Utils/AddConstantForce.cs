using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddConstantForce : MonoBehaviour
{
    private Rigidbody rb;

    public float xAmount, yAmount, zAmount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(xAmount, yAmount, zAmount);
    }
}
