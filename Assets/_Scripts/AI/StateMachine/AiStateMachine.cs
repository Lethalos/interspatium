using UnityEngine;

public class AiStateMachine : MonoBehaviour
{
    private IState currentState;

    public void SetState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }
}

public interface IState
{
    void OnEnter();
    void Tick();
    void OnExit();
}
