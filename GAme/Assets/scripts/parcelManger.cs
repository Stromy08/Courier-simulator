using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parcelManger : MonoBehaviour
{
   public List<GameObject> pickupPoints;
   public List<GameObject> dropoffPoints;
   public GameObject car;
   public GameObject parcel;
   private GameObject currentDropoffPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the lists with your pickup and dropoff points
        pickupPoints = new List<GameObject>();
        dropoffPoints = new List<GameObject>();
    }
}
