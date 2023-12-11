using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    //list of ui elements
    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text destinationText;
    //list of gui elements
    [SerializeField] TMP_Text GUI_destinationText;
    [SerializeField] GameObject GUI_destinationSelection;
    [SerializeField] TMP_Text Warnings;

    //general variables
    public bool deliveryActive;
    public enum deliveryStatus
    {
        Accepted,
        InProgress,
        NotActive
    }
    public deliveryStatus DeliveryStatus { get; set; }

    int score;
    string UI_DestinationText;
    public pauseMenu pauseScript;
    public settings settings;
    public bool IsHoldingParcel;
    public PlayerController playerController;

    //gameobjects
    public GameObject destination;
    [SerializeField] GameObject ParcelSpawn;
    [SerializeField] GameObject parcelPrefab;
    public GameObject parcelInstance;
    

    // List of dropoff zones
    public List<GameObject> dropoffZones;
    public List<GameObject> pickupZones;
    Vector3 spawnLocation;

    void Start()
    {
        DeliveryStatus = deliveryStatus.NotActive;
        GUI_destinationSelection.SetActive(false);
        score = 0;
        UI_DestinationText = "Post Office";
        UpdateUI();
        spawnLocation = ParcelSpawn.transform.position;
        Warnings.gameObject.SetActive(false);
        IsHoldingParcel = false;
    }

    void Update()
    {
        UpdateUI();
        checkForClose();
        Debug.Log(destination);
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        destinationText.text = "Destination: " + UI_DestinationText;
    }

    public void OpenMenu()
    {
        GUI_destinationSelection.SetActive(true);
        destination = dropoffZones[Random.Range(0, dropoffZones.Count)];
        GUI_destinationText.text = "Destination: " + destination.name;
        pauseScript.paused = true;
        if (DeliveryStatus == deliveryStatus.NotActive)
        {
            UI_DestinationText = "Waiting...";
        }

    }

    void checkForClose()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GUI_destinationSelection.SetActive(false);

            if (DeliveryStatus == deliveryStatus.NotActive)
            {
                UI_DestinationText = "Post Office";
            }
        }
        if (settings.isActive)
        {
            GUI_destinationSelection.SetActive(false);

            if (DeliveryStatus == deliveryStatus.NotActive)
            {
                UI_DestinationText = "Post Office";
            }
        }
    }

    public void AcceptDelivery()
    {
        if (DeliveryStatus == deliveryStatus.NotActive)
        {
            destinationText.text = "Destiantion: " + destination.name;
            UI_DestinationText = "Parcel Pickup Point";
            DeliveryStatus = deliveryStatus.Accepted;
            parcelInstance = Instantiate(parcelPrefab, spawnLocation, Quaternion.identity);
        }
        else if (DeliveryStatus == deliveryStatus.Accepted)
        {
            Warnings.gameObject.SetActive(true);
            Warnings.text = "You have Already Accepted the delivery.\nFind the parcel and deliver it.";
            // StartCoroutine(Wait(7));
        }
        else if (DeliveryStatus == deliveryStatus.InProgress)
        {
            Warnings.gameObject.SetActive(true);
            Warnings.text = "The delivery is already in progress.\nDeliver it to the correct house before starting a new one.";
            // StartCoroutine(Wait(7));
        }
    }

    public void ReRollDelivery()
    {
        if (DeliveryStatus == deliveryStatus.NotActive)
        {
            destination = dropoffZones[Random.Range(0, dropoffZones.Count)];
            GUI_destinationText.text = "Destination: " + destination.name;
        }
        else if (DeliveryStatus == deliveryStatus.Accepted)
        {
            Warnings.gameObject.SetActive(true);
            Warnings.text = "You have Already Accepted the delivery.\nFind the parcel and deliver it.";
            // StartCoroutine(Wait(7));
        }
        else if (DeliveryStatus == deliveryStatus.InProgress)
        {
            Warnings.gameObject.SetActive(true);
            Warnings.text = "The delivery is already in progress.\nDeliver it to the correct house before starting a new one.";
            // StartCoroutine(Wait(7));
        }
    }

    public void RecieveDelivery()
    {
        deliveryActive = true;
        UI_DestinationText = destination.name;
        DeliveryStatus = deliveryStatus.InProgress;
        Destroy(parcelInstance.gameObject);
        IsHoldingParcel = true;
    }

    // IEnumerator Wait(float waitTime)
    // {   
    //     Debug.Log("hide success");
    //     yield return new WaitForSeconds(waitTime);
    //     Debug.Log("hide success");
    //     Warnings.gameObject.SetActive(false);
    // }

    public void DropoffParcel(PlayerController playerController)
    {
        if (playerController.parcelInstance != null)
        {
            Destroy(playerController.parcelInstance);
            playerController.parcelInstance = null;
        }
        deliveryActive = false;
        score++;
        destination = pickupZones[Random.Range(0, pickupZones.Count)];
        UI_DestinationText = destination.name;
        DeliveryStatus = deliveryStatus.NotActive;
    }

}
