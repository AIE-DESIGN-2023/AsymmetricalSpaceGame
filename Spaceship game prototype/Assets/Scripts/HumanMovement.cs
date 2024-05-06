using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovement : MonoBehaviour
{
    private Vector2 inputMovementVector;
    private Vector3 inputMovementVector3;
    private Rigidbody rb;
    public float movementSpeed;

    private Vector2 inputVec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += inputMovementVector3 * movementSpeed * Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMovementVector = context.ReadValue<Vector2>();
        inputMovementVector3 = new Vector3(inputMovementVector.x, 0, inputMovementVector.y);
    }

    public void OnMove(InputValue context)
    {
        Vector2 inputVector = context.Get<Vector2>();
        inputVec = inputVector;
    }
}