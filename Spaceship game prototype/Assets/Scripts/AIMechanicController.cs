using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AIMechanicController : MonoBehaviour
{
    DoorScript doorScript;
    public GameObject[] alphaDoors;

    public GameObject doorAlpha;
    public Transform doorAMovePoint1;
    public Transform doorAMovepoint2;

    public bool alphaDoorOnCooldown;

    public float alphaDoorActiveDuration;
    private float currentAlphaDoorDuration;
    public float alphaDoorCooldownDuration;
    public float alphaDoorTelegraphDuration;
    [Space]
    public float betaDoorActiveDuration;
    private float currentBetaDoorActiveDuration;
    public float betaDoorCooldownDuration;
    public float abetaDoorTelegraphDuration;

    public Image alphaDoorFillImage;


    [Space]
    private bool aDFadeIn;
    private bool aDFadeOut;
    private float aDTimeToFade = 5f;
    public GameObject aDLoadingBarObject;
    public Image aDLoadingBarImage;
    public CanvasGroup aDLoadCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (alphaDoors == null)
        {
            alphaDoors = GameObject.FindGameObjectsWithTag("AlphaDoor");
        }
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>();
        }


        currentAlphaDoorDuration = alphaDoorActiveDuration;


    }

    // Update is called once per frame
    void Update()
    {


        if (alphaDoorOnCooldown) //change image fills 
        { alphaDoorFillImage.fillAmount = currentAlphaDoorDuration / alphaDoorCooldownDuration; }
        else
        { alphaDoorFillImage.fillAmount = currentAlphaDoorDuration / alphaDoorActiveDuration; }

        if (currentAlphaDoorDuration <= 0) //if active timer runs out, start cooldown - timer only goes back up if hits 0 or below
        { alphaDoorOnCooldown = true; currentAlphaDoorDuration += Time.deltaTime; }

        if (currentAlphaDoorDuration >= alphaDoorCooldownDuration) //once cooldown ends set timer back to active timer
        { alphaDoorOnCooldown = false; currentAlphaDoorDuration = alphaDoorActiveDuration; }



        if (Input.GetKeyDown(KeyCode.L) && alphaDoorOnCooldown == false)
        {
            //do the doors
            //invoke doors, telegraph duration


            Debug.Log("activated door group alpha");
        }
    }

    /*public void ActivateDoorA(InputAction.CallbackContext context)
    {
        Debug.Log("womp womp");
    }*/

    void ActivateAlphaDoors()
    {
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>().Open();
        }
    }

    void DeactivateAlphaDoors()
    {
        foreach (GameObject alphaDoor in alphaDoors)
        {
            alphaDoor.GetComponent<DoorScript>().Close();
        }
    }

}
