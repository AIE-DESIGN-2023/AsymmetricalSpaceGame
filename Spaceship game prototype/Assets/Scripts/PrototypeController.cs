using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeController : MonoBehaviour
{
    public GameObject human;
    public GameObject aliens;

    public bool humanActive = true;
    public bool alienActive;

    // Start is called before the first frame update
    void Start()
    {
        aliens.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && humanActive == true)
        {
            Debug.Log("Change Players to Aliens");
            human.SetActive(false);
            aliens.SetActive(true);

            humanActive = false;
            alienActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.H) && humanActive == false)
        {
            Debug.Log("CHange Players to Humans");
            human.SetActive(true);
            aliens.SetActive(false);

            humanActive = true;
            alienActive = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
