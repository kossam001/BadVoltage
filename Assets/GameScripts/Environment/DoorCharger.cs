using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCharger : Charger
{
    private readonly int IsAttackingHash = Animator.StringToHash("IsClosed");

    public Animator doorAnimator;

    protected override void Awake()
    {
        currentCapacity = 0;
        chargeCapacityDisplay.value = 0;
    }

    protected override void Update()
    {
        if (StageManager.Instance.isPaused) return;

        // Charging target
        if (isCharging && !isDown)
        {
            target.AddCharge(chargeStrength);
            currentCapacity -= chargeStrength;

            chargeCapacityDisplay.value = currentCapacity / maxChargeCapacity;

            if (currentCapacity >= maxChargeCapacity)
            {
                if (!StageManager.Instance.stageClear)
                {
                    StageManager.Instance.stageClear = true;

                    StartCoroutine(StageManager.Instance.GameOver(true));
                }

                doorAnimator.SetBool(IsAttackingHash, true);
            }
        }
    }
}
