using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("MainCamera");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position + target.transform.rotation * Vector3.forward, target.transform.rotation * Vector3.up);
    }
}
