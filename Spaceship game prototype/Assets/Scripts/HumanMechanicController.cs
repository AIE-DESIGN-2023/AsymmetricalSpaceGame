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

    public Image hackLoadingBarImage;

    // Start is called before the first frame update
    void Start()
    {
        currentHackTime = timeToHack;
    }

    // Update is called once per frame
    void Update()
    {
        hackLoadingBarImage.fillAmount = currentHackTime / timeToHack;

        //if (currentHackTime -= 0)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terminal01") //&& Input.GetKeyDown(KeyCode.E)     //change to controller input
        {
            Debug.Log("Begin Hacking Terminal 01");
            currentHackTime -= Time.deltaTime;
        }
    }

    void CompleteTerminal01()
    {
        
    }
}
