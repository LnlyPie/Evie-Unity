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
    private bool jumpInput;
    private bool jumpInputPad;
    private bool facingRight = true;

    public static bool unlimitedJumpMode;
    public bool fly_mode = false;
    private int extraJumps;
    public int extraJumpsValue;

    public Rigidbody2D rb;
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
        jumpInput = Input.GetKey(KeyCode.UpArrow);
        jumpInputPad = Input.GetKey("joystick button 0");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0) {
            Flip();
        } else if (facingRight == true && moveInput < 0) {
            Flip();
        }

        if (moveInput > 0f || moveInput < 0f)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        if (jumpInput || jumpInputPad)
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0 || Input.GetKeyDown("joystick button 0") && extraJumps > 0) {
            if (unlimitedJumpMode == true) {
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
            } else {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true || Input.GetKeyDown("joystick button 0") && extraJumps == 0 && isGrounded == true) {
            if (unlimitedJumpMode == true) {
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
            } else {
                rb.velocity = Vector2.up * jumpForce;
            }
        }


        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2")) {
            if (DialogueUI.isOpen == false) {
                Interactable?.Interact(this);
            }
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
