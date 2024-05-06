using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class HumanMechanicController : MonoBehaviour
{
    public float timeToHack;
    public float currentHackTime;
    public bool isHacking;
    public bool terminal1;
    public bool terminal2;
    public bool terminal3;
    public bool terminal4;

    public float timeToDisable;
    public float currentDisablingTime;
    public bool isDisabling;

    public bool isImmuneToKnockdown;
    public float knockdownTime;
    public float invincibilityFrameTime;

    public bool reactorMeltdown;

    public Image hackLoadingBarImage;

    //get movement script for knockdown

    // Start is called before the first frame update
    void Start()
    {
        currentHackTime = 0;
        invincibilityFrameTime = invincibilityFrameTime + knockdownTime; //this may need a different variable like invincDuration
    }

    // Update is called once per frame
    void Update()
    {
        hackLoadingBarImage.fillAmount = currentHackTime / timeToHack;

        if (isHacking == true)
        {
            currentHackTime += Time.deltaTime;
        }

        if (currentHackTime >= timeToHack)
        {
            currentHackTime = 0;
            isHacking = false;

            if (terminal1 == true)
            { CompleteTerminal01(); }
            if (terminal2 == true)
            { CompleteTerminal02(); }
            if (terminal3 == true)
            { CompleteTerminal03(); }
            if (terminal4 == true)
            { CompleteTerminal04(); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terminal01") //&& Input.GetKeyDown(KeyCode.E)     //change to controller input
        {
            Debug.Log("Begin Hacking Terminal 01");
            isHacking = true;
            terminal1 = true;
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
        }

        if (other.tag == "TerminalAI")
        {
            Debug.Log("Stopped Hacking SHIP AI");
            isDisabling = false;
            currentDisablingTime = 0;
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
        //pause movement
    }

    void KnockdownRecovery()
    {
        //unpause movement
    }

    void RemoveKnockdownImmunity()
    {
        isImmuneToKnockdown = false;
    }

    void HumanWinsGame()
    {

    }
}
