using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlienMovement : MonoBehaviour
{
    private Vector2 inputMovementVector_1;
    private Vector3 inputMovementVector3_1;
    private Vector2 inputMovementVector_2;
    private Vector3 inputMovementVector3_2;
    private Rigidbody rb;
    public float movementSpeed;
    public GameObject alien0;
    public GameObject alien2;

    private Vector2 inputVec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        alien0.transform.position += inputMovementVector3_1 * movementSpeed * Time.deltaTime;
        alien2.transform.position += inputMovementVector3_2 * movementSpeed * Time.deltaTime;
    }

    public void MoveLeftJoysick(InputAction.CallbackContext context)
    {
        inputMovementVector_1 = context.ReadValue<Vector2>();
        inputMovementVector3_1 = new Vector3(inputMovementVector_1.x, 0, inputMovementVector_1.y);
    }

    public void MoveRightJoysick(InputAction.CallbackContext context)
    {
        inputMovementVector_2 = context.ReadValue<Vector2>();
        inputMovementVector3_2 = new Vector3(inputMovementVector_2.x, 0, inputMovementVector_2.y);
    }
}