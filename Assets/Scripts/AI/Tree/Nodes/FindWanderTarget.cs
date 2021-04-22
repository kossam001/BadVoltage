using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindWanderTarget", menuName = "AITreeNodes/FindWanderTarget")]
public class FindWanderTarget : TreeNode
{
    public float wanderRange = 10.0f;
    public float duration = 10.0f;
    public float timer = 0.0f;

    public override bool Run()
    {
        timer -= Time.deltaTime;

        if (!brain.agent.hasPath || brain.agent.remainingDistance <= 0.1f)
            brain.moveDestination = new Vector3(Random.Range(-wanderRange, wanderRange), 0.0f, Random.Range(-wanderRange, wanderRange));

        else if (timer <= 0.0f)
        {
            timer = duration;
            state.ChangeState(StateID.Chase);
            return false;
        }

        return base.Run();
    }
}
