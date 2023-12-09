using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    //list of ui elements
    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text destinationText;
    //list of gui elements
    [SerializeField] TMP_Text GUI_destinationText;
    [SerializeField] GameObject GUI_destinationSelection;

    //general variables
    public bool deliveryActive;
    public enum deliveryStatus
    {
        Accepted,
        InProgress,
        NotActive
    }

    int score;
    GameObject destination; 
    string UI_DestinationText;

    public pauseMenu pauseScript;
    public settings settings;

    // List of dropoff zones
    public List<GameObject> dropoffZones;
    public List<GameObject> pickupZones;

    void Start()
    {
        deliveryStatus = deliveryStatus.NotActive;
        GUI_destinationSelection.SetActive(false);
        score = 0;
        UI_DestinationText = "Post Office";
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
        checkForClose();

        Debug.Log(deliveryStatus);
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
        UI_DestinationText = "Waiting...";
        pauseScript.paused = true;
    }

    void checkForClose()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GUI_destinationSelection.SetActive(false);
        }
        if (settings.isActive)
        {
            GUI_destinationSelection.SetActive(false);
        }
    }

    public void AcceptDelivery()
    {
        Debug.Log("deliveryManager.cs: AcceptDelivery()");
        destinationText.text = "Destiantion: " + destination.name;
        UI_DestinationText = "Parcel Pickup Point";
        deliveryStatus = deliveryStatus.Accepted;
    }

    public void ReRollDelivery()
    {   
        if (deliveryStatus.NotActive)
        {
            destination = dropoffZones[Random.Range(0, dropoffZones.Count)];
            GUI_destinationText.text = "Destination: " + destination.name;
        }
    }

    public void RecieveDelivery()
    {
        deliveryActive = true;
        UI_DestinationText = destination.name;
        deliveryStatus = deliveryStatus.InProgress;
    }

    public void DropoffParcel()
    {
        deliveryActive = false;
        score++;
        destination = pickupZones[Random.Range(0, pickupZones.Count)];
        UI_DestinationText = destination.name;
        deliveryStatus = deliveryStatus.NotActive;
    }

}
