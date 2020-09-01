using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartingCameraEffect : MonoBehaviour
{
    private Camera mainCam;

    private Transform startingLevelCamPos;
    private Vector3 originalCamPos;

    private bool movingToStart, movingToOriginal;
    private bool isInStartingPos;

    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        originalCamPos = mainCam.transform.position;

        startingLevelCamPos = GameObject.Find("Starting Camera Spot").transform;
        movingToStart = true;

        startTime = Time.time;
        journeyLength = Vector3.Distance(mainCam.transform.position, startingLevelCamPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToStart)
        {
            float distCovered = (Time.time - startTime) * 10f;
            float journeyPerc = distCovered / journeyLength;
            mainCam.transform.position = Vector3.Lerp(originalCamPos, startingLevelCamPos.position, Mathf.SmoothStep(0f, 1f, journeyPerc));

            if (Vector3.Distance(mainCam.transform.position, startingLevelCamPos.position) <= 0.01f)
            {
                movingToStart = false;
                isInStartingPos = true;
            }
        }
    }

    public bool IsInStartingPos()
    {
        return isInStartingPos;
    }
}
