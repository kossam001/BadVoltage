using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;

    [SerializeField]
    private float maxSpeed;
    public float movementSpeed;
    public float rotationSpeed;

    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 movementForce)
    {
        Vector3 lookDirection = transform.forward;
        Vector3 movementDirection = rigidbody.velocity;

        float lookToMoveAngle = Vector3.Angle(lookDirection, movementDirection);
        Vector3 angleSign = Vector3.Cross(lookDirection, movementDirection);

        // Sum forward and side force
        rigidbody.AddForce(movementForce * movementSpeed * Time.deltaTime);

        // Clamp velocity
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);

        if (Mathf.Abs(movementDirection.x) > 0.1f || Mathf.Abs(movementDirection.z) > 0.1f)
        {
            movementDirection = movementDirection.normalized;
            animator.SetFloat(MoveXHash, movementDirection.x);
            animator.SetFloat(MoveZHash, movementDirection.z);
        }
        else
        {
            animator.SetFloat(MoveXHash, 0.0f);
            animator.SetFloat(MoveZHash, 0.0f);
        }
    }

    public void Turn(Quaternion rotateDirection)
    {
        // Change character orientation based on camera rotation
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotationSpeed);
        Vector3 euler = Vector3.Scale(rotation.eulerAngles, new Vector3(0, 1, 0));
        transform.rotation = Quaternion.Euler(euler);
    }
}
