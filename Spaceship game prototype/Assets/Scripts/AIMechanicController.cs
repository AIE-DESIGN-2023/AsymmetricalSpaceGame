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




    //transition stuff
    [Space]
    private bool aDFadeIn;
    private bool aDFadeOut;
    private float aDTimeToFade = 5f;
    //public GameObject aDLoadingBarObject;
    //public Image aDLoadingBarImage;
    //public CanvasGroup aDLoadCanvas;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        FindAnyObjectByType<InputManagerController>().Swap(3);

        stunLauncherCrosshair.transform.position = stunLauncherSpawnpoint.position;

        currentRebootTime = 0;
        currentBattery = batteryCapacity;

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


        if (crosshairCanMove == true)
        {
            
            rb_crosshair.AddForce(inputMovementVectorA * crosshairMovementSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

    }

    public void ActivateDoorA(InputAction.CallbackContext value)
    {

        if (value.started && alphaDoorOnCooldown == false && currentBattery >= doorActivationCost && ai_isDeactivated == false)
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
        if (value.started && betaDoorOnCooldown == false && currentBattery >= doorActivationCost && ai_isDeactivated == false)
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
        if (value.started && currentBattery >= stunLauncherCost && stunLauncherActive == false && ai_isDeactivated == false) //turn the launcher ON
        {
            crosshairCanMove = true;
            stunLauncherActive = true;
            stunLauncherCrosshair.SetActive(true);
            Instantiate(crosshairActivationParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            crosshairActivationParticle.transform.parent = stunLauncherCrosshair.transform;
            currentStunAmmo = stunLauncherAmmoCount;
            //crosshair fires 3 rounds //must stun players
        }

        if (value.started && stunLauncherActive && currentStunAmmo >= 2 && ai_isDeactivated == false) //fire a shot now that crosshair is activated
        {
            //fire stun bomb with delay
            Instantiate(stunProjectile, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            Instantiate(crosshairFiringParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            crosshairFiringParticle.transform.parent = stunLauncherCrosshair.transform;
            currentStunAmmo -= 1;
        }
        else if (value.started && stunLauncherActive && currentStunAmmo == 1 && ai_isDeactivated == false) //turn off crosshair after firing last round
        {
            Instantiate(stunProjectile, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            Instantiate(crosshairFiringParticle, stunLauncherCrosshair.transform.position, stunLauncherCrosshair.transform.rotation, null);
            crosshairFiringParticle.transform.parent = stunLauncherCrosshair.transform;
            currentStunAmmo -= 1;
            crosshairCanMove = false;
            stunLauncherActive = false;
            stunLauncherCrosshair.SetActive(false);
        }
    }

    public void DeactivateAI()
    {
        ai_isDeactivated = true;
        deactivationCanvas.SetActive(true);
        DeactivateAlphaDoors();
        DeactivateBetaDoors();
        currentRebootTime = 0;
    }

    public void ReactivateAI()
    {
        //access cables and reset those
        ai_isDeactivated = false;
        deactivationCanvas.SetActive(false);
        currentRebootTime = 0;
        rebootTime = 0;
    }
}