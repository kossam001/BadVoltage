using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindActiveTarget", menuName = "AITreeNodes/FindActiveTarget")]
public class FindActiveTarget : TreeNode
{
    [Tooltip("How long to target this target")]
    public float duration;

    [Header("Debug")]
    public GameObject activeTarget;
    [SerializeField] private float timer;

    public override bool Run()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            brain.enemyTargets = Gameplay.Instance.GetEnemies(brain.character.GetComponent<CharacterData>().currentTeam);

            if (brain.enemyTargets.Count > 0)
            {
                brain.activeTarget = brain.enemyTargets[Random.Range(0, brain.enemyTargets.Count)];
                activeTarget = brain.activeTarget;
            }
            else
            {
                brain.activeTarget = null;
                brain.agent.ResetPath();
                state.ChangeState(StateID.Wander);
                return false;
            }

            timer = duration;
        }

        return base.Run();
    }
}
