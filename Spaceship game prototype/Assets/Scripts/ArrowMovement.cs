using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float moveDistance = 2f; // Distance the arrow moves left and right
    public float moveSpeed = 2f;    // Speed of arrow movement

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new position using Mathf.PingPong
        float newX = initialPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(newX, initialPosition.y, initialPosition.z);
    }
}
