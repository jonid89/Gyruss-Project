using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float circleRadius = 4f;
    public float rotationSpeed = 100f;
    public float shotCooldown = 0.2f; 
    public Rigidbody2D shotRigidbody; 
    public float shotSpeed = 10f;
    public Transform centerTransform;
    private float angle;
    private float shotTimer; 
    private ObjectPooler<Rigidbody2D> objectPooler;
    private Vector3 originalPosition;
    private GameEvents gameEvents;

    private void Start()
    {
        //Storing Original Position for Restart
        //originalPosition = new Vector3(0f, circleRadius, 0f);

        //Subscribing to Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnGameOver += DeactivateShip;
        GameEvents.OnNextLevel += OnNextLevel;

        //Creating Pool of Shots
        objectPooler = ObjectPooler<Rigidbody2D>.Instance;
        CreateShotsPool();
    }

    private void OnDestroy()
    {
        //Unsubscribe to Game Events
        GameEvents.OnGameOver -= DeactivateShip;
        GameEvents.OnNextLevel -= OnNextLevel;
    }

    private void OnEnable()
    {
        ResetShipPosition();
    }

    private void CreateShotsPool()
    {
        objectPooler.CreatePool(shotRigidbody, 30);
    }
        

    private void Update()
    {
        MoveShip();

        // Fire shots every shotCooldown while Fire1 button is down
        shotTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && shotTimer <= 0f)
        {
            FireShot();
            shotTimer = shotCooldown;
        }
    }

    private void MoveShip()
    {
        // Ship movement on circle
        float horizontalInput = Input.GetAxis("Horizontal");
        angle += horizontalInput * rotationSpeed * Time.deltaTime;

        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * circleRadius;
        float y = Mathf.Cos(angle * Mathf.Deg2Rad) * circleRadius;
        Vector3 newPosition = new Vector3(x, y, 0f);

        transform.position = newPosition;

        // Ship rotation to face center
        Vector3 directionToCenter = centerTransform.position - transform.position;
        float targetAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);
    }

    private void FireShot()
    {
        //Rigidbody2D  = Instantiate(shotRigidbody, transform.position, transform.rotation);
        Rigidbody2D shotInstanceRb = objectPooler.GetFromPool(shotRigidbody);
        shotInstanceRb.gameObject.SetActive(true);
        shotInstanceRb.transform.position = transform.position;
        shotInstanceRb.velocity = transform.up * shotSpeed;
    }

    public void DeactivateShip()
    {
        this.gameObject.SetActive(false);
    }

    private void ResetShipPosition()
    {
        angle = 0f;
    }

    private void OnNextLevel(int level)
    {
        ResetShipPosition();
    }

}
