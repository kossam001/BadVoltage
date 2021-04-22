using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCharger : Charger
{
    protected override void Awake()
    {
        currentCapacity = 0;
        chargeCapacityDisplay.value = 0;
    }

    protected override void Update()
    {
        // Charging target
        if (isCharging && !isDown)
        {
            target.charge += chargeStrength;
            currentCapacity -= chargeStrength;

            chargeCapacityDisplay.value = currentCapacity / maxChargeCapacity;

            if (currentCapacity >= maxChargeCapacity)
            {
                Debug.Log("Door Charged");
            }
        }
    }
}
