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

    private void Start()
    {
        
    }

    private void Update()
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

        // Fire shots
        shotTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && shotTimer <= 0f)
        {
            FireShot();
            shotTimer = shotCooldown;
        }
    }

    private void FireShot()
    {
        Rigidbody2D shotInstance = Instantiate(shotRigidbody, transform.position, transform.rotation);
        shotInstance.velocity = transform.up * shotSpeed;
    }
}
