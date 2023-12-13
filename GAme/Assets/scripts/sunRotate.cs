using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunRotate : MonoBehaviour
{

    public Transform sunTransform;
    [SerializeField] float dayLength = 120f;
    public Material skyboxMaterial;


    void Update()
    {
        sunTransform.Rotate(new Vector3((Time.deltaTime / dayLength) * 360, 0, 0));
    }
    
}