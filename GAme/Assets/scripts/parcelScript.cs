using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parcelScript : MonoBehaviour
{
    [SerializeField] GameObject parcelPrefab;
    public GameObject parcelInstance;
    public DeliveryManager deliveryManager;
    public PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }



    // Update is called once per frame
    void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }
        
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dropoffZone" && other.gameObject == deliveryManager.destination)
        {
            if (playerController.parcelInstance != null)
            {
                deliveryManager.DropoffParcel(playerController);
            }
        }
    }
}
