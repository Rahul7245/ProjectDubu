using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StateMachine<ApplicationState> applicationStateMachine;
    EventBusModel eventBusModel;

    void Awake()
    {
        InitEventBus();
    }
    void Start()
    {
        InitApplicationStateMachine();
    }

    private void InitApplicationStateMachine()
    {
        applicationStateMachine = new();
        applicationStateMachine.AddState(ApplicationState.MENU, new MainMenuState(applicationStateMachine));
        applicationStateMachine.AddState(ApplicationState.PLAYING, new PlayingState(applicationStateMachine));
       // applicationStateMachine.AddState(ApplicationState.GAMEOVER, new MainMenuState(applicationStateMachine));
        applicationStateMachine.ChangeState(ApplicationState.MENU);
    }

    void Update()
    {
        applicationStateMachine.Update();
    }

    private void InitEventBus()
    {
        eventBusModel = new();
        eventBusModel.Initialize();
    }

}
