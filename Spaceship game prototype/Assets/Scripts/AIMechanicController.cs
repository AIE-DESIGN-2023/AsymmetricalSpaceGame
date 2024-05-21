using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class AIMechanicController : MonoBehaviour
{
    DoorScript doorScript;

    [Header("Battery")]
    [SerializeField] float currentBattery;
    public float batteryCapacity;
    public float batteryChargeSpeed;

    public Image batteryImage;
    public TextMeshProUGUI batteryText;


    [Header("Doors")]
    public GameObject[] alphaDoors;

    public bool alphaDoorOnCooldown;
    public bool alphaDoorsActive;

    [SerializeField] float currentAlphaDoorDuration;
    public float alphaDoorActiveDuration;
    public float alphaDoorCooldownDuration;
    public float alphaDoorTelegraphDuration;
    [Space]
    /*public float betaDoorActiveDuration;
    private float currentBetaDoorActiveDuration;
    public float betaDoorCooldownDuration;
    public float abetaDoorTelegraphDuration;*/

    public Image alphaDoorFillImage;

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

        currentBattery = batteryCapacity;

        alphaDoors = GameObject.FindGameObjectsWithTag("AlphaDoor");
        /*Debug.Log("found alpha doors"); 
        if (alphaDoors.Length == 0)
        {
        }*/
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>();
            Debug.Log("Found doorscript in alphadoors");
        }

        currentBattery = batteryCapacity;
        currentAlphaDoorDuration = alphaDoorActiveDuration;


    }

    // Update is called once per frame
    void Update()
    {
        currentBattery += batteryChargeSpeed * Time.deltaTime;
        if (currentBattery > batteryCapacity) { currentBattery = batteryCapacity; }
        batteryImage.fillAmount = currentBattery / batteryCapacity;
        batteryText.text = (currentBattery / batteryCapacity).ToString("P");


        if (alphaDoorOnCooldown) //change image fills 
        { alphaDoorFillImage.fillAmount = currentAlphaDoorDuration / alphaDoorCooldownDuration; }
        else
        { alphaDoorFillImage.fillAmount = currentAlphaDoorDuration / alphaDoorActiveDuration; }


        if (alphaDoorsActive == true && alphaDoorOnCooldown == false)  //if doors are active
        { currentAlphaDoorDuration -= Time.deltaTime; }

        if (currentAlphaDoorDuration <= 0.05 && alphaDoorOnCooldown == false) //if active timer runs out, start cooldown - timer only goes back up if hits 0 or below
        { alphaDoorOnCooldown = true; currentAlphaDoorDuration += Time.deltaTime; }

        if (alphaDoorOnCooldown == true) { currentAlphaDoorDuration += Time.deltaTime; }

        if (currentAlphaDoorDuration >= alphaDoorCooldownDuration) //once cooldown ends set timer back to active timer
        { alphaDoorOnCooldown = false; currentAlphaDoorDuration = alphaDoorActiveDuration; }


        if (Input.GetKeyDown(KeyCode.L) && alphaDoorOnCooldown == false && currentBattery >= 10)
        {
            //do the doors
            //invoke doors, telegraph duration
            ActivateAlphaDoors();
            alphaDoorsActive = true;
            Debug.Log("activated door group alpha");
            Invoke("DeactivateAlphaDoors", alphaDoorActiveDuration + 0.05f);
        }

        if (Input.GetKeyDown(KeyCode.K) && currentBattery >= 30)
        {
            currentBattery -= 30;
            Debug.Log("Drained batery");

        }
    }

    /*public void ActivateDoorA(InputAction.CallbackContext context)
    {
        Debug.Log("womp womp");
    }*/

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

}