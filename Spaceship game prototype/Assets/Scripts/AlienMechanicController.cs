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

    // Start is called before the first frame update
    void Start()
    {
        currentEggLayingTime = eggLayingTime;



    }

    // Update is called once per frame
    void Update()
    {
        if (isLayingEgg == true)
        { currentEggLayingTime -= Time.deltaTime; }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Human" && isHoldingFlesh == false)
        { isHoldingFlesh = true; }

        //for Alien 2
        if (collider.gameObject.tag == "Alien01" && isHoldingFlesh == true && eggPriority == 2)
        {
            AlienMechanicController alienMechanicController = collider.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == false)
            {
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //For Alien 1
        if (collider.gameObject.tag == "Alien02" && isHoldingFlesh == true && eggPriority == 1)
        {
            AlienMechanicController alienMechanicController = collider.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == true)
            {
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //if both aliens have flesh
        if (collider.gameObject.tag == "Alien01" && isHoldingFlesh == true && eggPriority == 2)
        {
            AlienMechanicController alienMechanicController = collider.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == true)
            {
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
                Invoke("CheckForSecondFlesh", eggLayingTime + 0.5f);
            }
        }
    }

    void CheckForEggLayingTime()
    {
        if (currentEggLayingTime <= 0)
        {
            LayEgg();
            isHoldingFlesh = false;
            isLayingEgg = false;
            currentEggLayingTime = eggLayingTime;
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