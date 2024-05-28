using UnityEngine;

public class SkillCheckController : MonoBehaviour
{
    public RectTransform arrow; // Reference to the arrow UI element
    public RectTransform hitZone; // Reference to the hit zone UI element
    public float arrowSpeed = 100f; // Speed at which the arrow moves

    private bool isSkillCheckActive = false;

    private void Update()
    {
        if (isSkillCheckActive)
        {
            // Move the arrow from left to right
            arrow.anchoredPosition += Vector2.right * arrowSpeed * Time.deltaTime;

            // Check if the arrow is within the hit zone
            if (IsArrowInHitZone())
            {
                // Handle successful skill check (e.g., play a sound, show success message)
                //Debug.Log("Skill check passed!");
            }
        }
    }

    private bool IsArrowInHitZone()
    {
        // Get arrow position relative to the canvas
        Vector2 arrowPosition = arrow.anchoredPosition;

        // Get hit zone boundaries
        Vector2 hitZoneMin = hitZone.anchoredPosition - hitZone.sizeDelta / 2f;
        Vector2 hitZoneMax = hitZone.anchoredPosition + hitZone.sizeDelta / 2f;

        // Check if arrow position is within hit zone
        return arrowPosition.x >= hitZoneMin.x && arrowPosition.x <= hitZoneMax.x;
    }

    // Call this method to start the skill check
    public void StartSkillCheck()
    {
        isSkillCheckActive = true;
        // Reset arrow position to the left side
        arrow.anchoredPosition = new Vector2(-hitZone.sizeDelta.x / 2f, arrow.anchoredPosition.y);
    }

    // Call this method to stop the skill check
    public void StopSkillCheck()
    {
        isSkillCheckActive = false;
    }
}
