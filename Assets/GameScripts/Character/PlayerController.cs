using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public float movementSpeed;
    public float rotationSpeed;
    public Camera cam;
    public GameObject character;
    private Movement movementComponent;
    private Attack attackComponent;
    public CharacterData characterData;

    private float lookDirection;
    
    private Animator characterAnimator;

    [SerializeField] private float forwardMagnitude;

    private Vector2 movementDirection;

    public bool isPaused = false;
    public bool isShiftOn = false;

    private float shootCooldown = 0.0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterAnimator = character.GetComponent<Animator>();
        movementComponent = character.GetComponent<Movement>();
        attackComponent = character.GetComponent<Attack>();
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if ((movementDirection.y != 0.0f || movementDirection.x != 0.0f))
        {
            Turn();
        }

        movementComponent.MovementCalculation(movementDirection);
        movementComponent.SetIsRunning(isShiftOn);

        shootCooldown -= Time.deltaTime;
    }

    public void OnMovement(InputValue vector2)
    {
        if (isPaused) return;

        movementDirection = vector2.Get<Vector2>();
    }

    public override void Turn()
    {
        movementComponent.Turn(cam.transform.rotation);
    }

    public void OnAttack(InputValue button)
    {
        if (isPaused) return;
        if (shootCooldown >= 0.0f) return;
        
        shootCooldown = 0.5f;

        Invoke(nameof(Shoot), 0.2f);
    }

    private void Shoot()
    {
        attackComponent.Shoot();
    }

    public void OnPause()
    {
        UIManager.Instance.TogglePause();
    }

    public void OnShift(InputValue button)
    {
        isShiftOn = button.isPressed;
    }
}
