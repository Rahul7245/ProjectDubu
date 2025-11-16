public class BaseApplicationState : IState
{
    protected StateMachine<ApplicationState> stateMachine;
    public BaseApplicationState(StateMachine<ApplicationState> _stateMachine)
    {
        stateMachine = _stateMachine;
    }
    public virtual void OnEnter()
    {
    }

    public virtual void OnUpdate() { }

    public virtual void OnExit()
    {
    }
}
