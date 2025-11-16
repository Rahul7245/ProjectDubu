public class GameOverState : BaseApplicationState
{
    public GameOverState(StateMachine<ApplicationState> _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void OnEnter()
    {
        UnityEngine.Debug.Log("Entered GameOver");
        EventBusModel.applicationStateEntered.Value = ApplicationState.GAMEOVER;
        EventBusModel.score.Value = EventBusModel.gameOver.Value;
        EventBusModel.homeButton.Subscribe(OnHomeButtonClicked);
    }
    public override void OnExit()
    {
        EventBusModel.applicationStateExited.Value = ApplicationState.GAMEOVER;
        EventBusModel.homeButton.Unsubscribe(OnHomeButtonClicked);
    }

    private void OnHomeButtonClicked()
    {
        EventBusModel.playAudio.Value = AudioType.BUTTON;
        stateMachine.ChangeState(ApplicationState.MENU);
    }

}
