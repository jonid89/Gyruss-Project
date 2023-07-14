using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    private int shotDamage;
    private float movementSpeed;
    private int maxHealth;
    private int hitScore;
    private float initialCircleRadius;
    private float circleRadiusIncrement;
    private float currentCircleRadius;
    private int currentHealth;
    private Vector3 centerPosition;
    private float angle;
    private GameEvents gameEvents;
    private SpriteRenderer spriteRenderer;
    public float maxCircleRadius = 4f;
    

    private void Start()
    {
        //Subscribing to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnGameOver += DeactivateEnemy;
        GameEvents.OnNextLevel += OnNextLevel;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameOver -= DeactivateEnemy;
        GameEvents.OnNextLevel -= OnNextLevel;
    }

    private void OnEnable()
    {
        this.transform.position = centerPosition;
    }

    private void Update()
    {
        if(currentCircleRadius <= maxCircleRadius)
        {
            currentCircleRadius += circleRadiusIncrement * Time.deltaTime;
        }
        
        MoveAroundCircle();
    }

    public void SetConfig(
        Sprite enemySprite,
        Color enemyColor,
        float enemySize,
        int shotDamage,
        float movementSpeed,
        int maxHealth,
        int hitScore,
        float initialCircleRadius,
        float circleRadiusIncrement)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemySprite;
        spriteRenderer.color = enemyColor;
        transform.localScale = new Vector3(enemySize,-enemySize,1);
        this.shotDamage = shotDamage;
        this.movementSpeed = movementSpeed;
        this.maxHealth = maxHealth;
        this.hitScore = hitScore;
        this.initialCircleRadius = initialCircleRadius;
        this.circleRadiusIncrement = circleRadiusIncrement;

        // Initializing Enemy parameters
        currentHealth = maxHealth;
        currentCircleRadius = initialCircleRadius;
        centerPosition = transform.position;
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

            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Player"))
        {
            gameEvents.TriggerPlayerDamaged();
        }
    }

    private void GetDamage()
    {
        gameEvents.TriggerEnemyHit(shotDamage);

        // Handling Health damaged
        currentHealth -= shotDamage;
        if (currentHealth <= 0)
        {
            gameEvents.TriggerEnemyKilled();
            DeactivateEnemy();
        }
    }

    private void DeactivateEnemy()
    {
        this.gameObject.SetActive(false);
    }

    private void OnNextLevel(int level)
    {
        DeactivateEnemy();
    }
}
