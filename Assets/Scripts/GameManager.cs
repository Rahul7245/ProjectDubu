using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StateMachine<ApplicationState> applicationStateMachine;
    void Start()
    {
        InitApplicationStateMachine();
    }

    private void InitApplicationStateMachine()
    {
        applicationStateMachine = new();
        applicationStateMachine.AddState(ApplicationState.MENU, new MainMenuState());
        applicationStateMachine.AddState(ApplicationState.PLAYING, new MainMenuState());
        applicationStateMachine.AddState(ApplicationState.GAMEOVER, new MainMenuState());
        applicationStateMachine.ChangeState(ApplicationState.MENU);
    }
}
