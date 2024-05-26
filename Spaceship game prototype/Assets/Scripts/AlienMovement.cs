using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlienMovement : MonoBehaviour
{
    public bool controlsSwapped;

    private Vector2 inputMovementVector_1;
    private Vector3 inputMovementVector3_1;
    private Vector2 inputMovementVector_2;
    private Vector3 inputMovementVector3_2;
    //private Rigidbody rb;
    public float movementSpeed;
    public GameObject alien0;
    public GameObject alien2;

    private Vector2 inputVec;

    public AlienMechanicController alienMechanicController1;
    public AlienMechanicController alienMechanicController2;
    public Rigidbody rb_alien1;
    public Rigidbody rb_alien2;

    public bool alien1CanMove;
    public bool alien2CanMove;

    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        alien1CanMove = true;
        alien2CanMove = true;

        input = GetComponent<PlayerInput>();

        FindAnyObjectByType<InputManagerController>().Swap(2);
        

        //rb_alien1 = GetComponentInChildren<Rigidbody>();
        //rb_alien2 = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //alien0.transform.position += inputMovementVector3_1 * movementSpeed * Time.deltaTime;
        //alien2.transform.position += inputMovementVector3_2 * movementSpeed * Time.deltaTime;
        if (alien1CanMove == true)
        {
            //use the input to move
            rb_alien1.AddForce(inputMovementVector3_1 * movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }
        if (alien2CanMove == true)
        {
            rb_alien2.AddForce(inputMovementVector3_2 * movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

    }

    public void MoveLeftJoysick(InputAction.CallbackContext context)
    {
        if (alien1CanMove && controlsSwapped == false)
        {
            //take the current input
            inputMovementVector_1 = context.ReadValue<Vector2>();
            inputMovementVector3_1 = new Vector3(inputMovementVector_1.x, 0, inputMovementVector_1.y);
        }

        if (alien2CanMove && controlsSwapped)
        {
            inputMovementVector_2 = context.ReadValue<Vector2>();
            inputMovementVector3_2 = new Vector3(inputMovementVector_2.x, 0, inputMovementVector_2.y);
        }

    }

    public void MoveRightJoysick(InputAction.CallbackContext context)
    {
        if (alien2CanMove && controlsSwapped == false)
        {
            inputMovementVector_2 = context.ReadValue<Vector2>();
            inputMovementVector3_2 = new Vector3(inputMovementVector_2.x, 0, inputMovementVector_2.y);
        }

        if (alien1CanMove && controlsSwapped)
        {
            inputMovementVector_1 = context.ReadValue<Vector2>();
            inputMovementVector3_1 = new Vector3(inputMovementVector_1.x, 0, inputMovementVector_1.y);
        }

    }

    public void SwapAlienControls(InputAction.CallbackContext value)
    {
        if (value.started && controlsSwapped == false)
        { 
            controlsSwapped = true; //show the current keybinds above alien
                                 
        }
        if (value.started && controlsSwapped)
        { 
            controlsSwapped = false;
        }
    }
}