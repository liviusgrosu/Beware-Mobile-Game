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

    public bool changingScene;
    public bool stopAdvanceToNextLevel;

    private bool toggledWin, toggledLose;

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
        playerJoystickUI = GameObject.Find("Virtual Joystick Handler").GetComponent<IUIGenericElement>();

        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        //Have a pause feature

        if (playerHP.IsHealthEmpty() && !toggledLose)
        {
            toggledLose = true;
            gameOverUI.ToggleUI(true);
            ToggleInGameUI(false);
        }

        // Temp: trigger the game win screen
        if (exitDoor.playerFinished && !toggledWin)
        {
            toggledWin = true;
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
        int[] nextLevelParam = GetLevelIds();
        SceneManager.LoadScene($"World {nextLevelParam[0]} Level {nextLevelParam[1] + 1}");
    }

    public bool IsAnotherLevelAvailable()
    {
        if (stopAdvanceToNextLevel) return false;
        int[] nextLevelParam = GetLevelIds();
        return Application.CanStreamedLevelBeLoaded($"World {nextLevelParam[0]} Level {nextLevelParam[1] + 1}");
    }

    private int[] GetLevelIds()
    {
        List<String> leveParams = SceneManager.GetActiveScene().name.Split(' ').ToList();
        int worldId = int.Parse(leveParams.ElementAt(1));
        int levelId = int.Parse(leveParams.ElementAt(3));

        return new int[] { worldId, levelId };
    }
}
