using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAdvancedFiring : MonoBehaviour
{
    private EnemyProjectileAdvancedMovement movement;
    private Transform playerTransform;

    private WeaponController weaponController;

    [SerializeField]
    private float rotationSpeed = 360f;

    const int LOOK_MOVEMENT = 1;
    const int LOOK_ENEMY = 2;

    private void Awake()
    {
        movement = GetComponent<EnemyProjectileAdvancedMovement>();
        weaponController = GetComponent<WeaponController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.behaviorState == EnemyProjectileAdvancedMovement.BehaviourState.Moving)
        {
            if (weaponController.IsFiringWeapon()) weaponController.ToggleWeaponFire(false);
        }
        else
        {
            RotateTowardsTarget(LOOK_ENEMY);
            if (!weaponController.IsFiringWeapon()) weaponController.ToggleWeaponFire(true);
        }
    }

    void RotateTowardsTarget(int targetId)
    {
        Vector3 targetDirection = Vector3.zero;

        switch (targetId)
        {
            case LOOK_ENEMY:
                targetDirection = playerTransform.position - transform.position;
                break;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
