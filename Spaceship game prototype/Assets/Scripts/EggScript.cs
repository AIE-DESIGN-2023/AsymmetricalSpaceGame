using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public GameObject splatImage;
    public Transform splatImageSpawnpoint;

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
        currentEggArmingTime += Time.deltaTime;

        /*if (currentEggArmingTime >= eggArmingTime)
        {

        }*/
    }

    private void OnDestroy()
    {
        //play splat sound
        Instantiate(splatImage, splatImageSpawnpoint.position, splatImageSpawnpoint.rotation, null);
    }
}
