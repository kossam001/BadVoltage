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
                playerData.charge = Mathf.Max(playerData.charge - bullet.damage, 0);
            }
            else
            {
                playerData.AddHealth(-bullet.damage);
            }
        }
    }
}
