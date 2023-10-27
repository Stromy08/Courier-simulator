using UnityEngine;

public class car1 : MonoBehaviour
{
    public float speed = 10.0f;
    public float maxSpeed = 200.0f;
    public float acceleration = 10.0f;
    public float rotationSpeed = 100.0f;
    float deceleration;

    private Rigidbody rb;
    private Vector3 inputVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (speed <= -10)
                {   
                    deceleration = (float)(speed * -0.08);
                    speed = Mathf.Min(speed + acceleration * deceleration * Time.deltaTime, maxSpeed);
                }      
            else if (speed >= -10)
            {
                speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {   
            if (speed >= 10)
            {   
                deceleration = (float)(speed * 0.08);
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
