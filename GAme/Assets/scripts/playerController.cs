using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player behaviour Settings")]
    public float speed = 4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    [Header("Define objects ")]
    public Transform cam;

    [Header("Animations")]
    private Animator animator;

    [Header("Scripts")]
    public gameManager gameManager;
    public DeliveryManager deliveryManager;

    [Header("UI")]
    public GameObject FToEnterText;
    public GameObject FToTalkToNpcText;

    [Header("Is in hitbox bools")]
    public bool IsInCarHitbox;
    public bool IsinDeliveryNpcHitbox;

    [Header("Others")]
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Get the Animator component here

        deliveryManager = FindObjectOfType<DeliveryManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        FToTalkToNpcText.SetActive(false);
    }
    
    void Update()
    {
        PlayerWalk();
        OpenDeliveryMenu();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InstantiatePlayer")
        {
            IsInCarHitbox = true;
            FToEnterText.SetActive(true);
        }

        if (other.gameObject.tag == "pickupZone")
        {
            FToTalkToNpcText.SetActive(true);
            IsinDeliveryNpcHitbox = true;
        }

        if (other.gameObject.tag == "parcel")
        {
            
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InstantiatePlayer")
        {
            IsInCarHitbox = false;
            FToEnterText.SetActive(false);
        }

        if (other.gameObject.tag == "pickupZone")
        {
            FToTalkToNpcText.SetActive(false);
            IsinDeliveryNpcHitbox = false;
        }
    }

    void OpenDeliveryMenu()
    {
        if (IsinDeliveryNpcHitbox)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                deliveryManager.OpenMenu();
                FToTalkToNpcText.SetActive(false);
            }
        }
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

        velocity.y += gravity * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
