using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public Transform bulletSpawnPoint;
    private CharacterData data;

    public float force = 1000.0f;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
    }

    public void Shoot()
    {
        if (GetComponent<Animator>().GetBool(IsAttackingHash)) return;
        GetComponent<Animator>().SetBool(IsAttackingHash, true);
        Invoke(nameof(Fire), 0.2f);
    }

    private void Fire()
    {

    }
}
