public class GameOverState : IState
{
    public void OnEnter()
    {
        UnityEngine.Debug.Log("Game Over");
    }

    public void OnUpdate() { }

    public void OnExit() { }
}
