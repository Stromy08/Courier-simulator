using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    //list of ui elements
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text deliveryStatusText;
    [SerializeField] TMP_Text destinationText;
    //list of gui elements
    [SerializeField] TMP_Text GUI_destinationText;
    [SerializeField] GameObject GUI_destinationSelection;

    //general variables
    public bool deliveryActive;
    int score;
    string deliveryStatus;
    GameObject destination; 

    public pauseMenu pauseScript;
    public settings settings;

    // List of dropoff zones
    public List<GameObject> dropoffZones;
    public List<GameObject> pickupZones;

    void Start()
    {
        deliveryActive = false;
        GUI_destinationSelection.SetActive(false);
        score = 0;
        UpdateUI();
    }

    void Update()
    {
        if (deliveryActive)
        {
            deliveryStatus = "In Progress";
        }
        else
        {
            deliveryStatus = "Idle";
        }

        UpdateUI();
        checkForClose();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        deliveryStatusText.text = "Delivery status: " + deliveryStatus;
        destinationText.text = "Destination: " + (destination ? destination.name : "Post office");
    }

    public void OpenMenu()
    {
        GUI_destinationSelection.SetActive(true);
        destination = dropoffZones[Random.Range(0, dropoffZones.Count)];
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
        RecieveDelivery();
        GUI_destinationText.text = "Destination:" + destination;
    }

    public void RecieveDelivery()
    {
        destination = dropoffZones[Random.Range(0, dropoffZones.Count)];
        deliveryActive = true;
    }

    public void DropoffParcel()
    {
        deliveryActive = false;
        score++;
        destination = pickupZones[Random.Range(0, pickupZones.Count)];
    }


    public void payScore()
    {
        if (score >= 1)
        {
            score = score - 1;
        }

    }

}
