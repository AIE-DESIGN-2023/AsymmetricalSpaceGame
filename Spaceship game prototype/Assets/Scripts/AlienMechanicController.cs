using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class AlienMechanicController : MonoBehaviour
{
    public CableScript cableScript;
    AlienMovement alienMovement;
    HumanMechanicController humanMechanicController;
    GameObject human;

    public bool isAlien01;
    public bool isAlien02;
    [Space]

    public GameObject spawnpoint;
    public float respawnTime;
    public bool isDead;

    [Space]
    //get the human knockdown script
    public bool isHoldingFlesh;
    public bool isLayingEgg;
    public float eggLayingTime;
    public float currentEggLayingTime;

    public bool isMoving;

    private bool eggFadeIn;
    private bool eggFadeOut;
    private float eggTimeToFade = 5f;

    public GameObject eggLoadBarObject;
    public Image eggLoadingBarImage;
    public CanvasGroup eggLoadCanvas;

    public GameObject heldFlesh;
    public GameObject eggToLay;

    [Space]
    public GameObject cableLoadBarObject;
    public Image cableLoadBarImage;
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
        if (isAlien01)
        {
            spawnpoint = GameObject.FindGameObjectWithTag("Alien01Spawnpoint");
            Debug.Log("found spawnpoint1");
            this.gameObject.transform.position = spawnpoint.transform.position;
        }
        if (isAlien02)
        {
            spawnpoint = GameObject.FindGameObjectWithTag("Alien02Spawnpoint");
            Debug.Log("found spawnpoint2");
            this.gameObject.transform.position = spawnpoint.transform.position;
        }
        

        CableScript cableScript = GetComponentInParent<CableScript>();
        AlienMovement alienMovement = GetComponent<AlienMovement>();
        human = GameObject.FindGameObjectWithTag("Human");
        humanMechanicController = human.GetComponent<HumanMechanicController>();

        currentEggLayingTime = 0;
        currentCableDestroyTime = 0;

        heldFlesh.SetActive(false);

        //eggLoadBarObject.SetActive(false);
        eggLoadCanvas.alpha = 0;
        cableLoadBarObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        eggLoadingBarImage.fillAmount = currentEggLayingTime / eggLayingTime;
        cableLoadBarImage.fillAmount = currentCableDestroyTime / cableDestroyTime;

        if (isLayingEgg == true)
        { currentEggLayingTime += Time.deltaTime; }
        if (isMoving == true)
        { currentEggLayingTime -= Time.deltaTime; }
        if (isDestroyingCable == true)
        { currentCableDestroyTime += Time.deltaTime; }

        if (currentCableDestroyTime >= cableDestroyTime)
        {
            currentCableDestroyTime = 0;
            isDestroyingCable = false;

            if (redCable1 == true)
            { cableScript.DestroyRedCable1(); cableLoadBarObject.SetActive(false); ResetCables(); }
            if (redCable2 == true)
            { cableScript.DestroyRedCable2(); cableLoadBarObject.SetActive(false); ResetCables(); }
            if (redCable3 == true)
            { cableScript.DestroyRedCable3(); cableLoadBarObject.SetActive(false); ResetCables(); }
            if (blueCable1 == true)
            { cableScript.DestroyBlueCable1(); cableLoadBarObject.SetActive(false); ResetCables(); }
            if (blueCable2 == true)
            { cableScript.DestroyBlueCable2(); cableLoadBarObject.SetActive(false); ResetCables(); }
            if (blueCable3 == true)
            { Debug.Log("1"); cableScript.DestroyBlueCable3(); Debug.Log("2"); cableLoadBarObject.SetActive(false); ResetCables(); }
        }

        if (eggFadeIn)
        {
            if (eggLoadCanvas.alpha < 1)
            {
                eggLoadCanvas.alpha += eggTimeToFade * Time.deltaTime;
                if (eggLoadCanvas.alpha >= 1)
                {
                    eggFadeIn = false;
                }
            }
        }
        if (eggFadeOut)
        {
            if (eggLoadCanvas.alpha >= 0)
            {
                eggLoadCanvas.alpha -= eggTimeToFade * Time.deltaTime;
                if (eggLoadCanvas.alpha <= 0)
                {
                    eggFadeOut = false;
                }
            }
        }

        //check for cable dead or nah
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Human" && isHoldingFlesh == false /*&& humanMechanicController.isImmuneToKnockdown == false*/)
        { isHoldingFlesh = true; Debug.Log("Hit Human"); heldFlesh.SetActive(true); }

        //For Alien 1
        if (other.gameObject.tag == "Alien02EggProximity" && isHoldingFlesh == true && isAlien01 == true)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == false)
            {
                //eggLoadBarObject.SetActive(true);
                eggFadeIn = true;
                Debug.Log("Alien01 laying egg, Alien02 does not have egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //for Alien 2
        if (other.gameObject.tag == "Alien01EggProximity" && isHoldingFlesh == true && isAlien02 == true)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == false)
            {
                //eggLoadBarObject.SetActive(true);
                eggFadeIn = true;
                Debug.Log("Alien02 laying egg, Alien01 does not have egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
            }
        }

        //if both aliens have flesh
        if (other.gameObject.tag == "Alien02EggProximity" && isHoldingFlesh == true)
        {
            AlienMechanicController alienMechanicController = other.gameObject.GetComponent<AlienMechanicController>();
            if (alienMechanicController != null && alienMechanicController.isHoldingFlesh == true)
            {
                //eggLoadBarObject.SetActive(true);
                eggFadeIn = true;
                Debug.Log("Both Aliens have flesh, Alien01 laying egg");
                isLayingEgg = true;
                Invoke("CheckForEggLayingTime", eggLayingTime + 0.05f);
                alienMechanicController.Invoke("CheckForSecondFlesh", eggLayingTime + 0.5f);
            }
        }

        if (other.tag == "Chainsaw")
        {
            Debug.Log("Alien has been slain");
            isDead = true;
            isHoldingFlesh = false;
            heldFlesh.SetActive(false);
            this.gameObject.SetActive(false);
            this.gameObject.transform.position = spawnpoint.transform.position;
            Invoke("Respawn", respawnTime);
            if (isAlien01 == true)
            { alienMovement.alien1CanMove = false; }
            if (isAlien02 == true)
            { alienMovement.alien2CanMove = false; }
        }

        if (other.tag == "AlphaCable01")
        {
            isDestroyingCable = true;
            redCable1 = true;
            Debug.Log("Destroying Red Cable 01");
            cableLoadBarObject.SetActive(true);
        }

        if (other.tag == "AlphaCable02")
        {
            isDestroyingCable = true;
            redCable2 = true;
            Debug.Log("Destroying Red Cable 02");
            cableLoadBarObject.SetActive(true);
        }

        if (other.tag == "AlphaCable03")
        {
            isDestroyingCable = true;
            redCable3 = true;
            Debug.Log("Destroying Red Cable 03");
            cableLoadBarObject.SetActive(true);
        }

        if (other.tag == "BetaCable01")
        {
            isDestroyingCable = true;
            blueCable1 = true;
            Debug.Log("Destroying Blue Cable 01");
            cableLoadBarObject.SetActive(true);
        }

        if (other.tag == "BetaCable02")
        {
            isDestroyingCable = true;
            blueCable2 = true;
            Debug.Log("Destroying Blue Cable 02");
            cableLoadBarObject.SetActive(true);
        }

        if (other.tag == "BetaCable03")
        {
            isDestroyingCable = true;
            blueCable3 = true;
            Debug.Log("Destroying Blue Cable 03");
            cableLoadBarObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Alien01EggProximity")
        {
            eggFadeOut = true;
            ResetEggLaying();
        }

        if (other.tag == "Alien02EggProximity")
        {
            eggFadeOut = true;
            ResetEggLaying();
        }

        if (other.tag == "AlphaCable01")
        {
            Debug.Log("Stopped destroying red cable 1");
            isDestroyingCable = false;
            currentCableDestroyTime = 0;
            redCable1 = false;
            cableLoadBarObject.SetActive(false);
        }
        if (other.tag == "AlphaCable02")
        {
            Debug.Log("Stopped destroying red cable 2");
            isDestroyingCable = false;
            currentCableDestroyTime = 0;
            redCable2 = false;
            cableLoadBarObject.SetActive(false);
        }
        if (other.tag == "AlphaCable03")
        {
            Debug.Log("Stopped destroying red cable 3");
            isDestroyingCable = false;
            currentCableDestroyTime = 0;
            redCable3 = false;
            cableLoadBarObject.SetActive(false);
        }
        if (other.tag == "BetaCable01")
        {
            Debug.Log("Stopped destroying blue cable 1");
            isDestroyingCable = false;
            currentCableDestroyTime = 0;
            blueCable1 = false;
            cableLoadBarObject.SetActive(false);
        }
        if (other.tag == "BetaCable02")
        {
            Debug.Log("Stopped destroying blue cable 2");
            isDestroyingCable = false;
            currentCableDestroyTime = 0;
            blueCable2 = false;
            cableLoadBarObject.SetActive(false);
        }
        if (other.tag == "BetaCable03")
        {
            Debug.Log("Stopped destroying blue cable 3");
            isDestroyingCable = false;
            currentCableDestroyTime = 0;
            blueCable3 = false;
            cableLoadBarObject.SetActive(false);
        }
    }

    public void ResetEggLaying()
    {
        isLayingEgg = false;
        currentEggLayingTime = 0;
        //eggLoadBarObject.SetActive(false);
        eggFadeOut = true;
    }

    void ResetCables()
    {
        redCable1 = false;
        redCable2 = false;
        redCable3 = false;
        blueCable1 = false;
        blueCable2 = false;
        blueCable3 = false;
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
        eggFadeOut = true;
        heldFlesh.SetActive(false);
        Instantiate(eggToLay, this.transform.position, this.transform.rotation, null);
    }

    void Respawn()
    {
        Debug.Log("Alien is respawning");
        isDead = false;
        this.gameObject.SetActive(true);
        if (isAlien01 == true)
        {
            alienMovement.alien1CanMove = true;
        }
        if (isAlien02 == true)
        {
            alienMovement.alien2CanMove = true;
        }
    }
}