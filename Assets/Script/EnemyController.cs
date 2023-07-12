using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public int shotDamage = 5;
    public float movementSpeed = 100f;
    public int maxHealth = 10;
    public int hitScore = 2;
    public float initialCircleRadius = 0.5f;
    public float circleRadiusIncrement = 0.1f;
    private float currentCircleRadius;
    private int currentHealth;
    private Vector3 centerPosition;
    private float angle;
    private GameEvents gameEvents;
    

    private void Start()
    {
        gameEvents = GameEvents.Instance;
        
        // Initializing Enemy parameters
        currentHealth = maxHealth;
        currentCircleRadius = initialCircleRadius;
        centerPosition = transform.position;
    }

    private void Update()
    {
        currentCircleRadius += circleRadiusIncrement * Time.deltaTime;
        MoveAroundCircle();
    }

    private void MoveAroundCircle()
    {
        angle += movementSpeed * Time.deltaTime;
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * currentCircleRadius;
        float y = Mathf.Cos(angle * Mathf.Deg2Rad) * currentCircleRadius;
        transform.position = centerPosition + new Vector3(x, y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot"))
        {
            GetDamage();

            Destroy(collision.gameObject);
        }
    }

    private void GetDamage()
    {
        gameEvents.TriggerEnemyHit(shotDamage);

        // Handling Health damaged
        currentHealth -= shotDamage;
        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
