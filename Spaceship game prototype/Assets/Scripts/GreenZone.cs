using UnityEngine;

public class GreenZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            // Arrow entered the green zone
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Player pressed spacebar in the green zone
                Debug.Log("Successful hit!");
                // Add scoring logic or other feedback here
            }
        }
    }
}
