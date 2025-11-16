using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuView;
    [SerializeField] private GameObject gameView;
    [SerializeField] private GameObject gameOverView;
    void Start()
    {
        EventBusModel.applicationStateEntered.Subscribe(OnNewStateEntered);
        EventBusModel.applicationStateExited.Subscribe(OnStateExited);
    }
    void OnApplicationQuit()
    {
        EventBusModel.applicationStateEntered.Unsubscribe(OnNewStateEntered);
        EventBusModel.applicationStateExited.Unsubscribe(OnStateExited);
    }
    private void OnNewStateEntered()
    {
        switch (EventBusModel.applicationStateEntered.Value)
        {
            case ApplicationState.MENU:
            menuView.gameObject.SetActive(true);
            break;
            case ApplicationState.PLAYING:
            gameView.gameObject.SetActive(true);
            break;
            case ApplicationState.GAMEOVER:
            gameOverView.gameObject.SetActive(true);
            break;
            default:
            break;
        }
    }

     private void OnStateExited()
    {
        switch (EventBusModel.applicationStateExited.Value)
        {
            case ApplicationState.MENU:
            menuView.gameObject.SetActive(false);
            break;
            case ApplicationState.PLAYING:
            gameView.gameObject.SetActive(false);
            break;
            case ApplicationState.GAMEOVER:
            gameOverView.gameObject.SetActive(false);
            break;
            default:
            break;
        }
    }
}
