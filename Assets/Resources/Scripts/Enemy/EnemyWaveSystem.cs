using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class EnemyWaveSystem : MonoBehaviour
{
    private int waveCounter = 0;

    [SerializeField] private MulitDimensionalGO[] enemyWaves;

    private List<Transform> spawnPoints;
    private ExitDoorController exitDoorController;
    private EnemyWavesUI waveUI;

    private void Awake()
    {
        spawnPoints = new List<Transform>();
        foreach (Transform spawnPoint in transform.GetComponentsInChildren<Transform>())
            spawnPoints.Add(spawnPoint);

        spawnPoints.RemoveAt(0);
    }

    private void Start()
    {
        exitDoorController = GameObject.Find("Exit Door").GetComponent<ExitDoorController>();
        waveUI = GameObject.Find("Wave Indicator UI").GetComponent<EnemyWavesUI>();

        if (enemyWaves.Length <= 1) waveUI.ToggleUI(false);
    }

    public int? RequestWave()
    {
        waveCounter++;

        if (waveCounter > enemyWaves.Length)
        {
            exitDoorController.TriggerFullOpen();
            waveUI.UpdateUI("CLEAR");
            return null;
        }
        else exitDoorController.TriggerMomentOpen();

        waveUI.UpdateUI(waveCounter, enemyWaves.Length);

        int extraEntities = 0;
        foreach(GameObject entity in enemyWaves[waveCounter - 1].Objects)
        {
            EnemyVariantHandler objVarient = entity.GetComponent<EnemyVariantHandler>();
            if (objVarient != null && objVarient.containsVariant)
                extraEntities += objVarient.variantAmount;
        }

        StartCoroutine(SpawnWave(enemyWaves[waveCounter - 1]));
        return enemyWaves[waveCounter - 1].Objects.Length + extraEntities;
    }

    IEnumerator SpawnWave(MulitDimensionalGO wave)
    {
        for (int i = 0; i < wave.Objects.Length; i++)
        {
            GameObject enemy = Instantiate(wave.Objects[i], this.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemySpawnTravel>().StartTravel(spawnPoints.ElementAt(i % spawnPoints.Count).position);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public bool IsMoreWaves()
    {
        return !(waveCounter > enemyWaves.Length);
    }
}
