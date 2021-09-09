using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask movementMask;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float jumpCooldown = 0f;

    float moveZ;
    float moveX;

    private CharacterController controller;
    private Animator anim;
    private PlayerStats stats;
    private Camera cam;
    public Interactable focus;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<PlayerStats>();
        cam = Camera.main;
    }

    private void Update()
    {
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/

        jumpCooldown += Time.deltaTime;

        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                // display info
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    focus.Interact();
                }
            }
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
        }

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0f, moveZ);

        if (moveZ != 0 && moveX !=0)
        {
            moveDirection = transform.TransformDirection(moveDirection / 1.4142f);
        } else
        {
            moveDirection = transform.TransformDirection(moveDirection);
        }

        if (isGrounded)
        {
            if ((moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) || Input.GetKey(KeyCode.S))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if (Input.GetButtonDown("Jump") && jumpCooldown > .7f)
            {
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);

        //tu było blisko!
        /*transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(moveDirection), Time.deltaTime * 2f);*/
        
        //tu jeszcze bliżej!!
        /*transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);*/

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = stats.walkSpeed;
        anim.SetFloat("Speed", .5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = stats.runSpeed;
        anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);

    }

    private void Jump()
    {
        velocity += moveDirection * 0.75f;
        velocity.y = Mathf.Sqrt(stats.jumpHeight * -2f * gravity);
        anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        jumpCooldown = 0f;
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
    }
}
