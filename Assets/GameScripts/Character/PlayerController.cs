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

    public bool isShiftOn = false;
    public bool isJumpPressed = false;

    private float shootCooldown = 0.0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterData = character.GetComponent<CharacterData>();
        characterAnimator = character.GetComponent<Animator>();
        movementComponent = character.GetComponent<Movement>();
        attackComponent = character.GetComponent<Attack>();
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if (StageManager.Instance.isPaused) return;

        if ((movementDirection.y != 0.0f || movementDirection.x != 0.0f))
        {
            Turn();
        }

        if (isJumpPressed)
        {
            movementComponent.Jump();
        }

        movementComponent.MovementCalculation(movementDirection);
        movementComponent.SetIsRunning(isShiftOn);

        shootCooldown -= Time.deltaTime;
    }

    public void OnMovement(InputValue vector2)
    {
        if (StageManager.Instance.isPaused) return;

        movementDirection = vector2.Get<Vector2>();

        characterData.AddCharge(-0.1f, false);
    }

    public override void Turn()
    {
        if (StageManager.Instance.isPaused) return;

        movementComponent.Turn(cam.transform.rotation);

        characterData.AddCharge(-0.1f, false);
    }

    public void OnAttack(InputValue button)
    {
        if (StageManager.Instance.isPaused) return;
        if (shootCooldown >= 0.0f) return;
        if (characterData.charge <= 0.0f) return;
        
        shootCooldown = 0.5f;

        Invoke(nameof(Shoot), 0.5f);
    }

    public void OnJump(InputValue button)
    {
        if (StageManager.Instance.isPaused) return;
        if (characterData.charge <= 0.0f) return;

        if (button.isPressed)
        {
            isJumpPressed = true;
        }
        else
        {
            isJumpPressed = false;
        }
    }

    public void OnPause()
    {
        UIManager.Instance.TogglePause();
    }

    public void OnShift(InputValue button)
    {
        if (StageManager.Instance.isPaused) return;
        if (characterData.charge <= 0.0f)
        {
            isShiftOn = false;
            return;
        }

        isShiftOn = button.isPressed;
    }

    private void Shoot()
    {
        attackComponent.Shoot();
        characterData.AddCharge(-5);
    }
}
