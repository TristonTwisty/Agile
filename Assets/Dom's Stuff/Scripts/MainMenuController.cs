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

    private void Start()
    {
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

        DomPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
        TristonPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
        RicPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
        BobbyPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
        ActiveState = State.ShowDom;
        MainMenuPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
    }

    [Header("Credits Panels")]
    [SerializeField] private GameObject DomPanel;
    [SerializeField] private GameObject TristonPanel;
    [SerializeField] private GameObject RicPanel; 
    [SerializeField] private GameObject BobbyPanel;
    private bool LastPanel;
    public enum State { ShowDom, ShowTriston, ShowRic, ShowBobby};
    public State ActiveState = State.ShowDom;

    public void ShowCredits()
    {
        ReturnToMainMenuButton.SetActive(true);
        NextButton.SetActive(true);

        switch (ActiveState)
        {
            case State.ShowDom:
                if (LastPanel)
                {
                    BobbyPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                }
                else
                {
                    MainMenuPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                }
                DomPanel.GetComponent<Animator>().Play("Panel Entrance");
                ActiveState = State.ShowTriston;
                LastPanel = false;
                break;
            case State.ShowTriston:
                DomPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                TristonPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
                ActiveState = State.ShowRic;
                break;
            case State.ShowRic:
                TristonPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                RicPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
                ActiveState = State.ShowBobby;
                break;
            case State.ShowBobby:
                RicPanel.GetComponent<Animator>().SetTrigger("Panel Exit");
                BobbyPanel.GetComponent<Animator>().SetTrigger("Panel Entrance");
                ActiveState = State.ShowDom;
                LastPanel = true;
                break;
        }
    }
}