using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotate : MonoBehaviour
{
   public Transform sunTransform;
   [SerializeField] float dayLength = 120f;
   public Material skyboxMaterial;
   public float timePassed = 0;

    // 37 = południe
    // 58 = wieczór
    // 68 = noc
    // 110 = świt

   void Update()
   {
       timePassed += Time.deltaTime;
       if (timePassed >= dayLength)
       {
           timePassed = 0;
       }

       sunTransform.Rotate(new Vector3((Time.deltaTime / dayLength) * 360, 0, 0));
   }
}