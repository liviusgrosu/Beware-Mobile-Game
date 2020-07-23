using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttackingBehaviour : MonoBehaviour
{
    private PlayerMovement movement;
    private Transform playerBody;

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

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
        playerBody = transform.Find("Player Body");
        movement = GetComponent<PlayerMovement>();
        weaponController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesStillExist = enemyManager.GetClosestEnemy(transform.position) != null;

        if (movement.IsMoving())
        {
            if (weaponController.IsFiringWeapon()) weaponController.ToggleWeaponFire(false);
            //Find the nearest enemy when the player is moving 
            //This way its precalcuated and player doesn't switch
            if(enemiesStillExist) nearestTarget = enemyManager.GetClosestEnemy(transform.position);
            RotateTowardsTarget(LOOK_MOVEMENT);
        }
        else
        {
            if(enemiesStillExist)
            {
                //There needs to be a check if the enemy dies 
                //If thats the case, it needs to target a new enemy
                //However if there is no targets then just return from here 
                if (nearestTarget == null)
                    nearestTarget = enemyManager.GetClosestEnemy(transform.position);
                
                RotateTowardsTarget(LOOK_ENEMY);
                if (!weaponController.IsFiringWeapon()) weaponController.ToggleWeaponFire(true);
            }
            else
            {
                if (weaponController.IsFiringWeapon()) weaponController.ToggleWeaponFire(false);
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
    }
}
