using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicators : MonoBehaviour
{
    private PlayerAttackingBehaviour attackingBehaviour;
    private GameObject door;
    private Camera uiCam;

    public RectTransform enemyIndicatorImg, doorIndicatorImg;

    private Transform nearestEnemy;
    private Vector3 screenTargetPos;
    private Vector2 onScreenTargetPos;

    private Vector3 targetArrowNewPos;
    private Vector3 targetDirection;
    bool isTargetOffScreen;

    private Vector3 centreOfScreen;
    float max;

    void Start()
    {
        attackingBehaviour = GameObject.Find("Player").GetComponent<PlayerAttackingBehaviour>();
        door = GameObject.Find("Exit Door");

        uiCam = GameObject.Find("UI Camera").GetComponent<Camera>();
        centreOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    }

    void Update()
    {
        nearestEnemy = attackingBehaviour.GetNearestTarget();
        if (nearestEnemy != null)
            CalculateTargetIndicator(nearestEnemy.position, enemyIndicatorImg);
        else
            enemyIndicatorImg.GetComponent<Image>().enabled = false; // If the enemy dies

        if (door.GetComponent<ExitDoorController>().IsStuckOpen())
            CalculateTargetIndicator(door.transform.position, doorIndicatorImg);
    }

    private void CalculateTargetIndicator(Vector3 targetPos, RectTransform indicatorImg)
    {
        //get viewport positions
        screenTargetPos = uiCam.WorldToViewportPoint(targetPos);

        // If enemy is out of camera bounds then don't display the arrow
        isTargetOffScreen = (screenTargetPos.x >= 0 && screenTargetPos.x <= 1 && screenTargetPos.y >= 0 && screenTargetPos.y <= 1);
        indicatorImg.GetComponent<Image>().enabled = !isTargetOffScreen;

        if (!isTargetOffScreen)
        {
            // Get the ratio of which the arrow will reside in respect to screen resolution
            onScreenTargetPos = new Vector2(screenTargetPos.x - 0.5f, screenTargetPos.y - 0.5f) * 2;
            max = Mathf.Max(Mathf.Abs(onScreenTargetPos.x), Mathf.Abs(onScreenTargetPos.y));
            onScreenTargetPos = (onScreenTargetPos / (max * 2)) + new Vector2(0.5f, 0.5f);

            // Clamp the arrow image so part of it doesn't clip out of the cameras render window
            float clampedScreenWidth = Mathf.Clamp((onScreenTargetPos.x * Screen.width), indicatorImg.rect.width, Screen.width - indicatorImg.rect.width);
            float clampedScreenHeight = Mathf.Clamp((onScreenTargetPos.y * Screen.height), indicatorImg.rect.height, Screen.height - indicatorImg.rect.height);

            // Move the indicator to the calculated spot
            targetArrowNewPos = new Vector3(clampedScreenWidth, clampedScreenHeight, 0);
            indicatorImg.localPosition = targetArrowNewPos;

            // Rotate the arrow to follow the target
            targetDirection = targetArrowNewPos - centreOfScreen;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            indicatorImg.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // Lock rotation on x axis to compensate for UI cameras rotation
            indicatorImg.eulerAngles = new Vector3(60, 0, indicatorImg.eulerAngles.z);
        }
    }
}
