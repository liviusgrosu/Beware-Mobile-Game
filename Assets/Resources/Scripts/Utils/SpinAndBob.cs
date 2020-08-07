using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndBob : MonoBehaviour
{
    public float turnSpeed = 0.15f;
    public float bobbingMultiplier = 0.15f;
    private float startingYPos;

    void Awake()
    {
        startingYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, turnSpeed, 0);
        transform.position = new Vector3(transform.position.x, startingYPos + (Mathf.Sin(Time.time) * bobbingMultiplier), transform.position.z);
    }
}
