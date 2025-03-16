using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 5f; 
    public float jumpHeight = 1f; 
    public float gravity = -10f;  
    public float mouseSensitivity = 2f; 
    public Transform playerCamera; 
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float verticalRotation = 0f;
    public TMP_Text interactText;
    public float interactDistance = 3f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        isGrounded = characterController.isGrounded; 
        Move();
        Jump(); 
        ApplyGravity();
        Look();
        CheckInteraction();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {     
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Look()
    {       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
    void CheckInteraction()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "E - Etkileþime Geç";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }
}
