public class MainMenuState : BaseApplicationState
{
    public MainMenuState(StateMachine<ApplicationState> _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void OnEnter()
    {
        UnityEngine.Debug.Log("Entered Main Menu");
        EventBusModel.playButtonClicked.Subscribe(OnPlayButtonClicked);
        EventBusModel.applicationStateEntered.Value =ApplicationState.MENU;
    }

    public override void OnExit()
    {
        UnityEngine.Debug.Log("Exit Main Menu");
        EventBusModel.playButtonClicked.Unsubscribe(OnPlayButtonClicked);
       EventBusModel.applicationStateExited.Value =ApplicationState.MENU;
    }

    private void OnPlayButtonClicked()
    {
        stateMachine.ChangeState(ApplicationState.PLAYING);
    }

}
