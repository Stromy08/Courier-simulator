using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class car1 : MonoBehaviour
{
    [SerializeField] Image speedometer;
    [SerializeField] TMP_Text speedText;
    public float carSpeed = 10.0f;
    public float maxSpeed = 200.0f;
    public float acceleration = 10.0f;
    public float rotationSpeed = 100.0f;
    float deceleration;
    float rotationAmount;
    float rotation_speed;
    public bool paused = false;

    private Rigidbody rb;
    private Vector3 inputVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        float fillPercent = carSpeed / 2000f;
        speedometer.fillAmount = fillPercent;
        speedText.text = ((int)carSpeed / 10).ToString() + " km/h";

        if (!paused)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotationAmount = -1;
                if (carSpeed >= 1)
                {
                    rotation_speed = carSpeed * 0.2f;
                    transform.Rotate(0, rotationAmount * rotationSpeed * carSpeed * Time.deltaTime, 0);
                }
                else if (carSpeed <= -1)
                {
                    transform.Rotate(0, rotationAmount * rotationSpeed * carSpeed * Time.deltaTime, 0);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotationAmount = 1;
                if (carSpeed >= 1)
                {
                    transform.Rotate(0, rotationAmount * rotationSpeed * carSpeed * Time.deltaTime, 0);
                }
                else if (carSpeed <= -1)
                {   
                    transform.Rotate(0, rotationAmount * rotationSpeed * carSpeed * Time.deltaTime, 0);
                }
            }
            else
            {
                rotationAmount = 0;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (carSpeed <= -10)
                {   
                    deceleration = (float)(carSpeed * -0.08);
                    carSpeed = Mathf.Min(carSpeed + acceleration * deceleration * Time.deltaTime, maxSpeed);
                }      
            else if (carSpeed >= -10)
            {
                carSpeed = Mathf.Min(carSpeed + acceleration * Time.deltaTime, maxSpeed);
                transform.Rotate(0, rotationAmount * rotationSpeed, 0);
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {   
            if (carSpeed >= 10)
            {   
                deceleration = (float)(carSpeed * 0.08);
                carSpeed = Mathf.Max(carSpeed - acceleration * deceleration * Time.deltaTime, -maxSpeed);
            }
            else if (carSpeed <= 10)
            {
                carSpeed = Mathf.Max(carSpeed - acceleration * Time.deltaTime, -maxSpeed);
            }

        }
        else
        {
            // Gradually reset carSpeed to 0 when no keys are pressed
            carSpeed = Mathf.MoveTowards(carSpeed, 0, acceleration * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = transform.forward * carSpeed * Time.deltaTime;
        rb.velocity = move;
    }
}
