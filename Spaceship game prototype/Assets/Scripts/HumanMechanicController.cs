using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class HumanMechanicController : MonoBehaviour
{
    public HumanMovement humanMovement;

    public float timeToHack;
    public float currentHackTime;
    public bool isHacking;
    public bool terminal1;
    public bool terminal2;
    public bool terminal3;
    public bool terminal4;

    public bool terminal1Complete;
    public bool terminal2Complete;
    public bool terminal3Complete;
    public bool terminal4Complete;
    [Space]
    private bool hackFadeIn;
    private bool hackFadeOut;
    private float hackTimeToFade = 5f;
    public GameObject hackLoadingBarObject;
    public Image hackLoadingBarImage;
    public CanvasGroup hackLoadCanvas;

    [Space]

    public float timeToDisable;
    public float currentDisablingTime;
    public bool isDisabling;

    [Space]

    public bool chainsawActive;
    public float chainsawDuration;
    public float currentChainsawDuration;
    private bool chainsawFadeIn;
    private bool chainsawFadeOut;
    private float chainsawTimeToFade = 5f;
    public GameObject heldChainsaw;
    public CanvasGroup chainsawLoadCanvas;
    public Image chainsawDurationImage;

    [Space]

    public bool isImmuneToKnockdown;
    public float knockdownTime;
    public float invincibilityFrameTime;

    [Space]

    public bool reactorMeltdown;
    //public GameObject reactorNormal;
    //public GameObject reactorMelting;
    //public GameObject temporaryWinStatus;


    //get movement script for knockdown

    // Start is called before the first frame update
    void Start()
    {
        HumanMovement humanMovement = GetComponent<HumanMovement>();

        currentHackTime = 0;
        currentChainsawDuration = chainsawDuration;
        invincibilityFrameTime = invincibilityFrameTime + knockdownTime; //this may need a different variable like invincDuration

        //hackLoadingBarObject.SetActive(false);
        hackLoadCanvas.alpha = 0;
        //chainsawCanvasObject.SetActive(false);
        chainsawLoadCanvas.alpha = 0;

        heldChainsaw.SetActive(false);

        //reactorNormal.SetActive(true);
        //reactorMelting.SetActive(false);
        //temporaryWinStatus.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hackLoadingBarImage.fillAmount = currentHackTime / timeToHack;
        chainsawDurationImage.fillAmount = currentChainsawDuration / chainsawDuration;

        if (isHacking == true)
        {
            currentHackTime += Time.deltaTime;
            //hackLoadingBarObject.SetActive(true);
        }

        if (hackFadeIn)
        {
            if (hackLoadCanvas.alpha < 1)
            {
                hackLoadCanvas.alpha += hackTimeToFade * Time.deltaTime;
                if (hackLoadCanvas.alpha >= 1)
                {
                    hackFadeIn = false;
                }
            }
        }
        if (hackFadeOut)
        {
            if (hackLoadCanvas.alpha >= 0)
            {
                hackLoadCanvas.alpha -= hackTimeToFade * Time.deltaTime;
                if (hackLoadCanvas.alpha <= 0)
                {
                    hackFadeOut = false;
                }
            }
        }

        if (chainsawFadeIn)
        {
            if (chainsawLoadCanvas.alpha < 1)
            {
                chainsawLoadCanvas.alpha += chainsawTimeToFade * Time.deltaTime;
                if (chainsawLoadCanvas.alpha >= 1)
                {
                    chainsawFadeIn = false;
                }
            }
        }
        if (chainsawFadeOut)
        {
            if (chainsawLoadCanvas.alpha >= 0)
            {
                chainsawLoadCanvas.alpha -= chainsawTimeToFade * Time.deltaTime;
                if (chainsawLoadCanvas.alpha <= 0)
                {
                    chainsawFadeOut = false;
                }
            }
        }


        if (currentHackTime >= timeToHack)
        {
            currentHackTime = 0;
            isHacking = false;

            if (terminal1 == true)
            { CompleteTerminal01(); terminal1Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; }
            if (terminal2 == true)
            { CompleteTerminal02(); terminal2Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; }
            if (terminal3 == true)
            { CompleteTerminal03(); terminal3Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; }
            if (terminal4 == true)
            { CompleteTerminal04(); terminal4Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; }
        }

        if (chainsawActive == true)
        {
            currentChainsawDuration -= Time.deltaTime;
            //should we have something to show chainsaw timer? maybe blinking if possible, could be gasoline jug
        }
        if (currentChainsawDuration <= 0)
        {
            isImmuneToKnockdown = false;
            chainsawActive = false;
            heldChainsaw.SetActive(false);
            //chainsawCanvasObject.SetActive(false);
            chainsawFadeOut = true;
            Invoke("ResetChainsaw", 1f);
            Debug.Log("Deactivating chainsaw");
            //could drop the chainsaw
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terminal01")
        {
            Debug.Log("Begin Hacking Terminal 01");
            isHacking = true;
            terminal1 = true;
            hackFadeIn = true;
        }

        if (other.tag == "Terminal02")
        {
            Debug.Log("Begin Hacking Terminal 02");
            isHacking = true;
            terminal2 = true;
            hackFadeIn = true;
        }

        if (other.tag == "Terminal03")
        {
            Debug.Log("Begin Hacking Terminal 03");
            isHacking = true;
            terminal3 = true;
            hackFadeIn = true;
        }

        if (other.tag == "Terminal04")
        {
            Debug.Log("Begin Hacking Terminal 04");
            isHacking = true;
            terminal4 = true;
            hackFadeIn = true;
        }

        if (other.tag == "TerminalAI")
        {
            Debug.Log("Begin Disabling SHIP AI");
            isDisabling = true;
        }

        if (other.tag == "Alien01" && isImmuneToKnockdown == false)
        {
            GetKnockdown();
            Invoke("KnockdownRecovery", knockdownTime);
            Invoke("RemoveKnockdownImmunity", invincibilityFrameTime);
            isImmuneToKnockdown = true;
        }

        if (other.tag == "Alien02" && isImmuneToKnockdown == false)
        {
            GetKnockdown();
            Invoke("KnockdownRecovery", knockdownTime);
            Invoke("RemoveKnockdownImmunity", invincibilityFrameTime);
            isImmuneToKnockdown = true;
        }

        if (other.tag == "ChainsawPickup")
        {
            isImmuneToKnockdown = true;
            chainsawActive = true;
            Destroy(other.gameObject);
            heldChainsaw.SetActive(true);
            //chainsawCanvasObject.SetActive(true);
            chainsawFadeIn = true;
            Debug.Log("Picked up chainsaw");
        }

        if (other.tag == "Dropship" && reactorMeltdown == true)
        {
            HumanWinsGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terminal01")
        {
            Debug.Log("Stopped Hacking Terminal 01");
            isHacking = false;
            currentHackTime = 0;
            terminal1 = false;
            //hackLoadingBarObject.SetActive(false);
            hackFadeOut = true;
        }

        if (other.tag == "Terminal02")
        {
            Debug.Log("Stopped Hacking Terminal 02");
            isHacking = false;
            currentHackTime = 0;
            terminal2 = false;
            //hackLoadingBarObject.SetActive(false);
            hackFadeOut = true;
        }

        if (other.tag == "Terminal03")
        {
            Debug.Log("Stopped Hacking Terminal 03");
            isHacking = false;
            currentHackTime = 0;
            terminal3 = false;
            //hackLoadingBarObject.SetActive(false);
            hackFadeOut = true;
        }

        if (other.tag == "Terminal04")
        {
            Debug.Log("Stopped Hacking Terminal 04");
            isHacking = false;
            currentHackTime = 0;
            terminal4 = false;
            //hackLoadingBarObject.SetActive(false);
            hackFadeOut = true;
        }

        if (other.tag == "TerminalAI")
        {
            Debug.Log("Stopped Hacking SHIP AI");
            isDisabling = false;
            currentDisablingTime = 0;
            hackLoadingBarObject.SetActive(false);

        }

        if (other.tag == "Egg")
        {
            Debug.Log("Stepped on egg");
            Destroy(other.gameObject);
        }
    }

    void CompleteTerminal01()
    { Debug.Log("Completed hacking Terminal 01"); }
    void CompleteTerminal02()
    { Debug.Log("Completed hacking Terminal 02"); }
    void CompleteTerminal03()
    { Debug.Log("Completed hacking Terminal 03"); }
    void CompleteTerminal04()
    { Debug.Log("Completed hacking Terminal 04"); }

    void GetKnockdown()
    {
        
        humanMovement.PauseMovement();
        //pause movement
        //humanMovement.canMove = false;
        Debug.Log("Knocked by alien");
    }

    void KnockdownRecovery()
    {
        humanMovement.ResumeMovement();
        Debug.Log("Recovering from knopckdown");
        //unpause movement
        //humanMovement.canMove = true;
    }

    void RemoveKnockdownImmunity()
    {
        isImmuneToKnockdown = false;
    }

    void CheckForReactorMeltdown()
    {
        if (terminal1Complete == true && terminal2Complete == true && terminal3Complete == true && terminal4Complete == true)
        {
            reactorMeltdown = true;
            //reactorNormal.SetActive(false);
            //reactorMelting.SetActive(true);
        }
    }

    void HumanWinsGame()
    {
        Debug.Log("human won the game");
        //temporaryWinStatus.SetActive(true);
    }

    void ResetChainsaw()
    {
        currentChainsawDuration = chainsawDuration;
    }
}
