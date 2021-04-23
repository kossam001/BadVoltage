using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionComponent : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            CharacterData playerData = GetComponent<CharacterData>();
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            if (playerData.charge > 0)
            {
                playerData.AddCharge(-bullet.damage);
            }
            else
            {
                playerData.AddHealth(-bullet.damage);
            }
        }
    }
}
