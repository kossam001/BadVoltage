using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance;
    public static BulletManager Instance { get { return instance; } }

    public GameObject bulletTemplate;
    private Queue<GameObject> bullets;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        bullets = new Queue<GameObject>();

        for (int i = 0; i < 100; i++)
        {
            GameObject copyBullet = Instantiate(bulletTemplate);
            copyBullet.SetActive(false);
            copyBullet.transform.parent = transform;

            bullets.Enqueue(copyBullet);
        }
    }

    public GameObject GetBullet()
    {
        GameObject bullet;

        if (bullets.Count > 0)
            bullet = bullets.Dequeue();

        else
        {
            bullet = Instantiate(bulletTemplate);
        }

        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}
