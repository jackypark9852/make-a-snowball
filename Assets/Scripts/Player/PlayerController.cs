using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] LayerMask terrainLayer;
    Rigidbody rb;

    PlayerInputActions playerControls;
    InputAction rollControl;

    bool isRolling = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        playerControls = new PlayerInputActions();
        rollControl = playerControls.Player.Roll;
    }

    void FixedUpdate()
    {
        Vector3? mouseCursorPosition = GetMouseCursorPosition();
        if (mouseCursorPosition is not null)
        {
            MoveTowardsPosition((Vector3)mouseCursorPosition);
        }
    }

    private Vector3? GetMouseCursorPosition()
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

    private void MoveTowardsPosition(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x, transform.position.y, position.z);
        rb.MovePosition(Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime));
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
    }
    private void OnRollControlExit(InputAction.CallbackContext context)
    {
        FireSnowball();
        isRolling = false;
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
