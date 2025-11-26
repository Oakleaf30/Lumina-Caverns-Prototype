using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackDamage => PlayerHealth.Instance.attackDamage;
    public float kbForce = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if the object we hit is the Player
        if (other.CompareTag("Enemy"))
        {
            // 2. Try to get the PlayerHealth component from the colliding object
            Enemy enemy = other.GetComponent<Enemy>();
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();

            // 3. If the component exists, call its method and pass the damage value
            if (!enemy.invincible)
            {
                // PASSING THE DAMAGE VALUE HERE
                enemy.TakeDamage(attackDamage);
                Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;

                // 3. Apply the force
                enemyRb.AddForce(knockbackDirection * kbForce, ForceMode2D.Impulse);
            }
        }
    }
}
