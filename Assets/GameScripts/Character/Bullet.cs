using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float duration;
    public GameObject owner;
    public int damage;

    private void OnEnable()
    {
        StartCoroutine(DespawnTimer());
    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(duration);
        owner = null;
        BulletManager.Instance.ReturnBullet(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.CompareTag("Character") || other.gameObject.CompareTag("Player")) && !ReferenceEquals(other.gameObject, owner))
        {
            owner = null;
            BulletManager.Instance.ReturnBullet(gameObject);
        }
    }
}
