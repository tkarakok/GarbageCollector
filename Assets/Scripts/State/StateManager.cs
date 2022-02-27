using UnityEngine;

public enum State
{
    MainMenu,
    InGame,
    EndGame,
    GameOver
}

public class StateManager : Singleton<StateManager>
{
    private State _state;

    public State State { get => _state; set => _state = value; }

    private void Start()
    {
        _state = State.MainMenu;
    }
}
