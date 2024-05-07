using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienMechanicController : MonoBehaviour
{
    public bool isAlien01;
    public bool isAlien02;
    [Space]

    //get the human knockdown script
    public bool isHoldingFlesh;
    public bool isLayingEgg;
    public float eggLayingTime;
    public float currentEggLayingTime;

    public Image eggLoadingBarImage;

    public GameObject eggToLay;

    [Space]
    public bool isDestroyingCable;
    public float cableDestroyTime;
    public float currentCableDestroyTime;
    [Header("Active Cables:")]
    public bool redCable1;
    public bool redCable2;
    public bool redCable3;
    public bool blueCable1;
    public bool blueCable2;
    public bool blueCable3;

    // Start is called before the first frame update
    void Start()
    {
        currentEggLayingTime = 0;
        currentCableDestroyTime = 0;


    }

    // Update is called once per frame
    void Update()
    {
        eggLoadingBarImage.fillAmount = currentEggLayingTime / eggLayingTime;

        if (isLayingEgg == true)
        { currentEggLayingTime += Time.deltaTime; }
        if (isDestroyingCable == true)
        { currentCableDestroyTime += Time.deltaTime; }

        //check for cable dead or nah
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Human" && isHoldingFlesh == false)
        { isHoldingFlesh = true; Debug.Log("Hit Human"); }

        //For Alien 1
        if (other.gameObject.tag == "Alien02" && isHoldingFlesh == true && isAlien01 == true)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == false)
            {
                Debug.Log("Alien01 laying egg, Alien02 does not have egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //for Alien 2
        if (other.gameObject.tag == "Alien01" && isHoldingFlesh == true && isAlien02 == true)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == false)
            {
                Debug.Log("Alien02 laying egg, Alien01 does not have egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //if both aliens have flesh
        if (other.gameObject.tag == "Alien02" && isHoldingFlesh == true)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == true)
            {
                Debug.Log("Both Aliens have flesh, Alien01 laying egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
                alienMechanicController.Invoke("CheckForSecondFlesh", eggLayingTime + 0.5f);
            }
        }

        if (other.gameObject.tag == "RedCable01" && redCable1 == true)
        {
            isDestroyingCable = true;
            Debug.Log("Destroying Red Cable 01");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Alien01")
        {
            isLayingEgg = false;
            currentEggLayingTime = 0;
        }

        if (other.tag == "Alien02")
        {
            isLayingEgg = false;
            currentEggLayingTime = 0;
        }
    }

    void CheckForEggLayingTime()
    {
        if (currentEggLayingTime >= eggLayingTime && isHoldingFlesh == true)
        {
            LayEgg();
            isHoldingFlesh = false;
            isLayingEgg = false;
            currentEggLayingTime = 0;
            Debug.Log("Egg has been laid, resetting timer");
        }
    }

    void CheckForSecondFlesh()
    {
        if (isAlien02 == true && isHoldingFlesh == true)
        {
                Debug.Log("Alien02 also has egg, laying second egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
        }
    }

    void LayEgg()
    {
        Instantiate(eggToLay);
    }
}