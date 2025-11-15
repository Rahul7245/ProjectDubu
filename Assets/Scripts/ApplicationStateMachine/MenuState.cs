public class MainMenuState : BaseApplicationState
{
    public MainMenuState(StateMachine<ApplicationState> _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void OnEnter()
    {
        UnityEngine.Debug.Log("Entered Main Menu");
    }

}
