using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : AbstractMonoSingleton<UIManager>
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playingUI;
    [SerializeField] GameObject winPopUp;
    [SerializeField] GameObject losePopUp;

    private void Awake()
    {
        ShowMainMenu(true);
    }
    public void ShowMainMenu(bool isTrue)
    {
        mainMenu.SetActive(isTrue);
    }

    public void ShowPauseMenu(bool isTrue)
    {
        pauseMenu.SetActive(isTrue);
    }
    public void ShowPlayingUI(bool isTrue)
    {
        playingUI.SetActive(isTrue);
    }
    public void ShowWinPopUp(bool isTrue)
    {
        winPopUp.SetActive(isTrue);
    }
    public void ShowLosePopUp(bool isTrue)
    {
        losePopUp.SetActive(isTrue);
    }
}
