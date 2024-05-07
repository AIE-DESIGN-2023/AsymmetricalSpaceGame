using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public GameObject splatImage;
    public Transform splatImageSpawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //play splat sound
        Instantiate(splatImage,splatImageSpawnpoint.position, splatImageSpawnpoint.rotation, null );
    }
}
