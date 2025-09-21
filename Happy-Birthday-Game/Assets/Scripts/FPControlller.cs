
using UnityEngine;
using UnityEngine.InputSystem;
//Reference:Unity Technologies (2025) Input System Manual. Available at: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/index.html.(Accessed: 18 August 2025)
//Reference:OpenAI, 2025. ChatGPT (GPT-5 mini) [AI language model]. Personal assistance with OnCollect Callback. 19 August 2025.



public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 3f;
    public float verticalLookLimit = 90f;

    [Header("UI Settings")]
    public GameObject InventoryMenu; 

    private bool isInventoryOpen = false;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private PlayerControls controls;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controls = new PlayerControls();
        controls.Player.Interact.performed += OnInteract; 
        controls.Player.Collect.performed += OnCollect;
        controls.Player.OpenInventory.performed += OnOpenInventory;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

 

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    
    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f)) // 3f = range
            {
                Collectible collectible = hit.collider.GetComponent<Collectible>();
                if (collectible != null)
                {
             
                    PlayerInventory inventory = GetComponent<PlayerInventory>();
                    if (inventory != null)
                    {
                        inventory.CakeIngrediantCollected();
                        Debug.Log("Collected: " + collectible.ingredientName +
                                  " | Total: " + inventory.NumberOfCakeIngrediants);
                    }

                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInventoryOpen = !isInventoryOpen;
            InventoryMenu.SetActive(isInventoryOpen);

            if (isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }




    public LayerMask groundMask;
    public void HandleMovement()
    {
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        float deltaMove = moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + moveDirection * deltaMove;

        float rayDownDistance = 1.5f;
        float sideOffset = 0.3f;
        float edgeBuffer = 0.1f;

        Vector3 originCenter = newPosition + Vector3.up * 0.1f;
        Vector3 originLeft = originCenter - transform.right * sideOffset;
        Vector3 originRight = originCenter + transform.right * sideOffset;

        bool groundCenter = Physics.Raycast(originCenter, Vector3.down, rayDownDistance, groundMask);
        bool groundLeft = Physics.Raycast(originLeft, Vector3.down, rayDownDistance, groundMask);
        bool groundRight = Physics.Raycast(originRight, Vector3.down, rayDownDistance, groundMask);

        if (!groundCenter || !groundLeft || !groundRight)
        {
            Vector3 forwardDir = transform.forward * moveInput.y * deltaMove;
            Vector3 testPosition = transform.position + forwardDir;
            bool bufferGround = Physics.Raycast(testPosition + Vector3.up * 0.1f, Vector3.down, rayDownDistance + edgeBuffer, groundMask);

            if (!bufferGround)
            {
                moveDirection = transform.right * moveInput.x;
            }
        }

        controller.Move(moveDirection * deltaMove);


        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);




        ////normal movement 
        //Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        //controller.Move(move * moveSpeed * Time.deltaTime);

        //if (controller.isGrounded && velocity.y < 0)
        //    velocity.y = -2f;

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);




    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

   
    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("E is pressed"); 

        if (context.performed)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);   

            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>(); 

                if (interactable != null)
                {
                    interactable.Interact();
                }
            }

        }
    }



}
