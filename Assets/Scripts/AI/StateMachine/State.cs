using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateID
{
    Wander,
    Chase
}

[CreateAssetMenu(fileName = "State", menuName = "AIStateNodes/State")]
public class State : ScriptableObject
{
    [SerializeField] protected StateMachine stateMachine;
    [SerializeField] protected TreeNode rootNode;

    [Tooltip("Debug only")]
    [SerializeField] private TreeNode currentNode;
    [SerializeField] private Brain brain; // Going to need to know when to change states

    public StateID id;
    public List<State> transferrableStates;

    protected Dictionary<StateID, State> transition;

    public void Initialize(Brain _brain, StateMachine _stateMachine)
    {
        rootNode = Instantiate(rootNode);
        rootNode.Initialize(_brain, this);

        brain = _brain;
        stateMachine = _stateMachine;
    }

    public void SetCurrentNode(TreeNode node)
    {
        currentNode = node;
    }

    public virtual void Update()
    {
        rootNode.Run();
    }

    public virtual void ChangeState(StateID stateToChangeTo)
    {
        stateMachine.ChangeState(stateToChangeTo);
    }
}
