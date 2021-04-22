using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "AITreeNodes/Chase")]
public class ChaseTarget : TreeNode
{
    public override bool PerformCheck()
    {
        if (brain.activeTarget == null)
        {
            brain.agent.ResetPath();
            state.ChangeState(StateID.Wander);
            return false;
        }

        Vector3 targetPosition = brain.activeTarget.transform.position;
        Vector3 selfPosition = brain.character.transform.position;

        float distance = Vector3.Distance(targetPosition, selfPosition);

        if (distance > brain.combatRange * 0.5f)
        {
            brain.moveDestination = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));
        }
        else
        {
            brain.agent.ResetPath();
            state.ChangeState(StateID.Wander);

            return false;
        }

        return true;
    }

    public override bool Run()
    {
        if (!PerformCheck()) return false;

        return base.Run();
    }
}
