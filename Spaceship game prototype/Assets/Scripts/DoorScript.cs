using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform closePoint;
    public Transform openPoint;

    public float doorSpeed;
    float sinTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = openPoint.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        sinTime += Time.deltaTime * doorSpeed;
        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
        float t = evaluate(sinTime);
        transform.position = Vector3.Lerp(closePoint.position, openPoint.position, t);
    }

    public void Close()
    {
        sinTime += Time.deltaTime * doorSpeed;
        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
        float t = evaluate(sinTime);
        transform.position = Vector3.Lerp(openPoint.position, closePoint.position, t);
    }

    public float evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }
}
