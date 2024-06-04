using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject characterObjectivesGroup;
    //public GameObject PlayerManager;
    public GameObject blackImage;
    [SerializeField] CanvasGroup characterObjectivesCanvasGroup;
    //[SerializeField] GameObject playersCanvas;
    [SerializeField] CanvasGroup playersCanvasGroup;
    bool fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        blackImage.SetActive(true);
        Invoke("FadeOuttaHere", 6f);
        //PlayerManager.SetActive(false);
        playersCanvasGroup.alpha = 0 ;
        characterObjectivesCanvasGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (hackFadeIn)
        {
            if (hackLoadCanvas.alpha < 1)
            {
                hackLoadCanvas.alpha += hackTimeToFade * Time.deltaTime;
                if (hackLoadCanvas.alpha >= 1)
                {
                    hackFadeIn = false;
                }
            }
        }*/
        if (fadeOut)
        {
            if (characterObjectivesCanvasGroup.alpha >= 0)
            {
                characterObjectivesCanvasGroup.alpha -= 2 * Time.deltaTime;
                if (characterObjectivesCanvasGroup.alpha <= 0)
                {
                    fadeOut = false;
                }
            }
        }

    }

    void FadeOuttaHere()
    {
        fadeOut = true;
        Invoke("ActivateOuttaHere", 1f);

    }

    void ActivateOuttaHere()
    {
        //PlayerManager.SetActive(true);
        playersCanvasGroup.alpha = 1;
    }
}
