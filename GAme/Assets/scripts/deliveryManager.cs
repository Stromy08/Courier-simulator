using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text deliveryStatusText;
    [SerializeField] TMP_Text destinationText;
    bool carStatus;
    int score;
    string deliveryStatus;
    GameObject destination; // Change the type to GameObject

    // List of dropoff zones
    public List<GameObject> dropoffZones;
    public List<GameObject> pickupZones;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the delivery status and score
        carStatus = false;
        score = 0;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (carStatus)
        {
            deliveryStatus = "In Progress";
        }
        else
        {
            deliveryStatus = "Idle";
        }

        UpdateUI();
    }

    // Update the UI texts
    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        deliveryStatusText.text = "Delivery status: " + deliveryStatus;
        destinationText.text = "Destination: " + (destination ? destination.name : "Post office");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickupZone"))
        {
            if (!carStatus)
            {
                // Select a random dropoff zone from the list
                destination = dropoffZones[Random.Range(0, dropoffZones.Count)];
                carStatus = true;
            }
        }

        if (other.gameObject.CompareTag("dropoffZone"))
        {
            if (carStatus)
            {
                // Check if the collided object matches the selected dropoff zone
                if (other.gameObject == destination)
                {
                    carStatus = false;
                    score++;
                    destination = pickupZones[Random.Range(0, pickupZones.Count)];
                }
            }
        }

        UpdateUI();
    }
}
