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
        Invoke("ActivateExplosion", explosionDelay);
        Invoke("DeactivateHitbox", explosionDelay + 0.1f);
        Invoke("DestroyGameObject", 1.7f);
    }

    private void Awake()
    {
        hitbox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void ActivateExplosion()
    {
        hitbox.SetActive(true);
    }

    void DeactivateHitbox()
    {
        hitbox.SetActive(false);
        Debug.Log("turning off hitbox");
    }

    void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
