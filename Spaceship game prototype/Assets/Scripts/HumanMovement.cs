using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HumanMovement : MonoBehaviour
{
    GameObject humanSpawnpoint;
    private Vector2 inputMovementVector;
    private Vector3 inputMovementVector3;
    private Rigidbody rb;
    public float movementSpeed;

    private Vector2 inputVec;

    public bool canMove;

    public GameObject stunRing;

    public bool invertedMovement;
    float invertedMovementSpeed;

    [SerializeField] GameObject blackImage;
    [SerializeField] GameObject humanImage;
    [SerializeField] GameObject player1Controller;
    [SerializeField] GameObject ball;
    [SerializeField] CanvasGroup blackImageCanvasGroup;
    [SerializeField] CanvasGroup player1ControllerCanvasGroup;

    public GameObject humanSpriteMoving;
    public Image humanSpriteStill;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;

        stunRing.SetActive(false);

        humanSpawnpoint = GameObject.FindGameObjectWithTag("HumanSpawnpoint");
        this.gameObject.transform.position = humanSpawnpoint.transform.position;

        FindAnyObjectByType<InputManagerController>().Swap(1);

        invertedMovementSpeed = 0 - movementSpeed;

        blackImage = GameObject.FindGameObjectWithTag("BlackImage");
        humanImage = GameObject.FindGameObjectWithTag("HumanImage");
        player1Controller = GameObject.FindGameObjectWithTag("Player1ControllerImage");
        ball = GameObject.FindGameObjectWithTag("HumanBall");
        blackImageCanvasGroup = blackImage.GetComponent<CanvasGroup>();
        player1ControllerCanvasGroup = player1Controller.GetComponent<CanvasGroup>();
        player1ControllerCanvasGroup.alpha = 1;
        ball.SetActive(false);
        canMove = false;

        humanSpriteStill.fillAmount = 1;
        humanSpriteMoving.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += inputMovementVector3 * movementSpeed * Time.deltaTime;
        if (canMove == true)
        {
            if (invertedMovement == false)
            { rb.AddForce(inputMovementVector3 * movementSpeed * Time.fixedDeltaTime, ForceMode.Force); }

            if (invertedMovement)
            { rb.AddForce((inputMovementVector3 * invertedMovementSpeed) * Time.fixedDeltaTime, ForceMode.Force); }

        }




    }

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
        inputMovementVector = context.ReadValue<Vector2>();
        inputMovementVector3 = new Vector3(inputMovementVector.x, 0, inputMovementVector.y);
        }

        if (inputMovementVector.x < 0)
        {
            humanSpriteStill.transform.localScale = new Vector3(1, 1, 1);
            humanSpriteMoving.transform.localScale = new Vector3(6, 6, 6);
            Debug.Log("going left");
        }
        if (inputMovementVector.x > 0)
        {
            humanSpriteStill.transform.localScale = new Vector3(-1, 1, 1);
            humanSpriteMoving.transform.localScale = new Vector3(-6, 6, 6);
        }

        if (inputMovementVector.x == 0 && inputMovementVector.y == 0)
        {
            humanSpriteStill.fillAmount = 1;
            humanSpriteMoving.SetActive(false);
        }
        else
        {
            humanSpriteMoving.SetActive(true);
            humanSpriteStill.fillAmount = 0;
        }

        //Debug.LogError("nah id win");
        //Debug.Log(context);
    }

    public void Ball(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            ball.SetActive(true);
            Invoke("Unball", 0.1f);
        }
    }

    void Unball()
    {
        ball.SetActive(false);
    }

    public void PauseMovement()
    {
        canMove = false;
        stunRing.SetActive(true);
    }

    public void ResumeMovement()
    {
        canMove = true;
        stunRing.SetActive(false);
    }

}