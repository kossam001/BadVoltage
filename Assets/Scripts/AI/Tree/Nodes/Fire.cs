using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "AITreeNodes/Fire")]
public class Fire : TreeNode
{
    public float firerate;
    public float timer;

    public override void Initialize(Brain _brain, State _state)
    {
        timer = Random.Range(1.0f, 5.0f);

        base.Initialize(_brain, _state);
    }

    public override bool Run()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = Random.Range(1.0f, firerate);
            brain.character.GetComponent<Attack>().Shoot();
        }

        return base.Run();
    }
}
