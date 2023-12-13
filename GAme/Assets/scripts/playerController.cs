using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player behaviour Settings")]
    public float speed = 4f;
    public float sprintSpeed = 8f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    bool isSprinting;
    [SerializeField] float maxStamina = 100f; // Maximum stamina
    [SerializeField] public float staminaRecoveryRate = 15f; // Rate at which stamina recovers
    [SerializeField] public float staminaDrainRate = 5f; // Rate at which stamina drains while sprinting
    [SerializeField] private float currentStamina; // Current stamina


    [Header("Define objects ")]
    public Transform cam;
    public GameObject player;
    public GameObject parcelPrefab;
    public GameObject parcelInstance;

    [Header("Animations")]
    private Animator animator;

    [Header("Scripts")]
    public gameManager gameManager;
    public DeliveryManager deliveryManager;

    [Header("UI")]
    public GameObject FToEnterText;
    public GameObject FToTalkToNpcText;
    public GameObject PickupParcelText;
    public Image staminaBar;


    // zone states
    public enum IsInZone
    {
        none,
        CarEnterance,
        DeliveryNpc,
        ParcelEntity
    }
    [Header("Is in hitbox bools")]
    public IsInZone currentZone;

    [Header("Others")]
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Get the Animator component here
        GameObject deliveryManagerObject = GameObject.Find("DeliveryManager");
        if (deliveryManagerObject != null)
        {
            deliveryManager = deliveryManagerObject.GetComponent<DeliveryManager>();
        }

        deliveryManager = FindObjectOfType<DeliveryManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        FToTalkToNpcText.SetActive(false);
        PickupParcelText.SetActive(false);

    }

    void Update()
    {
        PlayerWalk();
        OpenDeliveryMenu();

        DropoffParcel();
        PickupParcel();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InstantiatePlayer")
        {
            currentZone = IsInZone.CarEnterance;
            FToEnterText.SetActive(true);
        }

        if (other.gameObject.tag == "pickupZone")
        {
            FToTalkToNpcText.SetActive(true);
            currentZone = IsInZone.DeliveryNpc;
        }

        if (other.gameObject.tag == "parcel")
        {
            currentZone = IsInZone.ParcelEntity;
            PickupParcelText.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InstantiatePlayer")
        {
            currentZone = IsInZone.none;
            FToEnterText.SetActive(false);
        }

        if (other.gameObject.tag == "pickupZone")
        {
            FToTalkToNpcText.SetActive(false);
            currentZone = IsInZone.none;
        }

        if (other.gameObject.tag == "parcel")
        {
            currentZone = IsInZone.none;
            PickupParcelText.SetActive(false);
        }
    }
    

    void OpenDeliveryMenu()
    {
        if (currentZone == IsInZone.DeliveryNpc)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                deliveryManager.OpenMenu();
                FToTalkToNpcText.SetActive(false);
            }
        }
    }

    void PickupParcel()
    {
        if (currentZone == IsInZone.ParcelEntity)
        {
            if (!deliveryManager.IsHoldingParcel)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    PickupParcelText.SetActive(false);
                    deliveryManager.RecieveDelivery();
                    currentZone = IsInZone.none;
                }
            }
        }
    }

    void DropoffParcel()
    {
        if (deliveryManager.IsHoldingParcel)
        {
            if (deliveryManager.DeliveryStatus == DeliveryManager.deliveryStatus.InProgress)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Vector3 spawnLocation = player.transform.position + new Vector3(0, 2, 0); // This will spawn the parcel 1 unit above the player
                    parcelInstance = Instantiate(parcelPrefab, spawnLocation, Quaternion.identity);
                    deliveryManager.parcelInstance = parcelInstance;
                    deliveryManager.IsHoldingParcel = false;
                }
            }
        }
    }

    public void HidePickupParcelText()
    {
        currentZone = IsInZone.none;
        PickupParcelText.SetActive(false);
        
    }



    void PlayerWalk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Set animation parameters based on player's input
        animator.SetBool("IsWalking", direction.magnitude >= 0.1f);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina > 0 && direction.magnitude >= 0.1f)
        {
            isSprinting = true;
            animator.SetBool("IsWalking", direction.magnitude >= 0.1f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina <= 0 || direction.magnitude <= 0.1f)
        {
            isSprinting = false;
        }
        float speedToUse = isSprinting ? sprintSpeed : speed;
        // set animator sprinting
        animator.SetBool("IsSprinting", isSprinting);

        // If the player is sprinting and has stamina left, drain stamina
        if (isSprinting && currentStamina > 0)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        // If the player is not sprinting and has less than max stamina, recover stamina
        else if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
        }

        // Clamp current stamina between 0 and max stamina
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Update the stamina bar fill amount
        staminaBar.fillAmount = currentStamina / maxStamina;

        velocity.y += gravity * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speedToUse * Time.deltaTime);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
