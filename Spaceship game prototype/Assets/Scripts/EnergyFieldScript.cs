using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnergyFieldScript : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject energyFieldPickup;

    public bool pickupActive;

    public float spawnDelay;
    public float currentSpawnDelayCounter;

    AudioSource audioSource;
    [SerializeField] AudioClip createSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentSpawnDelayCounter = 0;

        Instantiate(energyFieldPickup, spawnpoints[Random.Range(0, spawnpoints.Length)]);
        pickupActive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupActive == false)
        {
            currentSpawnDelayCounter += Time.deltaTime;
        }


        if (currentSpawnDelayCounter >= spawnDelay && pickupActive == false)
        {
            audioSource.clip = createSound;
            audioSource.Play();
            Instantiate(energyFieldPickup, spawnpoints[Random.Range(0, spawnpoints.Length)]);
            currentSpawnDelayCounter = 0;
            pickupActive = true;
        }
    }
}
