using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerHealthSystem playerHP;
    private IUIElement gameOverUI, gameWinUI, gamePauseUI; 
    private IUIElement scoreSystemUI, playerWeaponUI;

    private enum GameState
    { 
        Nil,
        Success,
        Failure,
        Paused
    }

    private GameState gameState;

    private void Awake()
    {
        gameState = GameState.Nil;
    }

    private void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
        gameOverUI = GameObject.Find("Game End Screen").GetComponent<IUIElement>();
        scoreSystemUI = GameObject.Find("Score Manager").GetComponent<IUIElement>();
        gameWinUI = GameObject.Find("Game Win Screen").GetComponent<IUIElement>();
        playerWeaponUI = GameObject.Find("Weapon Player UI").GetComponent<IUIElement>();
    }

    private void Update()
    {
        //Have a pause feature

        if (playerHP.IsHealthEmpty())
        {
            gameOverUI.ToggleUI(true);
            scoreSystemUI.ToggleUI(false);
            playerWeaponUI.ToggleUI(false);
        }

        // Temp: trigger the game win screen
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameWinUI.ToggleUI(true);
            scoreSystemUI.ToggleUI(false);
            playerWeaponUI.ToggleUI(false);
        }
    }
}
