using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "AITreeNodes/MoveToTarget")]
public class MoveToTarget : TreeNode
{
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");

    public override bool Run()
    {
        if (brain.agent.destination != brain.moveDestination)
        {
            CalcMovementAnimation();
            brain.agent.SetDestination(brain.moveDestination);
        }

        return base.Run();
    }

    public void CalcMovementAnimation()
    {
        Vector3 lookDirection = brain.character.transform.forward;
        Vector3 movementDirection = brain.agent.velocity;

        float lookToMoveAngle = Vector3.Angle(lookDirection, movementDirection);
        Vector3 angleSign = Vector3.Cross(lookDirection, movementDirection);

        movementDirection = movementDirection.normalized;

        movementDirection = Quaternion.Euler(brain.character.transform.rotation.eulerAngles) * movementDirection;

        brain.character.GetComponent<Animator>().SetFloat(MoveXHash, movementDirection.x);
        brain.character.GetComponent<Animator>().SetFloat(MoveZHash, movementDirection.z);
    }
}
