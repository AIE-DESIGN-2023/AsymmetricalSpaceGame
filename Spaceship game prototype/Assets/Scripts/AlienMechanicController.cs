using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMechanicController : MonoBehaviour
{
    //get the human knockdown script
    public bool isHoldingFlesh;
    public bool isLayingEgg;
    public int eggPriority;
    public float eggLayingTime;
    public float currentEggLayingTime;

    public GameObject eggToLay;

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
        if (isLayingEgg == true)
        { currentEggLayingTime += Time.deltaTime; }
        if (isDestroyingCable == true)
        { currentCableDestroyTime += Time.deltaTime; }

        //check for cable dead or nah
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Human" && isHoldingFlesh == false)
        { isHoldingFlesh = true; }

        //for Alien 2
        if (other.gameObject.tag == "Alien01" && isHoldingFlesh == true && eggPriority == 2)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == false)
            {
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //For Alien 1
        if (other.gameObject.tag == "Alien02" && isHoldingFlesh == true && eggPriority == 1)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == true)
            {
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //if both aliens have flesh
        if (other.gameObject.tag == "Alien01" && isHoldingFlesh == true && eggPriority == 2)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == true)
            {
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
                Invoke("CheckForSecondFlesh", eggLayingTime + 0.5f);
            }
        }

        if (other.gameObject.tag == "RedCable01" && redCable1 == true)
        {
            isDestroyingCable = true;

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
        if (currentEggLayingTime >= eggLayingTime)
        {
            LayEgg();
            isHoldingFlesh = false;
            isLayingEgg = false;
            currentEggLayingTime = 0;
        }
    }

    void CheckForSecondFlesh()
    {
        isLayingEgg = true;
        Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
    }

    void LayEgg()
    {
        Instantiate(eggToLay);
    }
}