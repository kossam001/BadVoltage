using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEffect : MonoBehaviour
{
    public CharacterData target;

    public bool isCharging;
    public float chargeStrength;

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

    private void Update()
    {
        if (isCharging)
        {
            target.charge += chargeStrength;
        }
    }
}
