using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager Instance;

    [SerializeField] private CanvasGroup MenuPanel;
    [SerializeField] private CanvasGroup ConnectingPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeState(MainMenuStateType newState)
    {
        switch (newState)
        {
            case MainMenuStateType.MainMenu:
                MenuPanel.alpha = 1;
                MenuPanel.interactable = true;
                MenuPanel.blocksRaycasts = true;
                ConnectingPanel.alpha = 0;
                break;
            case MainMenuStateType.Connecting:
                MenuPanel.alpha = 0;
                MenuPanel.interactable = false;
                MenuPanel.blocksRaycasts = false;
                ConnectingPanel.alpha = 1;
                break;
        }
    }


    public void Connect()
    {
        ConnectionManager.Instance.Connect();
    }
}


public enum MainMenuStateType
{
    MainMenu = 0,
    Connecting = 1
}
