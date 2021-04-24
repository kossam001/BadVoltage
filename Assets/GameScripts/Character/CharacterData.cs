using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    public AudioSource soundEffect;

    public GameObject damageEffect;
    public GameObject chargeEffect;

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
            healthSlider.value = health / maxHealth;

        if (chargeSlider)
            chargeSlider.value = charge / maxCharge;

        soundEffect = GetComponent<AudioSource>();
    }

    public void AddHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);

        healthSlider.value = health / maxHealth;

        if (!damageEffect.activeInHierarchy)
            damageEffect.SetActive(true);

        if (!IsInvoking(nameof(TurnOffEffect)))
            Invoke(nameof(TurnOffEffect), 1);

        if (health <= 0)
        {
            StageManager.Instance.RemoveFromTeam(this);

            if (tag == "Enemy")
                Destroy(gameObject);
            else if (tag == "Player" && !StageManager.Instance.stageClear)
            {
                StageManager.Instance.stageClear = true;
                StartCoroutine(StageManager.Instance.GameOver(false));
            }
        }
    }

    private void TurnOffEffect()
    {
        damageEffect.SetActive(false);
        chargeEffect.SetActive(false);
    }

    public void AddCharge(float value, bool activateEffect = true)
    {
        charge = Mathf.Clamp(charge + value, 0, maxCharge);

        chargeSlider.value = charge / maxCharge;

        if (!activateEffect) return;

        if (soundEffect != null && !soundEffect.isPlaying)
            soundEffect.Play();

        if (!chargeEffect.activeInHierarchy)
            chargeEffect.SetActive(true);

        if (!IsInvoking(nameof(TurnOffEffect)))
            Invoke(nameof(TurnOffEffect), 0.5f);
    }
}
