using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayObject : MonoBehaviour
{
    public float maxAngle = 2f;
    public float speed = 0.5f;
    private Quaternion endRotation;
    private Quaternion startRotation;
    private Quaternion originRotation;
    float rotationProgress;

    void Start()
    {
        rotationProgress = 0;
        originRotation = transform.rotation;
        startRotation = originRotation;
        endRotation = originRotation * Quaternion.Euler(Random.Range(-maxAngle, maxAngle), Random.Range(-maxAngle, maxAngle), Random.Range(-maxAngle, maxAngle));
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationProgress < 1 && rotationProgress >= 0){
            rotationProgress += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationProgress);
        } 
        else
        {
            rotationProgress = 0;
            startRotation = transform.rotation;
            endRotation = originRotation * Quaternion.Euler(Random.Range(-maxAngle, maxAngle), Random.Range(-maxAngle, maxAngle), Random.Range(-maxAngle, maxAngle));
        }
    }
}
