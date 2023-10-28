using UnityEngine;

public class car11 : MonoBehaviour
{
    public float speed = 10.0f;
    public float maxSpeed = 200.0f;
    public float acceleration = 10.0f;
    public float rotationSpeed = 100.0f;
    float deceleration;
    float rotationAmount;
    float rotation_speed;
    public float turningRadius = 10.0f; 

    private Rigidbody rb;
    private Vector3 inputVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotation_speed = (float)(speed * 0.2);
        if (Input.GetKey(KeyCode.A))
        {
            rotationAmount = -1;
            if (speed >= 1)
            {
                transform.Rotate(0, rotationAmount * rotationSpeed / rotation_speed * 1  * Time.deltaTime, 0);
            }
            else if (speed <= -1)
            {
                transform.Rotate(0, rotationAmount * rotationSpeed / rotation_speed * -1 * Time.deltaTime, 0);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationAmount = 1;
            if (speed >= 1)
            {
                transform.Rotate(0, rotationAmount * rotationSpeed / rotation_speed * Time.deltaTime, 0);
            }
            else if (speed <= -1)
            {   
                transform.Rotate(0, rotationAmount * rotationSpeed / rotation_speed * -1 * Time.deltaTime, 0);
            }
        }
        else
        {
            rotationAmount = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (speed <= -10)
                {   
                    deceleration = (float)(speed * -0.15);
                    speed = Mathf.Min(speed + acceleration * deceleration * Time.deltaTime, maxSpeed);
                }      
            else if (speed >= -10)
            {
                speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
                transform.Rotate(0, rotationAmount * rotationSpeed, 0);
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {   
            if (speed >= 10)
            {   
                deceleration = (float)(speed * 0.15);
                speed = Mathf.Max(speed - acceleration * deceleration * Time.deltaTime, -maxSpeed);
            }
            else if (speed <= 10)
            {
                speed = Mathf.Max(speed - acceleration * Time.deltaTime, -maxSpeed);
            }

        }
        else
        {
            // Gradually reset speed to 0 when no keys are pressed
            speed = Mathf.MoveTowards(speed, 0, acceleration * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime;
        rb.velocity = move;
    }
}
