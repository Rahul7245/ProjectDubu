public class PlayingState : BaseApplicationState
{
    public PlayingState(StateMachine<ApplicationState> _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void OnEnter()
    {
        UnityEngine.Debug.Log("Entered Playing");
        EventBusModel.applicationStateEntered.Value =ApplicationState.PLAYING;
        EventBusModel.gameStart.Value = EventBusModel.playButtonClicked.Value;
        EventBusModel.gameOver.Subscribe(GameOver);
         EventBusModel.homeButton.Subscribe(OnHomeButtonClicked);
    }

    public override void OnExit()
    {
        EventBusModel.gameOver.Unsubscribe(GameOver);
        EventBusModel.applicationStateExited.Value =ApplicationState.PLAYING;
         EventBusModel.homeButton.Unsubscribe(OnHomeButtonClicked);
    }

    public void GameOver()
    {
        stateMachine.ChangeState(ApplicationState.GAMEOVER);
    }
    private void OnHomeButtonClicked()
    {
        stateMachine.ChangeState(ApplicationState.MENU);
    }

}
