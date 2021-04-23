using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int id;
    public int maxHealth = 1000;
    public int health = 500;
    public bool isAttacking = false;

    public float maxCharge;
    public float charge;

    public Team currentTeam;

    public void AddHealth(int value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);

        if (health <= 0)
        {
            StageManager.Instance.RemoveFromTeam(this);
            Destroy(gameObject);
        }
    }
}
