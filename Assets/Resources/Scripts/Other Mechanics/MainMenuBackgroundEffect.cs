using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackgroundEffect : MonoBehaviour
{
    public GameObject effectParticle;
    public float cycleTime;
    public List<Transform> spawnersWave1, spawnersWave2;

    private float currEffectTime;
    private bool currSpawners;


    // Start is called before the first frame update
    void Start()
    {
        currEffectTime = cycleTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currEffectTime >= cycleTime)
        {
            currEffectTime = 0f;
            currSpawners = !currSpawners;
            if (currSpawners) SpawnEffectInstant(spawnersWave1);
            else SpawnEffectInstant(spawnersWave2);
        }
        currEffectTime += Time.deltaTime;
    }

    private void SpawnEffectInstant(List<Transform> spawners)
    {
        foreach(Transform spawner in spawners)
        {
            GameObject instant = Instantiate(effectParticle, spawner.transform.position, effectParticle.transform.rotation);
            instant.transform.parent = transform;
            Destroy(instant, 10f);
        }
    }
}
