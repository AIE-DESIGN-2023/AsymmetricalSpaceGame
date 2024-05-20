using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableScript : MonoBehaviour
{


    public float disablingTime;
    public float currentDisablingTime;

    public float redCableResetTime;
    public float blueCableResetTime;

    public bool redCablesDestroyed;
    public bool blueCablesDestroyed;

    [Header("Good luck dragging all the objects in")]
    public bool redCable1Destroyed;
    public bool redCable2Destroyed;
    public bool redCable3Destroyed;
    public bool blueCable1Destroyed;
    public bool blueCable2Destroyed;
    public bool blueCable3Destroyed;
    [Space]
    public GameObject red_Cable1_Active;
    public GameObject red_Cable2_Active;
    public GameObject red_Cable3_Active;
    public GameObject blue_Cable1_Active;
    public GameObject blue_Cable2_Active;
    public GameObject blue_Cable3_Active;
    public GameObject red_Cable1_Destroyed;
    public GameObject red_Cable2_Destroyed;
    public GameObject red_Cable3_Destroyed;
    public GameObject blue_Cable1_Destroyed;
    public GameObject blue_Cable2_Destroyed;
    public GameObject blue_Cable3_Destroyed;

    public bool ai_isDisabled;

    // Start is called before the first frame update
    void Start()
    {
        //get AI script to disable

        currentDisablingTime = 0;
        ResetBlueCables();
        ResetRedCables();

        red_Cable1_Active = GameObject.FindGameObjectWithTag("RCA1");
        red_Cable2_Active = GameObject.FindGameObjectWithTag("RCA2");
        red_Cable3_Active = GameObject.FindGameObjectWithTag("RCA3");
        blue_Cable1_Active = GameObject.FindGameObjectWithTag("BCA1");
        blue_Cable2_Active = GameObject.FindGameObjectWithTag("BCA2");
        blue_Cable3_Active = GameObject.FindGameObjectWithTag("BCA3");
        red_Cable1_Destroyed = GameObject.FindGameObjectWithTag("RCD1");
        red_Cable2_Destroyed = GameObject.FindGameObjectWithTag("RCD2");
        red_Cable3_Destroyed = GameObject.FindGameObjectWithTag("RCD3");
        blue_Cable1_Destroyed = GameObject.FindGameObjectWithTag("BCD1");
        blue_Cable2_Destroyed = GameObject.FindGameObjectWithTag("BCD2");
        blue_Cable3_Destroyed = GameObject.FindGameObjectWithTag("BCD3");
    }

    // Update is called once per frame
    void Update()
    {
        if (ai_isDisabled == true)
        { currentDisablingTime -= Time.deltaTime; } //disable AI

        if (currentDisablingTime <= 0)
        {
            ai_isDisabled = false;
        }


        if (redCable1Destroyed == true && redCable2Destroyed == true && redCable3Destroyed == true)
        {
            ai_isDisabled = true;
            currentDisablingTime = currentDisablingTime + disablingTime;
            redCable1Destroyed = false;
            redCable2Destroyed = false;
            redCable3Destroyed = false;
            Invoke("ResetRedCables", redCableResetTime);
        }
        if (blueCable1Destroyed == true && blueCable2Destroyed == true && blueCable3Destroyed == true)
        {
            ai_isDisabled = true;
            currentDisablingTime = currentDisablingTime + disablingTime;
            blueCable1Destroyed = false;
            blueCable2Destroyed = false;
            blueCable3Destroyed = false;
            Invoke("ResetBlueCables", blueCableResetTime);
        }

    }

    public void DestroyRedCable1()
    {
        Debug.Log("Destroyed a cable");
        redCable1Destroyed = true;
        red_Cable1_Active.SetActive(false);
        red_Cable1_Destroyed.SetActive(true);
    }
    public void DestroyRedCable2()
    {
        Debug.Log("Destroyed a cable");
        redCable2Destroyed = true;
        red_Cable2_Active.SetActive(false);
        red_Cable2_Destroyed.SetActive(true);
    }
    public void DestroyRedCable3()
    {
        Debug.Log("Destroyed a cable");
        redCable3Destroyed = true;
        red_Cable3_Active.SetActive(false);
        red_Cable3_Destroyed.SetActive(true);
    }
    public void DestroyBlueCable1()
    {
        Debug.Log("Destroyed a cable");
        blueCable1Destroyed = true;
        blue_Cable1_Active.SetActive(false);
        blue_Cable1_Destroyed.SetActive(true);
    }
    public void DestroyBlueCable2()
    {
        Debug.Log("Destroyed a cable");
        blueCable2Destroyed = true;
        blue_Cable2_Active.SetActive(false);
        blue_Cable2_Destroyed.SetActive(true);
    }
    public void DestroyBlueCable3()
    {
        Debug.Log("Destroyed a cable");
        blueCable3Destroyed = true;
        blue_Cable3_Active.SetActive(false);
        blue_Cable3_Destroyed.SetActive(true);
    }

    void ResetRedCables()
    {
        red_Cable1_Active.SetActive(true);
        red_Cable2_Active.SetActive(true);
        red_Cable3_Active.SetActive(true);
        red_Cable1_Destroyed.SetActive(false);
        red_Cable2_Destroyed.SetActive(false);
        red_Cable3_Destroyed.SetActive(false);
    }
    void ResetBlueCables()
    {
        blue_Cable1_Active.SetActive(true);
        blue_Cable2_Active.SetActive(true);
        blue_Cable3_Active.SetActive(true);
        blue_Cable1_Destroyed.SetActive(false);
        blue_Cable2_Destroyed.SetActive(false);
        blue_Cable3_Destroyed.SetActive(false);
    }
}
