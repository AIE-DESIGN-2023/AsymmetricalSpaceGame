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
    //private Rigidbody rb;
    public float movementSpeed;
    public GameObject alien0;
    public GameObject alien2;

    private Vector2 inputVec;

    AlienMechanicController alienMechanicController;
    public Rigidbody rb_alien1;
    public Rigidbody rb_alien2;

    public bool alien1CanMove;
    public bool alien2CanMove;

    // Start is called before the first frame update
    void Start()
    {
        alienMechanicController = GetComponentInChildren<AlienMechanicController>();
        alien1CanMove = true;
        alien2CanMove = true;

        //rb_alien1 = GetComponentInChildren<Rigidbody>();
        //rb_alien2 = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //alien0.transform.position += inputMovementVector3_1 * movementSpeed * Time.deltaTime;
        //alien2.transform.position += inputMovementVector3_2 * movementSpeed * Time.deltaTime;
        if (alien1CanMove == true)
        {
            rb_alien1.AddForce(inputMovementVector3_1 * movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }
        if (alien2CanMove == true)
        {
            rb_alien2.AddForce(inputMovementVector3_2 * movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

    }

    public void MoveLeftJoysick(InputAction.CallbackContext context)
    {
        if (alien1CanMove)
        {
        inputMovementVector_1 = context.ReadValue<Vector2>();
        inputMovementVector3_1 = new Vector3(inputMovementVector_1.x, 0, inputMovementVector_1.y);
        }

    }

    public void MoveRightJoysick(InputAction.CallbackContext context)
    {
        if (alien2CanMove)
        {
        inputMovementVector_2 = context.ReadValue<Vector2>();
        inputMovementVector3_2 = new Vector3(inputMovementVector_2.x, 0, inputMovementVector_2.y);
        }

    }
}