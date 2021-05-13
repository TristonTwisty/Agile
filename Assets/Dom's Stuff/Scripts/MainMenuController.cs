using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private string levelName;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject ReturnToMainMenuButton;
    [SerializeField] private GameObject NextButton;

    [Header("Fade Controller")]
    [SerializeField] private Animator BlackoutPanel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        BlackoutPanel.SetTrigger("Fade In");
        MainMenuPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMainMenu()
    {
        ReturnToMainMenuButton.SetActive(false);
        NextButton.SetActive(false);

        TeamPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
        AssetPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
        ActiveState = State.ShowTeam;
        MainMenuPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
    }
    #region Credits
    [Header("Credits Panels")]
    [SerializeField] private GameObject TeamPanel;
    [SerializeField] private GameObject AssetPanel;
    private bool LastPanel;
    public enum State { ShowTeam, ShowAssets};
    public State ActiveState = State.ShowTeam;

    public void ShowCredits()
    {
        ReturnToMainMenuButton.SetActive(true);
        NextButton.SetActive(true);

        switch (ActiveState)
        {
            case State.ShowTeam:
                if (LastPanel)
                {
                    AssetPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                }
                else
                {
                    MainMenuPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                }
                TeamPanel.GetComponent<Animator>().Play("Panel Entrance");
                ActiveState = State.ShowAssets;
                LastPanel = false;
                break;
            case State.ShowAssets:
                TeamPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                AssetPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
                ActiveState = State.ShowTeam;
                LastPanel = true;
                break;
        }
    }
    #endregion
}