using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform FollowTarget;
    [SerializeField] private float RotationSpeed = 1.0f;
    [SerializeField] private float HorizontalDamping = 1.0f;
    
    private Vector2 PreviousMouseInput;

    // Start is called before the first frame update
    void Start()
    {
        PreviousMouseInput = Vector2.zero;
    }

    public void OnLook(InputValue delta)
    {
        if (UIManager.Instance.pauseMenu.activeInHierarchy) return;

        Vector2 aimValue = delta.Get<Vector2>();

        FollowTarget.rotation *=
            Quaternion.AngleAxis(
                    Mathf.Lerp(PreviousMouseInput.x, aimValue.x, 1.0f / HorizontalDamping) * RotationSpeed,
                    transform.up
                );

        transform.rotation = Quaternion.Euler(0, FollowTarget.rotation.eulerAngles.y, 0);

        PreviousMouseInput = aimValue;
    }
}
