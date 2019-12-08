using System.Diagnostics;

public class StateMachine<TEntityType>
{
    //a pointer to the agent that owns this instance
    TEntityType _owner;

    State<TEntityType> _currentState;

    //a record of the last state the agent was in
    State<TEntityType> _previousState;

    //this is called every time the FSM is updated
    State<TEntityType> _globalState;

    public StateMachine(TEntityType owner)
    {
        this._owner = owner;
        this._currentState = null;
        this._previousState = null;
        this._globalState = null;
    }

    //call this to update the FSM
    public void Update()
    {
        //if a global state exists, call its execute method, else do nothing
        _globalState?.Execute(_owner);

        //same for the current state
        _currentState?.Execute(_owner);
    }

    public bool HandleMessage(Telegram telegram)
    {
        if (_currentState != null && _currentState.OnMessage(_owner, telegram))
        {
            return true;
        }

        if (_globalState != null && _globalState.OnMessage(_owner, telegram))
        {
            return true;
        }

        return false;
    }

    //change to a new state
    public void ChangeState(State<TEntityType> newState)
    {
        //make sure both states are both valid before attempting to 
        //call their methods
        Debug.Assert(newState != null, "<StateMachine::ChangeState>: trying to change to a null state");

        //keep a record of the previous state
        _previousState = _currentState;

        //call the exit method of the existing state
        _currentState.Exit(_owner);

        //change state to the new state
        _currentState = newState;

        //call the entry method of the new state
        _currentState.Enter(_owner);
    }

    //change state back to the previous state
    public void RevertToPreviousState()
    {
        ChangeState(_previousState);
    }

    public State<TEntityType> CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public State<TEntityType> PreviousState
    {
        get => _previousState;
        set => _previousState = value;
    }

    public State<TEntityType> GlobalState
    {
        get => _globalState;
        set => _globalState = value;
    }
}