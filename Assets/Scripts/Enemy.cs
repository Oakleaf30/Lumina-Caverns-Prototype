using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 25;
    public int attackDamage = 5;

    public bool invincible;

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
        if (other.CompareTag("Player"))
        {
            // 2. Try to get the PlayerHealth component from the colliding object
            PlayerController playerHealth = other.GetComponent<PlayerController>();

            // 3. If the component exists, call its method and pass the damage value
            if (!playerHealth.invincible)
            {
                // PASSING THE DAMAGE VALUE HERE
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(Invincible());
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(0.2f);
        invincible = false;
    }
}
