using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerHealthSystem playerHP;
    private IUIElement gameOverUI, scoreSystemUI, playerWeaponUI;

    private void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
        gameOverUI = GameObject.Find("Game End Screen").GetComponent<IUIElement>();
        scoreSystemUI = GameObject.Find("Score Manager").GetComponent<IUIElement>();
        playerWeaponUI = GameObject.Find("Weapon Player UI").GetComponent<IUIElement>();
    }

    private void Update()
    {
        if (playerHP.IsHealthEmpty())
        {
            gameOverUI.ToggleUI(true);
            scoreSystemUI.ToggleUI(false);
            playerWeaponUI.ToggleUI(false);
        }
    }
}
