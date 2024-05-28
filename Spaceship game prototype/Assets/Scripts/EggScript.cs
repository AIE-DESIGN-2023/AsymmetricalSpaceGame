using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public GameObject splatImage;
    public Transform splatImageSpawnpoint;

    public bool eggArmed;
    [SerializeField] float currentEggArmingTime;
    public float eggArmingTime;

    GameObject alien;
    AlienMechanicController alienMechanicController;
    AlienMovement alienMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentEggArmingTime = 0;

        if (alien == null)
        {
            alien = GameObject.FindGameObjectWithTag("AlienParent");
            alienMechanicController = alien.GetComponentInParent<AlienMechanicController>();
            alienMovement = alien.GetComponentInParent<AlienMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (eggArmed == false)
        {
            currentEggArmingTime += Time.deltaTime;
        }


        if (currentEggArmingTime >= eggArmingTime)
        {
            eggArmed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StunProjectile" && eggArmed == false)
        {
            GoSplat();
            alienMovement.laidEggs -= 1;
        }

        if (other.tag == "Human" && eggArmed == false)
        {
            GoSplat();
            alienMovement.laidEggs -= 1;
        }

        if (other.tag == "Chainsaw")
        {
            GoSplat();
            alienMovement.laidEggs -= 1;
        }
    }

    void GoSplat()
    {
        Instantiate(splatImage, splatImageSpawnpoint.position, splatImageSpawnpoint.rotation, null);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //play splat sound
        
    }
}
