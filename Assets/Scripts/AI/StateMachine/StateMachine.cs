using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateID startState;
    public List<State> stateTemplates;
    private Dictionary<StateID, State> states;
    private Brain brain;

    [Tooltip("Debug")]
    [SerializeField] State currentState;

    private void Awake()
    {
        brain = GetComponent<Brain>();
        states = new Dictionary<StateID, State>();

        foreach (State state in stateTemplates)
        {
            State stateCopy = Instantiate(state);
            stateCopy.Initialize(brain, this);
            states.Add(stateCopy.id, stateCopy);
        }

        currentState = states[startState];
    }

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(StateID id)
    {
        currentState = states[id];
    }
}
