using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Image speedometer;
    public TMP_Text speedText;
    public float maxSpeed = 50f;
    public GameObject playerPrefab;
    public gameManager gameManager;


    void Start()
    {

    }

   public void FixedUpdate()
   {
        if (gameManager.IsDriving)
        {
            float motor = maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            
            foreach (AxleInfo axleInfo in axleInfos) {
                if (axleInfo.steering) {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor) {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                if (Input.GetKey(KeyCode.Space) && axleInfo.handBrake) { //currently not working. might remove//
                     axleInfo.leftWheel.motorTorque = 0;
                     axleInfo.rightWheel.motorTorque = 0;
                     
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
   }

   public void ApplyLocalPositionToVisuals(WheelCollider collider)
   {
       if (collider.transform.childCount == 0) {
           return;
       }

       Transform visualWheel = collider.transform.GetChild(0);

       Vector3 position;
       Quaternion rotation;
       collider.GetWorldPose(out position, out rotation);

       visualWheel.transform.position = position;
       visualWheel.transform.rotation = rotation;

       visualWheel.transform.Rotate(0, 0, 90);
   }

    void Update()
    {
        UpdateSpeedometer();
    }

   private void UpdateSpeedometer()
   {
       float speed = GetComponent<Rigidbody>().velocity.magnitude;
       float fillPercent = speed / maxSpeed;
       speedometer.fillAmount = fillPercent;
       speedText.text = ((int)speed).ToString() + " km/h";
   }

    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}

[System.Serializable]
public class AxleInfo {
   public WheelCollider leftWheel;
   public WheelCollider rightWheel;
   public bool motor; 
   public bool steering; 
   public bool handBrake;
}