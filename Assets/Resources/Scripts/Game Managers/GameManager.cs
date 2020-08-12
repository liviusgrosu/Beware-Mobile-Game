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
    private ExitDoorCollider exitDoor;

    private IUIGenericElement gameOverUI, gameWinUI, gamePauseUI; 
    private IUIGenericElement scoreSystemUI, playerWeaponUI, playerJoystickUI, waveIndicatorUI;

    private string currentSceneName;

    [SerializeField]
    private string levelNamePrefix;

    public bool changingScene;

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
        changingScene = false;
    }

    private void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
        exitDoor = GameObject.Find("Exit Door Collider").GetComponent<ExitDoorCollider>();

        gameOverUI = GameObject.Find("Game End Screen").GetComponent<IUIGenericElement>();
        scoreSystemUI = GameObject.Find("Score Manager").GetComponent<IUIGenericElement>();
        waveIndicatorUI = GameObject.Find("Wave Indicator UI").GetComponent<IUIGenericElement>();

        gameWinUI = GameObject.Find("Game Win Screen").GetComponent<IUIGenericElement>();
        playerWeaponUI = GameObject.Find("Weapon Player UI").GetComponent<IUIGenericElement>();
        playerJoystickUI = GameObject.Find("Virtual Joystick Background").GetComponent<IUIGenericElement>();

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

    public void ToggleInGameUI(bool state)
    {
        scoreSystemUI.ToggleUI(state);
        playerWeaponUI.ToggleUI(state);
        playerJoystickUI.ToggleUI(state);
        waveIndicatorUI.ToggleUI(state);
    }

    public void RestartLevel()
    {
        changingScene = true;
        SceneManager.LoadScene(currentSceneName);
    }

    public void GoBackToMainMenu()
    {
        changingScene = true;
        SceneManager.LoadScene("MainMenuScene");
    }
    
    public void AdvanceLevel()
    {
        changingScene = true;
        int nextLevelId = GetLevelId() + 1;
        SceneManager.LoadScene($"{levelNamePrefix} {nextLevelId}");
    }

    public bool IsAnotherLevelAvailable()
    {
        int nextLevelId = GetLevelId() + 1;
        return Application.CanStreamedLevelBeLoaded($"{levelNamePrefix} {nextLevelId}");
    }

    private int GetLevelId()
    {
        string currSceneName = SceneManager.GetActiveScene().name;
        return int.Parse(currSceneName.Split().Last());
    }
}
