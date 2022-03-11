using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }


    // Speed & Movement
    public float speed;
    public float jumpForce;
    private float moveInput;
    private float jumpFloat;
    private bool facingRight = true;

    public static bool unlimitedJumpMode;
    private int extraJumps;
    public int extraJumpsValue;

    private Rigidbody2D rb;
    private Animator anim;

    // Ground
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private void Start() {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        jumpFloat = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0) {
            Flip();
        } else if (facingRight == true && moveInput < 0) {
            Flip();
        }

        if (moveInput > 0f | moveInput < 0f)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        if (jumpFloat > 0f | jumpFloat < 0f)
        {
            anim.SetBool("moving", false);
            anim.SetBool("jumping", true);
        }
        else
        {
            anim.SetBool("jumping", false);
        }
    }

    private void Update() {
        if (isGrounded == true) {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0) {
            if (!unlimitedJumpMode == true) {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            } else {
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true) {
            if (!unlimitedJumpMode == true) {
                rb.velocity = Vector2.up * jumpForce;
            } else {
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
            }
        }


        if (Input.GetKeyDown(KeyCode.E)) {
            Interactable?.Interact(this);
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
