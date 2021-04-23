using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            if (ReferenceEquals(bullet.owner, gameObject)) return;

            GetComponent<CharacterData>().AddHealth(-other.gameObject.GetComponent<Bullet>().damage);
        }
    }
}
