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
    }

    public override void OnExit()
    {
        EventBusModel.gameOver.Unsubscribe(GameOver);
        EventBusModel.applicationStateExited.Value =ApplicationState.PLAYING;
    }

    public void GameOver()
    {
        stateMachine.ChangeState(ApplicationState.GAMEOVER);
    }

}
