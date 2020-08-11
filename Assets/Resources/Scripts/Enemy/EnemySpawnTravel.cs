using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawnTravel : MonoBehaviour
{
    private Collider enemyCollider;
    //The body and movement


    private Vector3 start, destination;
    private float startTime;
    private float journeyLength;
    [SerializeField] private float movementSpeed;

    //Get all the enemy scripts

    private void Awake()
    {
        //Get all the enemy scripts
        enemyCollider = GetComponent<Collider>();
        if (enemyCollider == null) print("collider is null");
        enemyCollider.enabled = false;
    }

    public void StartTravel(Vector3 destination)
    {
        start = transform.position;
        this.destination = destination;
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, this.destination);
    }

    private void Update()
    {
        if(destination != null)
        {
            float distCovered = (Time.time - startTime) * movementSpeed;
            float journeyPerc = distCovered / journeyLength;

            transform.position = Vector3.Lerp(start, destination, Mathf.SmoothStep(0f, 1f, journeyPerc));


            if (Vector3.Distance(transform.position, destination) <= 0.01f)
            {
                GameObject.Find("Enemy Manager").GetComponent<EnemyManager>().AddEnemy(gameObject.GetInstanceID(), this.transform);
                enemyCollider.enabled = true;
                Destroy(this);
            }
        }
    }
}
