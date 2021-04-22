using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isValid;

    private void OnCollisionEnter(Collision other)
    {
        isValid = false;
    }

    private void OnCollisionExit(Collision other)
    {
        isValid = true;
    }

    public GameObject SpawnObject(GameObject obj)
    {
        if (!isValid) return null;

        return Instantiate(obj, transform.position, transform.rotation);
    }
}
