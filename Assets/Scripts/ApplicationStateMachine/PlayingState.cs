public class PlayingState : BaseApplicationState
{
    public PlayingState(StateMachine<ApplicationState> _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void OnEnter()
    {
        UnityEngine.Debug.Log("Entered Playing");
    }

}
