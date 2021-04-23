using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int id;
    public int maxHealth = 100;
    public int health = 100;
    public bool isAttacking = false;

    public float maxCharge = 1000;
    public float charge = 500;

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
