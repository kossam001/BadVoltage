using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Animator hashes
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");
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
    public bool canJump;
    public float jumpForce;
    public float maxJumpHeight;

    private float jumpTime;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 movementForce)
    {
        if (Vector3.Distance(movementForce, Vector3.zero) <= 0.01f)
        {
            rigidbody.velocity = Vector3.zero;
        }
        else
        {
            maxSpeed = isRunning ? maxRunSpeed : maxWalkSpeed;
            animator.SetBool(IsRunningHash, isRunning);

            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);

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
        if (canJump)
        {
            rigidbody.AddForce(new Vector3(0, jumpForce) * Time.deltaTime);
            jumpTime += Time.deltaTime;
        }

        if (jumpTime * rigidbody.velocity.magnitude >= maxJumpHeight)
        {
            canJump = false;
        }
    }

    public void StopJump()
    {
        canJump = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            canJump = true;
    }
}
