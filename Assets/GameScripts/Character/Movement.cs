using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Animator hashes
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private readonly int IsInAir = Animator.StringToHash("IsInAir");
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");

    private Rigidbody rigidbody;
    private Animator animator;

    [SerializeField]
    public float maxWalkSpeed;
    public float maxRunSpeed;
    
    public float movementSpeed;
    private float maxSpeed;
    public float rotationSpeed;
    public bool isRunning;

    // Jump
    public float jumpForce;
    public float maxJumpHeight;

    private float jumpTime;
    private Vector3 groundHeight;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 movementForce)
    {
        if (Vector3.Distance(movementForce, Vector3.zero) <= 0.01f)
        {
            rigidbody.velocity = Vector3.Scale(new Vector3(0,1,0), rigidbody.velocity);
        }
        else
        {
            maxSpeed = isRunning ? maxRunSpeed : maxWalkSpeed;
            animator.SetBool(IsRunningHash, isRunning);

            Vector3 clampedMoveVector = Vector3.ClampMagnitude(Vector3.Scale(new Vector3(1, 0, 1), rigidbody.velocity), maxSpeed);

            rigidbody.velocity = new Vector3(clampedMoveVector.x, rigidbody.velocity.y, clampedMoveVector.z);

            // Sum forward and side force
            rigidbody.AddForce(movementForce * movementSpeed);
        }
    }

    public void MovementCalculation(Vector2 movementDirection)
    {
        Vector3 forwardForce = transform.forward * movementDirection.y;
        Vector3 rightForce = transform.right * movementDirection.x;

        animator.SetFloat(MoveXHash, movementDirection.x);
        animator.SetFloat(MoveZHash, movementDirection.y);

        Move(forwardForce + rightForce);
    }

    public void Turn(Quaternion rotateDirection)
    {
        // Change character orientation based on camera rotation
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotationSpeed);
        Vector3 euler = Vector3.Scale(rotation.eulerAngles, new Vector3(0, 1, 0));
        transform.rotation = Quaternion.Euler(euler);
    }

    public void SetIsRunning(bool on)
    {
        isRunning = on;
    }

    public void Jump()
    {
        rigidbody.AddForce(new Vector3(0, jumpForce) * Time.deltaTime);
        jumpTime += Time.deltaTime;

        GetComponent<CharacterData>().AddCharge(-0.2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool(IsInAir, false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool(IsInAir, true);
        }
    }
}
