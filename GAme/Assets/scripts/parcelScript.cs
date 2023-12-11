using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parcelScript : MonoBehaviour
{


    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }   
        
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dropoffZone")
        {

        }
    }
}
