using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class car1 : MonoBehaviour
{
    [SerializeField] Image speedometer;
    [SerializeField] TMP_Text speedText;
    public float maxSpeed = 50f;
    public float acceleration = 10f;
    public float rotationSpeed = 10f;
    public bool paused = false;
    public store storeScript;

    private Rigidbody rb;
    private float inputVertical;
    private float inputHorizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 1f;
    }

    private void Update()
    {
        UpdateSpeedometer();
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            MoveCar();
            RotateCar();
        }
    }

    private void UpdateSpeedometer()
    {
        float speed = rb.velocity.magnitude;
        float fillPercent = speed / maxSpeed;
        speedometer.fillAmount = fillPercent;
        speedText.text = ((int)speed).ToString() + " km/h";
    }

    private void HandleInput()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
    }

    private void MoveCar()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector3 force = transform.forward * inputVertical * acceleration;
            rb.AddForce(force, ForceMode.Acceleration);
        }
    }

    private void RotateCar()
    {
        if (rb.velocity.magnitude > 1f)
        {
            Vector3 rotation = transform.up * inputHorizontal * rotationSpeed;
            rb.AddTorque(rotation, ForceMode.VelocityChange);
        }
    }
}