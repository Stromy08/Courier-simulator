using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunRotate : MonoBehaviour
{
    [SerializeField] float sunRotationAmount;
    float sunRotation;


    void Update()
    {
        sunRotation = sunRotationAmount * Time.deltaTime;
        transform.Rotate(0, sunRotation, 0);
    }
    
}
