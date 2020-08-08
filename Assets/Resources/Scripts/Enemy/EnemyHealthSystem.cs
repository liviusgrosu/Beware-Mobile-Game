using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    public bool godMode;

    public int maxHP = 1;
    private int currHP;

    private EnemyManager enemyManager;
    private EnemyDropHandler dropHandler;
    private EnemyVariantHandler variantHandler;

    private SoundController audioController;

    [SerializeField]
    private GameObject healthCellUI;
    private List<GameObject> healthBar;
    private Transform canvas;

    [SerializeField]
    private Sprite fullHPImg, emptyHPImg;

    //Changing x pos
    private float cellSpacing = 0.05f;
    private float cellWidth;

    private void Awake()
    {
        dropHandler = GetComponent<EnemyDropHandler>();
        variantHandler = GetComponent<EnemyVariantHandler>();


        canvas = transform.GetChild(0);
        healthBar = new List<GameObject>();
        cellWidth = healthCellUI.GetComponent<RectTransform>().rect.width;

        currHP = maxHP;

        InitHealthUI();
    }

    private void Start()
    {
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
        audioController = GameObject.Find("Sound Controller").GetComponent<SoundController>();

        audioController.PlayEnemySpawnSound();
    }

    private void InitHealthUI()
    {
        float lastXPos = 0f;
        for (int i = 0; i < maxHP; i++)
        {
            GameObject cell = Instantiate(healthCellUI, canvas.transform.position, canvas.rotation);
            cell.transform.parent = canvas;
            healthBar.Add(cell);

            if (i == 0)
            {
                float startingPos = ((2 + maxHP - 3) * cellWidth / 2) + ((1 + maxHP - 3) * cellSpacing / 2);
                cell.GetComponent<RectTransform>().localPosition = new Vector3(-startingPos, 0f, 0f);
            }
            else
            {
                cell.GetComponent<RectTransform>().localPosition = new Vector3(lastXPos + cellSpacing + cellWidth, 0f, 0f);
            }

            lastXPos = cell.GetComponent<RectTransform>().localPosition.x;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (godMode) return;

        currHP += amount;

        if (currHP <= 0)
        {
            audioController.PlayEnemyDeathSound();
            dropHandler.DropLoot();
            variantHandler.SpawnVariant();
            enemyManager.RemoveEnemy(gameObject.GetInstanceID());
        }
        else if (currHP > maxHP)
            currHP = maxHP;

        for(int i = 0; i < maxHP; i++)
        {
            if (i < currHP) healthBar.ElementAt(i).GetComponent<Image>().sprite = fullHPImg;
            else healthBar.ElementAt(i).GetComponent<Image>().sprite = emptyHPImg;
        }
    }
}
