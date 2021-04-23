using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    public Slider healthSlider;
    public Slider chargeSlider;

    public int id;
    public bool isAttacking = false;

    private float maxHealth = 100;
    public float health = 100;
    private float maxCharge = 1000;
    public float charge = 500;

    public Team currentTeam;

    private void Awake()
    {
        if (healthSlider)
            AddHealth(0);

        if (chargeSlider)
            AddCharge(0);
    }

    public void AddHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);

        healthSlider.value = health / maxHealth;

        if (health <= 0)
        {
            StageManager.Instance.RemoveFromTeam(this);
            Destroy(gameObject);
        }
    }

    public void AddCharge(float value)
    {
        charge = Mathf.Clamp(charge + value, 0, maxCharge);

        chargeSlider.value = charge / maxCharge;
    }
}
