using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Quaternion fixedRotation;
    // Start is called before the first frame update
    void Awake()
    {
        fixedRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = fixedRotation;
    }
}
