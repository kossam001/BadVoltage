using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Root", menuName = "AITreeNodes/Root")]
public class TreeNode : ScriptableObject
{
    protected Brain brain;
    protected State state;
    public List<TreeNode> nodeTemplates;
    [SerializeField] protected List<TreeNode> nodes;

    public bool isSelector = false; // Otherwise it is a sequence

    public virtual void Initialize(Brain _brain, State _state)
    {
        // Each character will have its own nodes
        foreach (TreeNode node in nodeTemplates)
        {
            TreeNode nodeCopy = Instantiate(node);
            nodeCopy.Initialize(_brain, _state);
            nodes.Add(nodeCopy);
        }

        brain = _brain;
        state = _state;
    }

    public virtual bool PerformCheck()
    {
        return true;
    }

    public virtual bool Run()
    {
        foreach (TreeNode node in nodes)
        {
            if (!node.Run() ^ isSelector) return false;
        }
        return true;
    }
}
