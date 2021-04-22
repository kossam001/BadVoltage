using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Transform billboardTransform;

    private void LateUpdate()
    {
        billboardTransform.LookAt(Camera.main.transform, -Vector3.up);
    }
}
