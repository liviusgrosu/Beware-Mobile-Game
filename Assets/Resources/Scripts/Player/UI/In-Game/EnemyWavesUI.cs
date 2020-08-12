using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWavesUI : MonoBehaviour, IUIGenericElement
{
    private EnemyWaveSystem waveSystem;
    private bool isUIActive;

    public Text waveText;

    private void Start()
    {
        waveSystem = GameObject.Find("Enemy Wave System").GetComponent<EnemyWaveSystem>();
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
    }

    public void UpdateUI(int wave, int maxWave)
    {
        waveText.text = $"{wave} / {maxWave}";
    }

    public void UpdateUI(string text)
    {
        waveText.text = text;
    }
}
