using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class AIMechanicController : MonoBehaviour
{
    DoorScript doorScript;
    PlayerInput input;
    public GameObject human;
    HumanMechanicController humanMechanicController;
    HumanMovement humanMovement;

    public GameObject alienParent;
    AlienMechanicController alienMechanicController;
    AlienMovement alienMovement;

    public bool ai_isDeactivated;
    public float rebootTime;
    public float currentRebootTime;
    public float rebootChargeMultiplier;
    public GameObject deactivationCanvas;
    public GameObject rebootParticles;
    public Transform rebootParticleSpawnpoint1;
    public Transform rebootParticleSpawnpoint2;
    public Image rebootImage;
    public TextMeshProUGUI rebootText;

    [Header("Battery")]
    public float currentBattery;
    public float batteryCapacity;
    public float batteryChargeSpeed;
    public float chargePerPress;

    public Image batteryImage;
    public TextMeshProUGUI batteryText;
    public GameObject chargeParticle;
    public Transform chargeParticle_spawnpoint;


    [Header("Doors")]

    public float doorActivationCost;
    public GameObject[] alphaDoors;

    public bool alphaDoorOnCooldown;
    public bool alphaDoorsActive;

    [SerializeField] float currentAlphaDoorDuration;
    public float alphaDoorActiveDuration;
    public float alphaDoorCooldownDuration;
    public float alphaDoorTelegraphDuration;
    [Space]

    public GameObject[] betaDoors;

    public bool betaDoorOnCooldown;
    public bool betaDoorsActive;

    [SerializeField] float currentBetaDoorDuration;
    public float betaDoorActiveDuration;
    public float betaDoorCooldownDuration;
    public float betaDoorTelegraphDuration;

    public Image alphaDoorFillImage;
    public Image betaDoorFillImage;

    [Space]
    [Header("Stun Gun")]
    private Vector2 inputMovementVectorA;
    private Vector3 inputMovementVector3A;
    public float crosshairMovementSpeed;
    public int stunLauncherAmmoCount;
    public int currentStunAmmo;
    public float stunLauncherCost;
    public bool crosshairCanMove;
    public bool stunLauncherActive;

    public Rigidbody rb_crosshair;
    public GameObject stunLauncherCrosshair;
    public Transform stunLauncherSpawnpoint;
    public GameObject stunProjectile;
    public TextMeshProUGUI stunLauncherCount_Text;
    public GameObject crosshairActivationParticle;
    public GameObject crosshairFiringParticle;
    public Image stunLauncherImage;
    private bool crosshairFadeIn;
    private bool crosshairFadeOut;
    private float crosshairTimeToFade = 3f;
    public CanvasGroup crosshairCanvasGroup;

    [Space]
    [Header("Gravity Inversion")]
    [SerializeField] bool gravityInversionActive;
    bool gravityInversionOnCooldown;
    public float gravityInversionCost;
    public float gravityInversionTime;
    [SerializeField] float currentGravityInversionTime;
    public float gravityInversionCooldownDuration;
    public Image gravityInversionImage;

    TimerScript timerScript;
    public GameObject aiWinCanvas;
    public CanvasGroup aiWinCanvasGroup;


    //transition stuff
    [Space]
    private bool aDFadeIn;
    private bool aDFadeOut;
    private float aDTimeToFade = 5f;
    //public GameObject aDLoadingBarObject;
    //public Image aDLoadingBarImage;
    //public CanvasGroup aDLoadCanvas;

    // Start is called before the first frame update

    [SerializeField] GameObject blackImage;
    [SerializeField] GameObject shipAIImage;
    [SerializeField] GameObject player3Controller;
    [SerializeField] GameObject ball;
    [SerializeField] CanvasGroup blackImageCanvasGroup;
    [SerializeField] CanvasGroup player3ControllerCanvasGroup;
    [SerializeField] GameObject playerHolder;
    [SerializeField] CanvasGroup playerHolderCanvasGroup;
    bool BCFadeIn;
    bool BCFadeOut;

    public GameObject gravityVFX;

    AudioSource audioSource;
    [SerializeField] AudioClip DoorActivationSound;
    [SerializeField] AudioClip AIDeactivatedSound;
    [SerializeField] AudioClip ExplosionSOund;
    [SerializeField] AudioClip GravActivateSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        input = GetComponent<PlayerInput>();
        FindAnyObjectByType<InputManagerController>().Swap(3);

        human = GameObject.FindGameObjectWithTag("Human");
        humanMechanicController = human.GetComponentInParent<HumanMechanicController>();
        humanMovement = human.GetComponentInParent<HumanMovement>();

        alienParent = GameObject.FindGameObjectWithTag("AlienParent");
        alienMechanicController = alienParent.GetComponentInParent<AlienMechanicController>();
        alienMovement = alienParent.GetComponentInParent<AlienMovement>();

        timerScript = GetComponentInParent<TimerScript>();
        //timerScript.timerOn = true;
        Invoke("StartTimer", 3f);

        aiWinCanvas = GameObject.FindGameObjectWithTag("AIWinStatus");
        aiWinCanvasGroup = aiWinCanvas.GetComponent<CanvasGroup>();
        aiWinCanvasGroup.alpha = 0;

        blackImage = GameObject.FindGameObjectWithTag("BlackImage");
        shipAIImage = GameObject.FindGameObjectWithTag("ShipAIImage");
        player3Controller = GameObject.FindGameObjectWithTag("Player3ControllerImage");
        ball = GameObject.FindGameObjectWithTag("AIBall");
        blackImageCanvasGroup = blackImage.GetComponent<CanvasGroup>();
        player3ControllerCanvasGroup = player3Controller.GetComponent<CanvasGroup>();
        playerHolder = GameObject.FindGameObjectWithTag("PlayerHolder");
        playerHolderCanvasGroup = playerHolder.GetComponent<CanvasGroup>();
        blackImageCanvasGroup.alpha = 1;
        player3ControllerCanvasGroup.alpha = 1;
        playerHolderCanvasGroup.alpha = 1;
        ball.SetActive(false);

        stunLauncherCrosshair.transform.position = stunLauncherSpawnpoint.position;
        //stunLauncherCrosshair.SetActive(false);
        crosshairCanvasGroup.alpha = 0;

        currentRebootTime = 0;
        currentBattery = 0;

        alphaDoors = GameObject.FindGameObjectsWithTag("AlphaDoor");
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>();
            Debug.Log("Found doorscript in alphadoors");
        }

        betaDoors = GameObject.FindGameObjectsWithTag("BetaDoor");
        foreach (GameObject betaDoor in betaDoors)
        {
            betaDoor.GetComponent<DoorScript>();
            Debug.Log("Found doorscript in betadoors");
        }

        currentBattery = batteryCapacity;
        currentAlphaDoorDuration = alphaDoorActiveDuration;
        currentBetaDoorDuration = betaDoorActiveDuration;
        currentStunAmmo = stunLauncherAmmoCount;
        currentGravityInversionTime = gravityInversionTime;

        humanMovement.canMove = false;
        alienMovement.alien2CanMove = false;
        alienMovement.alien1CanMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ai_isDeactivated) { currentRebootTime += Time.deltaTime; }
        if (currentRebootTime >= rebootTime) { ReactivateAI();  }
        rebootImage.fillAmount = currentRebootTime / rebootTime;
        rebootText.text = (currentRebootTime / rebootTime).ToString("P");

        if (ai_isDeactivated == false) { currentBattery += batteryChargeSpeed * Time.deltaTime; }
        if (currentBattery > batteryCapacity) { currentBattery = batteryCapacity; }
        batteryImage.fillAmount = currentBattery / batteryCapacity;
        batteryText.text = (currentBattery / batteryCapacity).ToString("P");

        stunLauncherImage.fillAmount = currentStunAmmo / stunLauncherAmmoCount;

        if (alphaDoorOnCooldown) //change image fills 
        { alphaDoorFillImage.fillAmount = currentAlphaDoorDuration / alphaDoorCooldownDuration; }
        else
        { alphaDoorFillImage.fillAmount = currentAlphaDoorDuration / alphaDoorActiveDuration; }


        //DO THE DEACTIVATION THING

        if (alphaDoorsActive == true && alphaDoorOnCooldown == false)  //if doors are active
        { currentAlphaDoorDuration -= Time.deltaTime; }

        if (currentAlphaDoorDuration <= 0.05 && alphaDoorOnCooldown == false) //if active timer runs out, start cooldown - timer only goes back up if hits 0 or below
        { alphaDoorOnCooldown = true; currentAlphaDoorDuration += Time.deltaTime; }

        if (alphaDoorOnCooldown == true) { currentAlphaDoorDuration += Time.deltaTime; }

        if (currentAlphaDoorDuration >= alphaDoorCooldownDuration) //once cooldown ends set timer back to active timer
        { alphaDoorOnCooldown = false; currentAlphaDoorDuration = alphaDoorActiveDuration; }


        if (betaDoorOnCooldown) //change image fills 
        { betaDoorFillImage.fillAmount = currentBetaDoorDuration / betaDoorCooldownDuration; }
        else
        { betaDoorFillImage.fillAmount = currentBetaDoorDuration / betaDoorActiveDuration; }


        if (betaDoorsActive == true && betaDoorOnCooldown == false)  //if doors are active
        { currentBetaDoorDuration -= Time.deltaTime; }

        if (currentBetaDoorDuration <= 0.05 && betaDoorOnCooldown == false) //if active timer runs out, start cooldown - timer only goes back up if hits 0 or below
        { betaDoorOnCooldown = true; currentBetaDoorDuration += Time.deltaTime; }

        if (betaDoorOnCooldown == true) { currentBetaDoorDuration += Time.deltaTime; }

        if (currentBetaDoorDuration >= betaDoorCooldownDuration) //once cooldown ends set timer back to active timer
        { betaDoorOnCooldown = false; currentBetaDoorDuration = betaDoorActiveDuration; }


        if (gravityInversionOnCooldown)
        { gravityInversionImage.fillAmount = currentGravityInversionTime / gravityInversionCooldownDuration; }
        else
        { gravityInversionImage.fillAmount = currentGravityInversionTime / gravityInversionTime; }

        if (gravityInversionActive)
        { currentGravityInversionTime -= Time.deltaTime; }
        if (currentGravityInversionTime <= 0.05 && gravityInversionOnCooldown == false)
        { gravityInversionActive = false; gravityInversionOnCooldown = true; }
        if (gravityInversionOnCooldown)
        {
            currentGravityInversionTime += Time.deltaTime;
        }
        if (currentGravityInversionTime >= gravityInversionCooldownDuration)
        { gravityInversionOnCooldown = false; currentGravityInversionTime = gravityInversionTime; }



        if (crosshairCanMove == true)
        {
            rb_crosshair.AddForce(inputMovementVector3A * crosshairMovementSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

        if (crosshairFadeIn)
        {
            if (crosshairCanvasGroup.alpha < 1)
            {
                crosshairCanvasGroup.alpha += crosshairTimeToFade * Time.deltaTime;
                if (crosshairCanvasGroup.alpha >= 1)
                {
                    crosshairFadeIn = false;
                }
            }
        }
        if (crosshairFadeOut)
        {
            if (crosshairCanvasGroup.alpha >= 0)
            {
                crosshairCanvasGroup.alpha -= crosshairTimeToFade * Time.deltaTime;
                if (crosshairCanvasGroup.alpha <= 0)
                {
                    crosshairFadeOut = false;
                }
            }
        }

        if (BCFadeIn)
        {
            if (blackImageCanvasGroup.alpha < 1)
            {
                blackImageCanvasGroup.alpha += crosshairTimeToFade * Time.deltaTime;
                playerHolderCanvasGroup.alpha += crosshairTimeToFade * Time.deltaTime;
                if (blackImageCanvasGroup.alpha >= 1)
                {
                    BCFadeIn = false;
                }
            }
        }
        if (BCFadeOut)
        {
            if (blackImageCanvasGroup.alpha >= 0)
            {
                blackImageCanvasGroup.alpha -= crosshairTimeToFade * Time.deltaTime;
                playerHolderCanvasGroup.alpha -= crosshairTimeToFade * Time.deltaTime;
                if (blackImageCanvasGroup.alpha <= 0)
                {
                    BCFadeOut = false;
                }
            }
        }
    }

    void StartTimer()
    {

        BCFadeOut = true;
        timerScript.timerOn = true;
        humanMovement.canMove = true;
        alienMovement.alien1CanMove = true;
        alienMovement.alien2CanMove = true;
        currentBattery = batteryCapacity;
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
    public void ActivateDoorA(InputAction.CallbackContext value)
    {

        if (value.started && alphaDoorOnCooldown == false && alphaDoorsActive == false && currentBattery >= doorActivationCost && ai_isDeactivated == false && betaDoorsActive == false)
        {
            //do the doors
            //invoke doors, telegraph duration
            currentBattery -= doorActivationCost;
            Debug.Log("after if");
            ActivateAlphaDoors();
            alphaDoorsActive = true;
            Debug.Log("activated door group alpha");
            Invoke("DeactivateAlphaDoors", alphaDoorActiveDuration + 0.05f);
        }
    }

    public void ActivateDoorB(InputAction.CallbackContext value)
    {
        if (value.started && betaDoorOnCooldown == false && betaDoorsActive == false && currentBattery >= doorActivationCost && ai_isDeactivated == false && alphaDoorsActive == false)
        {
            //do the doors
            //invoke doors, telegraph duration
            currentBattery -= doorActivationCost;
            Debug.Log("after if");
            ActivateBetaDoors();
            betaDoorsActive = true;
            Debug.Log("activated door group beta");
            Invoke("DeactivateBetaDoors", betaDoorActiveDuration + 0.05f);
        }
    }

    void ActivateAlphaDoors()
    {
        audioSource.clip = DoorActivationSound;
        audioSource.Play();
        Debug.Log("Activating alpha doors phase1");
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>().Open();
        }
        Debug.Log("Successfully activated alpha doors");
    }

    void DeactivateAlphaDoors()
    {
        alphaDoorsActive = false;
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>().Close();
        }
        Debug.Log("Deactivated alpha doors");
    }


    void ActivateBetaDoors()
    {
        audioSource.clip = DoorActivationSound;
        audioSource.Play();
        Debug.Log("Activating beta doors phase1");
        foreach (GameObject betaDoor in betaDoors)
        {
            betaDoor.GetComponent<DoorScript>().Open();
        }
        Debug.Log("Successfully activated beta doors");
    }

    void DeactivateBetaDoors()
    {
        betaDoorsActive = false;
        foreach (GameObject betaDoor in betaDoors)
        {
            betaDoor.GetComponent<DoorScript>().Close();
        }
        Debug.Log("Deactivated beta doors");
    }

    public void ChargeBatteryPress(InputAction.CallbackContext value)
    {
        if (value.started && ai_isDeactivated == false)
        {
            currentBattery += chargePerPress;
            Instantiate(chargeParticle, chargeParticle_spawnpoint.position, chargeParticle_spawnpoint.rotation, null);
            Debug.Log("Adding battery from mash");
        }

        if (value.started && ai_isDeactivated)
        {
            currentRebootTime += chargePerPress * rebootChargeMultiplier;
            Instantiate(rebootParticles, rebootParticleSpawnpoint1.position, rebootParticleSpawnpoint1.rotation, null);
            Instantiate(rebootParticles, rebootParticleSpawnpoint2.position, rebootParticleSpawnpoint2.rotation, null);
            Debug.Log("Charging the reboot battery");
        }
    }

    public void MoveCrosshair(InputAction.CallbackContext context)
    {
        if (crosshairCanMove)
        {
            //take the current input
            inputMovementVectorA = context.ReadValue<Vector2>();
            inputMovementVector3A = new Vector3(inputMovementVectorA.x, 0, inputMovementVectorA.y);

        }

    }

    public void TriggerCrosshair(InputAction.CallbackContext value)
    {
        audioSource.clip = ExplosionSOund;

        if (value.started && currentBattery >= stunLauncherCost && stunLauncherActive == false && ai_isDeactivated == false) //turn the launcher ON
        {
            stunLauncherCrosshair.transform.position = stunLauncherSpawnpoint.transform.position;
            currentBattery -= stunLauncherCost;
            crosshairCanMove = true;
            stunLauncherActive = true;
            //stunLauncherCrosshair.SetActive(true);
            crosshairFadeIn = true;
            Instantiate(crosshairActivationParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, stunLauncherCrosshair.transform);
            stunLauncherCount_Text.text = "3";
            currentStunAmmo = stunLauncherAmmoCount;
            //crosshair fires 3 rounds //must stun players
            return;
        }

        if (value.started && stunLauncherActive && currentStunAmmo == 3 && ai_isDeactivated == false) //fire a shot now that crosshair is activated
        {
            audioSource.Play();
            //fire stun bomb with delay
            Instantiate(stunProjectile, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            Instantiate(crosshairFiringParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, stunLauncherCrosshair.transform);
            currentStunAmmo -= 1;
            stunLauncherCount_Text.text = "2";
            Debug.Log("Updating ammo count");
            return;
        }
        if (value.started && stunLauncherActive && currentStunAmmo == 2 && ai_isDeactivated == false) //fire a shot now that crosshair is activated
        {
            audioSource.Play();
            //fire stun bomb with delay
            Instantiate(stunProjectile, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            Instantiate(crosshairFiringParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, stunLauncherCrosshair.transform);
            currentStunAmmo -= 1;
            stunLauncherCount_Text.text = "1";
            Debug.Log("Updating ammo count");
            return;
        }
        else if (value.started && stunLauncherActive && currentStunAmmo == 1 && ai_isDeactivated == false) //turn off crosshair after firing last round
        {
            audioSource.Play();
            Instantiate(stunProjectile, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            Instantiate(crosshairFiringParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, stunLauncherCrosshair.transform);
            currentStunAmmo -= 1;
            crosshairCanMove = false;
            stunLauncherActive = false;
            Invoke("TurnOffCrosshair", 0.5f);
            return;
        }
    }

    public void InvertControls(InputAction.CallbackContext value)
    {
        if (value.started && gravityInversionActive == false && gravityInversionOnCooldown == false && currentBattery >= gravityInversionCost)
        {
            Instantiate(gravityVFX);
            audioSource.clip = GravActivateSound;
            audioSource.Play();
            currentBattery -= gravityInversionCost;
            gravityInversionActive = true;
            humanMovement.invertedMovement = true;
            //alienMovement.invertedMovement = true;
            Invoke("RevertControls", gravityInversionTime);
        }
    }

    void RevertControls()
    {
        //gravityInversionOnCooldown = true;
        gravityInversionActive = false;
        humanMovement.invertedMovement = false;
        alienMovement.invertedMovement = false;
    }

    void TurnOffCrosshair()
    {
        crosshairFadeOut = true;
    }

    public void DeactivateAI()
    {
        audioSource.clip = AIDeactivatedSound;
        audioSource.Play();
        ai_isDeactivated = true;
        deactivationCanvas.SetActive(true);
        if (alphaDoorsActive) { DeactivateAlphaDoors(); }
        if (betaDoorsActive) { DeactivateBetaDoors(); }
        currentRebootTime = 0;
        crosshairCanMove = false;
        stunLauncherActive = false;
        Invoke("TurnOffCrosshair", 0.5f);
        timerScript.timerOn = false;
    }

    public void ReactivateAI()
    {
        //access cables and reset those
        ai_isDeactivated = false;
        deactivationCanvas.SetActive(false);
        currentRebootTime = 0;
        rebootTime = 0;
        timerScript.timerOn = true;
    }

    public void AIWinGame()
    {
        aiWinCanvasGroup.alpha = 1;
    }
}