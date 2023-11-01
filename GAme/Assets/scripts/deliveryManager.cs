using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deliveryManager : MonoBehaviour
{   
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text deliveryStatusText;
    bool carStatus;
    int score;
    string deliveryStatus;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + ((int)score).ToString();
        deliveryStatusText.text = "Delivery status: " + deliveryStatus;
        if (carStatus == true)
        {
            deliveryStatus = "In Progress";
        }
        else
        {
            deliveryStatus = "Idle";
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("pickupZone"))
        {
            if (carStatus == false)
            {
                carStatus = true;
            }
            else
            {
                
            }
            
        }

        if (other.gameObject.CompareTag("dropoffZone"))
        {
            if (carStatus == true)
            {
                carStatus = false;
                score = score + 1;
                
            }
            else
            {
                
            }
        }
    }
}
