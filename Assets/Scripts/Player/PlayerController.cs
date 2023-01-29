using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float maxRotationSpeed = 60f;
    [SerializeField] LayerMask terrainLayer;
    Rigidbody rb;

    Vector3 moveDir;

    PlayerInputActions playerControls;
    InputAction rollControl;

    bool isRolling = false;

    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        playerControls = new PlayerInputActions();
        rollControl = playerControls.Player.Roll;
    }

    void FixedUpdate()
    {
        Vector3? mousePosition = GetMousePosition();
        if (mousePosition is not null)
        {
            // MoveTowardsPosition(mouseCursorPosition.Value);
            Vector3? mouseDirection = GetMouseDirection(mousePosition.Value);
            if (mouseDirection is not null)
            {
                MoveInDirection(transform.forward, GetMouseDistance(mousePosition.Value));
                FaceDirectionOfMovement(mouseDirection.Value);
            }
        }
    }

    private Vector3? GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer);
        if (hasHit)
        {
            return hit.point;
        }
        return null;
    }

    private Vector3? GetMouseDirection(Vector3 position)
    {
        Vector3 facedDirection = position - transform.position;
        facedDirection.y = 0f;
        if (facedDirection == Vector3.zero)
        {
            return null;
        }
        return facedDirection.normalized;
    }
    private float GetMouseDistance(Vector3 position)
    {
        Vector3 facedDirection = position - transform.position;
        facedDirection.y = 0f;
        return facedDirection.magnitude;
    }

    private void MoveTowardsPosition(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x, transform.position.y, position.z);
        rb.MovePosition(Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime));
    }

    private void MoveInDirection(Vector3 direction, float maxDistanceBeforeSpeedPerSecond)
    {
        Vector3 target = transform.position + direction * moveSpeed * Time.deltaTime;
        float maxDistance = maxDistanceBeforeSpeedPerSecond * moveSpeed * Time.deltaTime;
        rb.MovePosition(Vector3.MoveTowards(transform.position, target, maxDistance));
    }

    /*
    private void MoveInMouseDirectionProjectedOntoFacedDirection(Vector3 facedDirection, Vector3 mouseDirection)  // May be incorrect
    {
        MoveInDirection(Vector3.Project(mouseDirection, facedDirection));
    }
    */

    private void FaceDirectionOfMovement(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        Quaternion deltaRotation = Quaternion.Inverse(transform.rotation) * newRotation;
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        deltaRotation.ToAngleAxis(out angle, out axis);
        if (angle > maxRotationSpeed)
        {
            angle = maxRotationSpeed;
        }
        Quaternion clampedDeltaRotation = Quaternion.AngleAxis(angle, axis);
        rb.MoveRotation(transform.rotation * clampedDeltaRotation);

        // Look into
        // Quaternion.FromToRotation
        // Quaternion.RotateTowards

        // Quaternion newRotation = Quaternion.LookRotation(direction);
        // rb.MoveRotation(newRotation);
    }


    void Update()
    {
        if (isRolling)
        {
            RollSnowball();
        }
    }

    private void OnRollControlEnter(InputAction.CallbackContext context)
    {
        CreateSnowball();
        isRolling = true;
        anim.SetBool("isRolling", true);
    }
    private void OnRollControlExit(InputAction.CallbackContext context)
    {
        FireSnowball();
        isRolling = false;
        anim.SetBool("isRolling", false);
    }

    private void CreateSnowball()
    {
        
    }
    private void RollSnowball()
    {
        
    }
    private void FireSnowball()
    {
        
    }

    void OnEnable()
    {
        rollControl.Enable();
        rollControl.started += OnRollControlEnter;
        rollControl.canceled += OnRollControlExit;
    }

    void OnDisable()
    {
        rollControl.Disable();
    }
}
