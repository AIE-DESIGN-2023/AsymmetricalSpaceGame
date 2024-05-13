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
    void FixedUpdate()
    {
        //transform.position += inputMovementVector3 * movementSpeed * Time.deltaTime;
        rb.AddForce(inputMovementVector3 * movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMovementVector = context.ReadValue<Vector2>();
        inputMovementVector3 = new Vector3(inputMovementVector.x, 0, inputMovementVector.y);
        //Debug.LogError("nah id win");
        Debug.Log(context);
    }

}