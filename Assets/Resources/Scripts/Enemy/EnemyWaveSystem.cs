using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class EnemyWaveSystem : MonoBehaviour
{
    [SerializeField]
    private bool debugDisableWaves;

    private int waveCounter = 0;

    [SerializeField] private EnemyWaveObject[] enemyWaves;

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
        exitDoorController = GameObject.FindWithTag("Exit Door").GetComponent<ExitDoorController>();
        waveUI = GameObject.Find("Wave Indicator UI").GetComponent<EnemyWavesUI>();

        if (enemyWaves.Length <= 1) waveUI.ToggleUI(false);
    }

    public int? RequestWave()
    {
        waveCounter++;

        if (waveCounter > enemyWaves.Length || debugDisableWaves)
        {
            exitDoorController.TriggerFullOpen();
            waveUI.UpdateUI("CLEAR");
            return null;
        }
        else exitDoorController.TriggerMomentOpen();

        waveUI.UpdateUI(waveCounter, enemyWaves.Length);

        int extraEntities = 0;
        foreach(EnemyInstaParams entity in enemyWaves[waveCounter - 1].enemy)
            if (entity.variantAmount > 0) extraEntities += entity.variantAmount;

        StartCoroutine(SpawnWave(enemyWaves[waveCounter - 1]));
        return enemyWaves[waveCounter - 1].enemy.Length + extraEntities;
    }

    IEnumerator SpawnWave(EnemyWaveObject wave)
    {
        for(int i = 0; i < enemyWaves[waveCounter - 1].enemy.Length; i++)
        {
            EnemyInstaParams entity = enemyWaves[waveCounter - 1].enemy[i];

            GameObject enemy = Instantiate(entity.enemyObj, this.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyVariantHandler>().containsVariant = entity.variantAmount > 0;
            enemy.GetComponent<EnemyVariantHandler>().variantAmount = entity.variantAmount;
            enemy.GetComponent<EnemyVariantHandler>().varientPrefab = entity.variantObject;

            enemy.GetComponent<EnemyDropHandler>().weaponDrop = entity.weaponDrop;
            enemy.GetComponent<EnemyDropHandler>().coinAmount = entity.coinDropAmount;
            enemy.GetComponent<EnemyDropHandler>().miscDrops = entity.miscDrop;

            if(enemy.GetComponent<WeaponController>() != null)
                enemy.GetComponent<WeaponController>().startingWeapon = entity.startingWeapon;

            enemy.GetComponent<EnemySpawnTravel>().StartTravel(spawnPoints.ElementAt(i % spawnPoints.Count).position);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public bool IsMoreWaves()
    {
        return !(waveCounter > enemyWaves.Length);
    }
}
