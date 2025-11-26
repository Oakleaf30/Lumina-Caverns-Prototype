using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swordHitbox;
    public float swingDuration = 0.2f;

    // Define the start and end angles of the arc (e.g., a 100-degree swing)
    private float startAngle = -45f; // Start swing from lower-back (-45 degrees)
    private float endAngle = 55f;    // End swing to upper-front (55 degrees)

    private bool isAttacking = false;

    void Update()
    {
        swordHitbox.transform.position = transform.position;
    }

    public void Attack(Vector2 direction)
    {
        if (!isAttacking) StartCoroutine(SwingSword(direction));
    }

    private IEnumerator SwingSword(Vector2 direction)
    {
        isAttacking = true;

        (startAngle, endAngle) = GetAngles(direction);

        // --- 1. Preparation ---
        // Ensure the hitbox is in its starting rotation before swinging
        
        swordHitbox.transform.localRotation = Quaternion.Euler(0, 0, startAngle);
        swordHitbox.SetActive(true);

        float timer = 0f;

        // --- 2. Animate the Rotation ---
        while (timer < swingDuration)
        {
            // Calculate the percentage completion of the swing (0.0 to 1.0)
            float t = timer / swingDuration;

            // Calculate the current angle using Lerp on the rotation
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // Apply the rotation (only around the Z-axis in 2D)
            swordHitbox.transform.localRotation = Quaternion.Euler(0, 0, currentAngle);

            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the rotation completes exactly at the end angle
        swordHitbox.transform.localRotation = Quaternion.Euler(0, 0, endAngle);

        // --- 3. Deactivate and Reset ---
        swordHitbox.SetActive(false);
        isAttacking = false;

        // Reset rotation back to zero or original facing direction for next swing preparation
        swordHitbox.transform.localRotation = Quaternion.identity;
    }

    private (float startAngle, float endAngle) GetAngles(Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            return (0, -180);
        }
        else if (direction == Vector2.left)
        {
            return (180, 0);
        } else if (direction == Vector2.up)
        {
            return (90, -90);
        } else if (direction == Vector2.down)
        {
            return (270, 90);
        }
        else
        {
             return (0, 0);
        }
    }
}