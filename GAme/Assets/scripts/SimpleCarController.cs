using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Image speedometer;
    public TMP_Text speedText;
    public float maxSpeed = 50f;

    public GameObject car;
    private AudioListener audioListener;

    public gameManager gameManager;

    bool toggleLightsBool;
    [SerializeField] GameObject Headlights;


    void Start()
    {
        toggleLightsBool = false;
        Headlights.SetActive(toggleLightsBool);
        audioListener = car.GetComponent<AudioListener>();
    }

    public void FixedUpdate()
    {
        if (gameManager.IsDriving)
        {
            audioListener.enabled = true;
            float motor = maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                if (Input.GetKey(KeyCode.Space) && axleInfo.handBrake)
                { //currently not working. might remove//
                    axleInfo.leftWheel.motorTorque = 0;
                    axleInfo.rightWheel.motorTorque = 0;
                    axleInfo.leftWheel.brakeTorque = Mathf.Infinity;
                    axleInfo.rightWheel.brakeTorque = Mathf.Infinity;

                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
        else
        {
            audioListener.enabled = false;
        }
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        Transform parent = collider.transform.parent;
        Transform visualWheel = parent.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }


    void Update()
    {
        UpdateSpeedometer();
        toggleLights();
    }

    private void UpdateSpeedometer()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude;
        float fillPercent = speed / maxSpeed;
        speedometer.fillAmount = fillPercent;
        speedText.text = ((int)speed).ToString() + " km/h";
    }

    void toggleLights()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            toggleLightsBool = !toggleLightsBool;
            Headlights.SetActive(toggleLightsBool);
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    public bool handBrake;
}