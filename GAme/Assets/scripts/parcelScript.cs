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
        deliveryManager = FindObjectOfType<DeliveryManager>();
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
                playerController.parcelInstance.gameObject.tag = "Untagged";
                deliveryManager.DropoffParcel(playerController);
                playerController.currentZone = PlayerController.IsInZone.none;
            }
        }
    }
}
