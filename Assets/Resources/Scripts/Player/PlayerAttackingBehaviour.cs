﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttackingBehaviour : MonoBehaviour
{
    private PlayerMovement movement;
    private Transform playerBody;
    private Animator modelAnimator;

    private WeaponController weaponController;
    [SerializeField]
    private Transform bulletSpawn;

    private bool isFiring;

    [SerializeField] 
    private float rotationSpeed = 360f;

    private EnemyManager enemyManager;
    private Transform nearestTarget;
    private bool enemiesStillExist;

    const int LOOK_MOVEMENT = 1;
    const int LOOK_ENEMY = 2;

    private bool isGenerallyLookingAtEnemy;
    [SerializeField] private float angleRelativeTargetLook = 20f;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
        
        playerBody = transform.Find("Player Body");
        modelAnimator = playerBody.GetComponent<Animator>();

        movement = GetComponent<PlayerMovement>();
        weaponController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesStillExist = enemyManager.IsMoreEnemies();

        if (movement.IsMoving())
        {
            if (weaponController.IsFiringWeapon()) weaponController.ToggleWeaponFire(false);
            //Find the nearest enemy when the player is moving 
            //This way its precalcuated and player doesn't switch
            if (enemiesStillExist)
                nearestTarget = enemyManager.GetClosestEnemy(transform.position);
            RotateTowardsTarget(LOOK_MOVEMENT);
        }
        else
        {
            if(enemiesStillExist)
            { 
                //If an enemy dies then change to the nearest alive one
                if (nearestTarget == null)
                {
                    nearestTarget = enemyManager.GetClosestEnemy(transform.position);
                }
                
                RotateTowardsTarget(LOOK_ENEMY);
                if (!weaponController.IsFiringWeapon() && isGenerallyLookingAtEnemy) 
                {
                    weaponController.ToggleWeaponFire(true);
                    if (!modelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shooting Cycle"))
                    {
                        modelAnimator.SetTrigger("Start Shooting");
                    }
                }
            }
            else
            {
                if (weaponController.IsFiringWeapon()) 
                {
                    weaponController.ToggleWeaponFire(false);
                    if (!modelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle Cycle"))
                    {
                        modelAnimator.SetTrigger("Start Idling");
                    }
                }
                RotateTowardsTarget(LOOK_MOVEMENT);
            }
        }
    }

    void RotateTowardsTarget(int targetId)
    {
        Vector3 targetDirection = Vector3.zero;

        switch (targetId)
        {
            case LOOK_ENEMY: 
                targetDirection = nearestTarget.position - transform.position; 
                break;
            case LOOK_MOVEMENT: 
                targetDirection = movement.GetMovingDirection();
                if (targetDirection == Vector3.zero) return;
                break;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        playerBody.rotation = Quaternion.RotateTowards(playerBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        playerBody.eulerAngles = new Vector3(0, playerBody.eulerAngles.y, 0);

        isGenerallyLookingAtEnemy = (Vector3.Angle(playerBody.forward, targetDirection) < angleRelativeTargetLook);
    }

    public Transform GetNearestTarget()
    {
        return nearestTarget;
    }
}
