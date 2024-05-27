using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectile : MonoBehaviour
{
    public float explosionDelay;

    public GameObject hitbox;

    // Start is called before the first frame update
    void Start()
    {
        hitbox.SetActive(false);
    }

    private void Awake()
    {
        hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ActivateExplosion", explosionDelay);
        Invoke("DestroyGameObject", 2f);
    }


    void ActivateExplosion()
    {
        hitbox.SetActive(true);
    }

    void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
