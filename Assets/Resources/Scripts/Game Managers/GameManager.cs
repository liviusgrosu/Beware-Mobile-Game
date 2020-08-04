using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerHealthSystem playerHP;
    private ExitDoor exitDoor;

    private IUIElement gameOverUI, gameWinUI, gamePauseUI; 
    private IUIElement scoreSystemUI, playerWeaponUI, playerJoystickUI;

    private string currentSceneName;

    [SerializeField]
    private string levelNamePrefix;

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
        exitDoor = GameObject.Find("Exit Door").GetComponent<ExitDoor>();

        gameOverUI = GameObject.Find("Game End Screen").GetComponent<IUIElement>();
        scoreSystemUI = GameObject.Find("Score Manager").GetComponent<IUIElement>();
        gameWinUI = GameObject.Find("Game Win Screen").GetComponent<IUIElement>();
        playerWeaponUI = GameObject.Find("Weapon Player UI").GetComponent<IUIElement>();
        playerJoystickUI = GameObject.Find("Virtual Joystick Background").GetComponent<IUIElement>();

        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        //Have a pause feature

        if (playerHP.IsHealthEmpty())
        {
            gameOverUI.ToggleUI(true);
            ToggleInGameUI(false);
        }

        // Temp: trigger the game win screen
        if (exitDoor.playerFinished)
        {
            gameWinUI.ToggleUI(true);
            ToggleInGameUI(false);
        }
    }

    private void ToggleInGameUI(bool state)
    {
        scoreSystemUI.ToggleUI(state);
        playerWeaponUI.ToggleUI(state);
        playerJoystickUI.ToggleUI(state);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    
    public void AdvanceLevel()
    {
        try
        {
            string currSceneName = SceneManager.GetActiveScene().name;
            int levelId = int.Parse(currSceneName.Split().Last()) + 1;
            SceneManager.LoadScene($"{levelNamePrefix} {levelId}");
        }
        catch(Exception e)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
