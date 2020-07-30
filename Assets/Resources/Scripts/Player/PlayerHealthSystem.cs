using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{

    public bool godMode;

    public int maxHP = 1;
    private int currHP;

    [SerializeField] private GameObject healthCellUI;
    private List<GameObject> healthBar;
    private Transform canvas;

    [SerializeField] private Sprite fullHPImg, emptyHPImg;

    //Changing x pos
    private float cellSpacing = 0.05f;
    private float cellWidth;

    [Space(5)]
    [Header("Invincible Frames")]
    [SerializeField] private Material regularMat;
    [SerializeField] private Material invincibleMat;
    [SerializeField] private MeshRenderer rend;

    [SerializeField] private float invincibleTime;
    private bool isInvincible;

    private void Start()
    {
        canvas = transform.GetChild(0);
        healthBar = new List<GameObject>();
        cellWidth = healthCellUI.GetComponent<RectTransform>().rect.width;

        currHP = maxHP;

        InitHealthUI();
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
        if (godMode || isInvincible) return;

        if (currHP > 0) currHP += amount;

        if (currHP <= 0) Die();
        else if (currHP > maxHP) currHP = maxHP;

        for (int i = 0; i < maxHP; i++)
        {
            if (i < currHP) healthBar.ElementAt(i).GetComponent<Image>().sprite = fullHPImg;
            else healthBar.ElementAt(i).GetComponent<Image>().sprite = emptyHPImg;
        }

        if(amount < 0) StartCoroutine(StayInvinsible());
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttackingBehaviour>().enabled = false;
    }

    public bool IsHealthFull()
    {
        return currHP == maxHP;
    }

    public bool IsHealthEmpty()
    {
        return currHP <= 0;
    }

    private IEnumerator StayInvinsible()
    {
        rend.material = invincibleMat;
        isInvincible = true;
        gameObject.layer = LayerMask.NameToLayer("Invincible Player");

        yield return new WaitForSeconds(invincibleTime);

        rend.material = regularMat;
        isInvincible = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
