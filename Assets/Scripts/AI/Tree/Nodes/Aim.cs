using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aim", menuName = "AITreeNodes/Aim")]
public class Aim : TreeNode
{
    public override bool Run()
    {
        if (brain.activeTarget != null)
        {
            brain.character.transform.LookAt(brain.activeTarget.transform.position);
        }

        return base.Run();
    }
}
