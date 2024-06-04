using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class HumanMechanicController : MonoBehaviour
{
    public HumanMovement humanMovement;

    AIMechanicController aiMechanicController;
    GameObject shipAI;

    AlienMechanicController alienMechanicController;
    AlienMovement alienMovement;
    GameObject alien;

    EnergyFieldScript energyFieldScript;
    GameObject energyFieldManager;


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
    public GameObject escapePodObject;
    public Animator escapePodAnim;

    [Space]

    public bool isDisabling;
    public float timeToDisable;
    public float currentDisablingTime;
    public float ai_DisableTime;

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
    public GameObject humanWinCanvas;
    public CanvasGroup humanWinCanvasGroup;


    /*private bool ShieldFadeIn;
    private bool ShieldFadeOut;
    float ShieldTimeToFade;
    public CanvasGroup ShieldLoadCanvas;*/
    public GameObject shieldCanvas;

    public GameObject terminalImage1, terminalImage2, terminalImage3, terminalImage4;
    public GameObject terminalIncompleteImage1, terminalIncompleteImage2, terminalIncompleteImage3, terminalIncompleteImage4;
    public GameObject terminalCompleteImage1, terminalCompleteImage2, terminalCompleteImage3, terminalCompleteImage4;

    AudioSource audioSource;
    [SerializeField] AudioClip StunnedSound;
    [SerializeField] AudioClip TerminalSound;
    [SerializeField] AudioClip BarrierSound1;
    [SerializeField] AudioClip EscapePodActivateSound;
    [SerializeField] AudioClip EggSquishSound;


    //get movement script for knockdown

    // Start is called before the first frame update
    void Start()
    {
        HumanMovement humanMovement = GetComponent<HumanMovement>();
        humanWinCanvas = GameObject.FindGameObjectWithTag("HumanWinStatus");
        humanWinCanvasGroup = humanWinCanvas.GetComponent<CanvasGroup>();
        humanWinCanvasGroup.alpha = 0;
        //humanWinCanvas.SetActive(false); Debug.Log("Turned off human win text");

        energyFieldManager = GameObject.FindGameObjectWithTag("EnergyFieldManager");
        energyFieldScript = energyFieldManager.GetComponent<EnergyFieldScript>();

        terminalImage1 = GameObject.FindGameObjectWithTag("TerminalImage1");
        terminalImage2 = GameObject.FindGameObjectWithTag("TerminalImage2");
        terminalImage3 = GameObject.FindGameObjectWithTag("TerminalImage3");
        terminalImage4 = GameObject.FindGameObjectWithTag("TerminalImage4");

        terminalIncompleteImage1 = GameObject.FindGameObjectWithTag("TerminalIncomplete1");
        terminalIncompleteImage2 = GameObject.FindGameObjectWithTag("TerminalIncomplete2");
        terminalIncompleteImage3 = GameObject.FindGameObjectWithTag("TerminalIncomplete3");
        terminalIncompleteImage4 = GameObject.FindGameObjectWithTag("TerminalIncomplete4");

        terminalCompleteImage1 = GameObject.FindGameObjectWithTag("TerminalComplete1");
        terminalCompleteImage2 = GameObject.FindGameObjectWithTag("TerminalComplete2");
        terminalCompleteImage3 = GameObject.FindGameObjectWithTag("TerminalComplete3");
        terminalCompleteImage4 = GameObject.FindGameObjectWithTag("TerminalComplete4");

        terminalCompleteImage1.SetActive(false);
        terminalCompleteImage2.SetActive(false);
        terminalCompleteImage3.SetActive(false);
        terminalCompleteImage4.SetActive(false);

        terminalImage1.SetActive(false);
        terminalImage2.SetActive(false);
        terminalImage3.SetActive(false);
        terminalImage4.SetActive(false);


        currentDisablingTime = 0;
        currentHackTime = 0;
        currentChainsawDuration = chainsawDuration;
        invincibilityFrameTime = invincibilityFrameTime + knockdownTime; //this may need a different variable like invincDuration

        //hackLoadingBarObject.SetActive(false);
        hackLoadCanvas.alpha = 0;
        //chainsawCanvasObject.SetActive(false);
        chainsawLoadCanvas.alpha = 0;
        //ShieldLoadCanvas.alpha = 0;
        shieldCanvas.SetActive(false);


        heldChainsaw.SetActive(false);
        hackFadeOut = true;
        Debug.Log("fading out hacking bar");
        //reactorNormal.SetActive(true);
        //reactorMelting.SetActive(false);
        //temporaryWinStatus.SetActive(false);

        escapePodObject = GameObject.FindGameObjectWithTag("EscapePod");
        escapePodAnim = escapePodObject.GetComponentInParent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHacking)
        {
            hackLoadingBarImage.fillAmount = currentHackTime / timeToHack;
        }
        else if (isDisabling)
        {
            hackLoadingBarImage.fillAmount = currentDisablingTime / timeToDisable;
        }
        chainsawDurationImage.fillAmount = currentChainsawDuration / chainsawDuration;

        if (isDisabling) { currentDisablingTime += Time.deltaTime; } //disable AI
        if (currentDisablingTime >= timeToDisable) { aiMechanicController.DeactivateAI(); aiMechanicController.rebootTime += ai_DisableTime; currentDisablingTime = 0; isDisabling = false; }

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

        /*if (ShieldFadeIn)
        {
            if (ShieldLoadCanvas.alpha < 1)
            {
                ShieldLoadCanvas.alpha += ShieldTimeToFade * Time.deltaTime;
                if (ShieldLoadCanvas.alpha >= 1)
                {
                    ShieldFadeIn = false;
                }
            }
        }
        if (ShieldFadeOut)
        {
            if (ShieldLoadCanvas.alpha >= 0)
            {
                ShieldLoadCanvas.alpha -= ShieldTimeToFade * Time.deltaTime;
                if (ShieldLoadCanvas.alpha <= 0)
                {
                    ShieldFadeOut = false;
                }
            }
        }*/


        if (currentHackTime >= timeToHack)
        {
            currentHackTime = 0;
            isHacking = false;
            audioSource.clip = TerminalSound;
            audioSource.Play();

            if (terminal1 == true)
            { CompleteTerminal01(); terminal1Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; terminalImage1.SetActive(true); terminalCompleteImage1.SetActive(true); terminalIncompleteImage1.SetActive(false); }
            if (terminal2 == true)
            { CompleteTerminal02(); terminal2Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; terminalImage2.SetActive(true); terminalCompleteImage2.SetActive(true); terminalIncompleteImage2.SetActive(false); }
            if (terminal3 == true)
            { CompleteTerminal03(); terminal3Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; terminalImage3.SetActive(true); terminalCompleteImage3.SetActive(true); terminalIncompleteImage3.SetActive(false); }
            if (terminal4 == true)
            { CompleteTerminal04(); terminal4Complete = true; CheckForReactorMeltdown(); hackFadeOut = true; terminalImage4.SetActive(true); terminalCompleteImage4.SetActive(true); terminalIncompleteImage4.SetActive(false); }
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
            Invoke("ResetChainsaw", 0.1f);
            Debug.Log("Deactivating chainsaw");
            //could drop the chainsaw
        }

        if (shipAI == null)
        {
            shipAI = GameObject.FindGameObjectWithTag("ShipAI");
            aiMechanicController = shipAI.GetComponentInParent<AIMechanicController>();
        }
        if (alien == null)
        {
            alien = GameObject.FindGameObjectWithTag("AlienParent");
            alienMechanicController = alien.GetComponentInParent<AlienMechanicController>();
            alienMovement = alien.GetComponentInParent<AlienMovement>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HumanUIKiller")
        {
            isHacking = false;
            isDisabling = false;
            currentHackTime = 0;
            currentDisablingTime = 0;
            terminal1 = false;
            terminal2 = false;
            terminal3 = false;
            terminal4 = false;
            hackLoadCanvas.alpha = 0;
        }

        if (other.tag == "Terminal01" && terminal1Complete == false && humanMovement.canMove == true)
        {
            Debug.Log("Begin Hacking Terminal 01");
            isHacking = true;
            terminal1 = true;
            hackFadeIn = true;
        }

        if (other.tag == "Terminal02" && terminal2Complete == false && humanMovement.canMove == true)
        {
            Debug.Log("Begin Hacking Terminal 02");
            isHacking = true;
            terminal2 = true;
            hackFadeIn = true;
        }

        if (other.tag == "Terminal03" && terminal3Complete == false && humanMovement.canMove == true)
        {
            Debug.Log("Begin Hacking Terminal 03");
            isHacking = true;
            terminal3 = true;
            hackFadeIn = true;
        }

        if (other.tag == "Terminal04" && terminal4Complete == false && humanMovement.canMove == true)
        {
            Debug.Log("Begin Hacking Terminal 04");
            isHacking = true;
            terminal4 = true;
            hackFadeIn = true;
        }

        if (other.tag == "TerminalAI" && humanMovement.canMove == true)
        {
            hackFadeIn = true;
            isDisabling = true;
            Debug.Log("Begin Disabling SHIP AI");
        }

        if (other.tag == "Alien01" && isImmuneToKnockdown == false)
        {
            GetKnockdown();
            Invoke("KnockdownRecovery", knockdownTime);
            Invoke("RemoveKnockdownImmunity", invincibilityFrameTime);
            Invoke("RemoveKnockdownCanvas", (invincibilityFrameTime - 0.5f));
            isDisabling = false;
            isHacking = false;
            currentHackTime = 0;
            currentDisablingTime = 0;
            if (hackFadeOut == false)
            {
                hackFadeOut = true;
            }
            else
            {
                hackLoadCanvas.alpha = 0;
            }

            terminal1 = false; terminal2 = false; terminal3 = false; terminal4 = false;
            isImmuneToKnockdown = true;
        }

        if (other.tag == "Alien02" && isImmuneToKnockdown == false)
        {
            GetKnockdown();
            Invoke("KnockdownRecovery", knockdownTime);
            Invoke("RemoveKnockdownImmunity", invincibilityFrameTime);
            Invoke("RemoveKnockdownCanvas", (invincibilityFrameTime - 0.5f));
            isHacking = false;
            currentHackTime = 0;
            currentDisablingTime = 0;
            if (hackFadeOut == false)
            {
                hackFadeOut = true;
            }
            else
            {
                hackLoadCanvas.alpha = 0;
            }
            terminal1 = false; terminal2 = false; terminal3 = false; terminal4 = false;
            isImmuneToKnockdown = true;
        }

        if (other.tag == "StunProjectile" && isImmuneToKnockdown == false)
        {
            GetKnockdown();
            Invoke("KnockdownRecovery", knockdownTime);
            Invoke("RemoveKnockdownImmunity", knockdownTime);
            isHacking = false;
            currentHackTime = 0;
            currentDisablingTime = 0;
            if (hackFadeOut == false)
            {
                hackFadeOut = true;
            }
            else
            {
                hackLoadCanvas.alpha = 0;
            }

            terminal1 = false; terminal2 = false; terminal3 = false; terminal4 = false;
            isImmuneToKnockdown = true;
        }

        if (other.tag == "ChainsawPickup")
        {
            audioSource.clip = BarrierSound1;
            audioSource.Play();

            isImmuneToKnockdown = true;
            chainsawActive = true;
            Destroy(other.gameObject);
            heldChainsaw.SetActive(true);
            //chainsawCanvasObject.SetActive(true);
            chainsawFadeIn = true;
            energyFieldScript.pickupActive = false;
            Debug.Log("Picked up chainsaw");
        }

        if (other.tag == "Dropship" && reactorMeltdown == true)
        {
            HumanWinsGame();
        }

        if (other.tag == "Egg")
        {
            audioSource.clip = EggSquishSound;
            audioSource.Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terminal01")
        {
            
            isHacking = false;
            currentHackTime = 0;
            terminal1 = false;
            //hackLoadingBarObject.SetActive(false);

            if (hackFadeOut)
            {
                hackLoadCanvas.alpha = 0;
                hackFadeOut = false;
            }
            else
            {
                hackFadeOut = true;
            }
            Debug.Log("Stopped Hacking Terminal 01");
        }

        if (other.tag == "Terminal02")
        {
            
            isHacking = false;
            currentHackTime = 0;
            terminal2 = false;
            //hackLoadingBarObject.SetActive(false);

            if (hackFadeOut)
            {
                hackLoadCanvas.alpha = 0;
                hackFadeOut = false;
            }
            else
            {
                hackFadeOut = true;
            }
            Debug.Log("Stopped Hacking Terminal 02");
        }

        if (other.tag == "Terminal03")
        {
            
            isHacking = false;
            currentHackTime = 0;
            terminal3 = false;
            //hackLoadingBarObject.SetActive(false);

            if (hackFadeOut)
            {
                hackLoadCanvas.alpha = 0;
                hackFadeOut = false;
            }
            else
            {
                hackFadeOut = true;
            }
            Debug.Log("Stopped Hacking Terminal 03");
        }

        if (other.tag == "Terminal04")
        {
            
            isHacking = false;
            currentHackTime = 0;
            terminal4 = false;
            //hackLoadingBarObject.SetActive(false);
            if (hackFadeOut)
            {
                hackLoadCanvas.alpha = 0;
                hackFadeOut = false;
            }
            else
            {
                hackFadeOut = true;
            }

            Debug.Log("Stopped Hacking Terminal 04");
        }

        if (other.tag == "TerminalAI")
        {
            
            isDisabling = false;
            currentDisablingTime = 0;
            //hackLoadingBarObject.SetActive(false);

            if (hackFadeOut)
            {
                hackLoadCanvas.alpha = 0;
                hackFadeOut = false;
            }
            else
            {
                hackFadeOut = true;
            }
            Debug.Log("Stopped Hacking SHIP AI");
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
        if (hackFadeOut == false)
        {
            hackFadeOut = true;
        }

        audioSource.clip = StunnedSound;
        audioSource.Play();
        humanMovement.PauseMovement();
        //pause movement
        //humanMovement.canMove = false;
        Debug.Log("Knocked by alien");
    }

    void KnockdownRecovery()
    {
        humanMovement.ResumeMovement();
        shieldCanvas.SetActive(true);
        Debug.Log("Recovering from knopckdown");
        //unpause movement
        //humanMovement.canMove = true;
    }

    void RemoveKnockdownImmunity()
    {
        isImmuneToKnockdown = false;
        shieldCanvas.SetActive(false);
    }

    void RemoveKnockdownCanvas()
    {
        
    }

    void CheckForReactorMeltdown()
    {
        if (terminal1Complete == true && terminal2Complete == true && terminal3Complete == true && terminal4Complete == true)
        {
            Invoke("PlayDropshipSFX", 1.5f);
            escapePodAnim.Play("EscapePodDrop");
            reactorMeltdown = true;
            //reactorNormal.SetActive(false);
            //reactorMelting.SetActive(true);
        }
    }

    void PlayDropshipSFX()
    {
        audioSource.clip = EscapePodActivateSound;
        audioSource.Play();
    }

    void HumanWinsGame()
    {
        humanWinCanvasGroup.alpha = 1;
        Debug.Log("human won the game");
        //WinStatusCanvas.SetActive(true);
        //temporaryWinStatus.SetActive(true);
    }

    void ResetChainsaw()
    {
        currentChainsawDuration = chainsawDuration;
    }
}
