public class GameOverState : BaseApplicationState
{
    public GameOverState(StateMachine<ApplicationState> _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void OnEnter()
    {
        UnityEngine.Debug.Log("Entered GameOver");
    }

}
