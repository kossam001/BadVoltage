using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    public GameObject chargeEffect;
    public Slider chargeCapacityDisplay;
    public CharacterData target;

    [SerializeField] protected bool isCharging;
    [SerializeField] protected bool isDown;
    [SerializeField] protected float chargeStrength;
    [SerializeField] protected float maxChargeCapacity;
    [SerializeField] protected float maxDownTime;

    [SerializeField] protected float currentCapacity;
    [SerializeField] protected float currentDownTime;

    protected virtual void Awake()
    {
        currentCapacity = maxChargeCapacity;
        currentDownTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            target = other.GetComponent<CharacterData>();
            isCharging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            target = null;
            isCharging = false;
        }
    }

    protected virtual void Update()
    {
        if (StageManager.Instance.isPaused) return;

        // Charging target
        if (isCharging && !isDown)
        {
            if (target == null)
            {
                isCharging = false;
                return;
            }

            if (target.CompareTag("Player"))
                target.AddCharge(chargeStrength);

            else if (target.CompareTag("Enemy"))
                target.AddHealth(-chargeStrength);

            currentCapacity -= chargeStrength;

            chargeEffect.SetActive(true);

            if (currentCapacity <= 0)
            {
                // Overcharged
                isDown = true;
                isCharging = false;
                target = null;
            }
        }
        else
        {
            if (chargeEffect.activeInHierarchy)
                chargeEffect.SetActive(false);
        }

        // Refilling
        if (!isCharging && !isDown && currentCapacity < maxChargeCapacity)
        {
            currentCapacity = Mathf.Min(currentCapacity + chargeStrength, maxChargeCapacity);
        }

        // Is down
        if (isDown)
        {
            currentDownTime += Time.deltaTime;

            if (currentDownTime >= maxDownTime)
            {
                isDown = false;
                currentDownTime = 0;
            }
        }

        // Update UI
        if (currentCapacity < maxChargeCapacity)
        {
            chargeCapacityDisplay.value = currentCapacity / maxChargeCapacity;
        }
    }
}
