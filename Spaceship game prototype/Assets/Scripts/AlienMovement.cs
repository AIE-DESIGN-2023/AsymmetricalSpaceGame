using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlienMovement : MonoBehaviour
{
    public bool controlsSwapped;
    public Transform alien1ParticleSpawnpoint;
    public Transform alien2ParticleSpawnpoint;
    public GameObject r_JSparticle;
    public GameObject l_JSparticle;
    public GameObject pingParticle;

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

    public GameObject alienWinCanvas;
    public CanvasGroup alienWinCanvasGroup;

    private PlayerInput input;

    public int eggsToLay;
    public int laidEggs = 0;

    public GameObject eggImage1, eggImage2, eggImage3, eggImage4;
    public GameObject pingImage;
    public GameObject swapImage;

    public bool invertedMovement;
    float invertedMovementSpeed;

    [SerializeField] GameObject blackImage;
    [SerializeField] GameObject alienImage;
    [SerializeField] GameObject player2Controller;
    [SerializeField] GameObject ball;
    [SerializeField] CanvasGroup blackImageCanvasGroup;
    [SerializeField] CanvasGroup player2ControllerCanvasGroup;

    public GameObject alien1SpriteMoving;
    public Image alien1SpriteStill;

    public GameObject alien2SpriteMoving;
    public Image alien2SpriteStill;


    // Start is called before the first frame update
    void Start()
    {
        alien1CanMove = true;
        alien2CanMove = true;


        input = GetComponent<PlayerInput>();

        FindAnyObjectByType<InputManagerController>().Swap(2);

        alienWinCanvas = GameObject.FindGameObjectWithTag("AlienWinStatus"); Debug.Log("Found alien win canvus");
        alienWinCanvasGroup = alienWinCanvas.GetComponent<CanvasGroup>();
        alienWinCanvasGroup.alpha = 0;

        eggImage1 = GameObject.FindGameObjectWithTag("EggImage1");
        eggImage2 = GameObject.FindGameObjectWithTag("EggImage2");
        eggImage3 = GameObject.FindGameObjectWithTag("EggImage3");
        eggImage4 = GameObject.FindGameObjectWithTag("EggImage4");
        pingImage = GameObject.FindGameObjectWithTag("PingImage");
        swapImage = GameObject.FindGameObjectWithTag("SwapImage");

        blackImage = GameObject.FindGameObjectWithTag("BlackImage");
        alienImage = GameObject.FindGameObjectWithTag("AliensImage");
        player2Controller = GameObject.FindGameObjectWithTag("Player2ControllerImage");
        ball = GameObject.FindGameObjectWithTag("AlienBall");
        blackImageCanvasGroup = blackImage.GetComponent<CanvasGroup>();
        player2ControllerCanvasGroup = player2Controller.GetComponent<CanvasGroup>();
        player2ControllerCanvasGroup.alpha = 1;
        ball.SetActive(false);

        eggImage1.SetActive(false);
        eggImage2.SetActive(false);
        eggImage3.SetActive(false);
        eggImage4.SetActive(false);

        alien1CanMove = false;
        alien2CanMove = true;

        invertedMovementSpeed = 0 - movementSpeed;
        //alienWinCanvas.SetActive(false);
        //rb_alien1 = GetComponentInChildren<Rigidbody>();
        //rb_alien2 = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //alien0.transform.position += inputMovementVector3_1 * movementSpeed * Time.deltaTime;
        //alien2.transform.position += inputMovementVector3_2 * movementSpeed * Time.deltaTime;
        if (invertedMovement == false)
        {
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
        else
        {
            if (alien1CanMove == true)
            {
                //use the input to move
                rb_alien1.AddForce(inputMovementVector3_1 * invertedMovementSpeed * Time.fixedDeltaTime, ForceMode.Force);
            }
            if (alien2CanMove == true)
            {
                rb_alien2.AddForce(inputMovementVector3_2 * invertedMovementSpeed * Time.fixedDeltaTime, ForceMode.Force);
            }
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

        //alien 1
        if (inputMovementVector_1.x < 0)
        {
            alien1SpriteStill.transform.localScale = new Vector3(1, 1, 1);
            alien1SpriteMoving.transform.localScale = new Vector3(6, 6, 6);
        }
        if (inputMovementVector_1.x > 0)
        {
            alien1SpriteStill.transform.localScale = new Vector3(-1, 1, 1);
            alien1SpriteMoving.transform.localScale = new Vector3(-6, 6, 6);
        }
        if (inputMovementVector_1.x == 0 && inputMovementVector_1.y == 0)
        {
            alien1SpriteStill.fillAmount = 1;
            alien1SpriteMoving.SetActive(false);
        }
        else
        {
            alien1SpriteStill.fillAmount = 0;
            alien1SpriteMoving.SetActive(true);
        }

        //alien 2
        if (inputMovementVector_2.x < 0)
        {
            alien2SpriteStill.transform.localScale = new Vector3(1, 1, 1);
            alien2SpriteMoving.transform.localScale = new Vector3(6,6, 6);
        }
        if (inputMovementVector_2.x > 0)
        {
            alien2SpriteStill.transform.localScale = new Vector3(-1, 1, 1);
            alien2SpriteMoving.transform.localScale = new Vector3(-6, 6, 6);
        }
        if (inputMovementVector_2.x == 0 && inputMovementVector_2.y == 0)
        {
            alien2SpriteStill.fillAmount = 1;
            alien2SpriteMoving.SetActive(false);
        }
        else
        {
            alien2SpriteStill.fillAmount = 0;
            alien2SpriteMoving.SetActive(true);
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
            Instantiate(r_JSparticle, alien1ParticleSpawnpoint.position, alien1ParticleSpawnpoint.rotation, alien1ParticleSpawnpoint);
            //r_JSparticle.transform.parent = alien1ParticleSpawnpoint.transform;
            Instantiate(l_JSparticle, alien2ParticleSpawnpoint.position, alien2ParticleSpawnpoint.rotation, alien2ParticleSpawnpoint);
            //l_JSparticle.transform.parent = alien2ParticleSpawnpoint.transform;
            swapImage.SetActive(true);
            Debug.Log("controls swapped to true");
            return;
        }


        if (value.started && controlsSwapped)
        { 
            controlsSwapped = false;
            Instantiate(l_JSparticle, alien1ParticleSpawnpoint.position, alien1ParticleSpawnpoint.rotation, alien1ParticleSpawnpoint);
            Instantiate(r_JSparticle, alien2ParticleSpawnpoint.position, alien2ParticleSpawnpoint.rotation, alien2ParticleSpawnpoint);
            swapImage.SetActive(false);
            Debug.Log("controls swapped to false");
            return;
        } 

        if (value.started)
        {
            Debug.Log("Value started is true");
        }
    }

    public void PingAlienControls(InputAction.CallbackContext value)
    {


        if (value.started && controlsSwapped == false)
        {
            //show the current keybinds above alien
            Instantiate(l_JSparticle, alien1ParticleSpawnpoint.position, alien1ParticleSpawnpoint.rotation, alien1ParticleSpawnpoint);
            //r_JSparticle.transform.parent = alien1ParticleSpawnpoint.transform;
            Instantiate(r_JSparticle, alien2ParticleSpawnpoint.position, alien2ParticleSpawnpoint.rotation, alien2ParticleSpawnpoint);
            Instantiate(pingParticle, alien1ParticleSpawnpoint.position, alien1ParticleSpawnpoint.rotation, alien1ParticleSpawnpoint);
            Instantiate(pingParticle, alien2ParticleSpawnpoint.position, alien2ParticleSpawnpoint.rotation, alien2ParticleSpawnpoint);
            //l_JSparticle.transform.parent = alien2ParticleSpawnpoint.transform;
            pingImage.SetActive(true);
            Debug.Log("controls swapped to true");
            Invoke("TurnOffPingImage", 1f);
            return;
        }


        if (value.started && controlsSwapped)
        {
            Instantiate(r_JSparticle, alien1ParticleSpawnpoint.position, alien1ParticleSpawnpoint.rotation, alien1ParticleSpawnpoint);
            Instantiate(l_JSparticle, alien2ParticleSpawnpoint.position, alien2ParticleSpawnpoint.rotation, alien2ParticleSpawnpoint);
            Instantiate(pingParticle, alien1ParticleSpawnpoint.position, alien1ParticleSpawnpoint.rotation, alien1ParticleSpawnpoint);
            Instantiate(pingParticle, alien2ParticleSpawnpoint.position, alien2ParticleSpawnpoint.rotation, alien2ParticleSpawnpoint);
            pingImage.SetActive(true);
            Debug.Log("controls swapped to false");
            Invoke("TurnOffPingImage", 1f);
            return;
        }
    }

    void TurnOffPingImage()
    {
        pingImage.SetActive(false);
    }

    public void CheckForEggs()
    {
        if (laidEggs == 1)
        { eggImage1.SetActive(true); eggImage2.SetActive(false); }
        if (laidEggs == 2)
        { eggImage2.SetActive(true); eggImage3.SetActive(false); }
        if (laidEggs == 3)
        { eggImage3.SetActive(true); eggImage4.SetActive(false); }
        if (laidEggs == 4)
        { eggImage4.SetActive(true); }

        if (laidEggs >= eggsToLay)
        {
            AliensWinGame();
        }
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


    public void AliensWinGame()
    {
        //alienWinCanvas.SetActive(true);
        alienWinCanvasGroup.alpha = 1;
        blackImageCanvasGroup.alpha = 1;
        Invoke("ReloadScene", 3.5f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Playtest");
    }
}