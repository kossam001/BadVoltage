using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    public GameObject particleEffect;
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
        if (other.CompareTag("Player"))
        {
            target = other.GetComponent<CharacterData>();
            isCharging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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
            target.AddCharge(chargeStrength);
            currentCapacity -= chargeStrength;

            if (currentCapacity <= 0)
            {
                // Overcharged
                isDown = true;
                isCharging = false;
                target = null;
            }
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
