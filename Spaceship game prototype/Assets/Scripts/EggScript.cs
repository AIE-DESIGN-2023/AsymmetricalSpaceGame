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

    // Start is called before the first frame update
    void Start()
    {
        currentEggArmingTime = 0;
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
        if (other.tag == "StunBomb")
        {
            GoSplat();
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
