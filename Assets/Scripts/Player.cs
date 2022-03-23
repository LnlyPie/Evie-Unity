using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    SoundManager soundMan = new SoundManager();

    public static bool ghost_mode = false;

    [Header("Dialogues")]
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    [Header("Movement")]
    public float speedVar;
    public float jumpForceVar;
    private float speed;
    private float jumpForce;
    public static bool canMove = true;
    private float moveInput;
    private bool jumpInput;
    private bool jumpInputPad;
    private bool facingRight = true;

    public static bool unlimitedJumpMode;
    public static bool fly_mode = false;
    private int extraJumps;
    public int extraJumpsValue;
    private int jumps;
    public static float gravity = 2.0f;

    public static Rigidbody2D rb;
    public static BoxCollider2D bc;
    private Animator anim;

    private bool isGrounded;
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Particles")]
    public ParticleSystem dust;

    private void Start() {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = gravity;
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey(KeyCode.UpArrow);
        jumpInputPad = Input.GetKey("joystick button 0");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (!canMove) {
            speed = 0f;
            jumpForce = 0f;
        } else {
            speed = speedVar;
            jumpForce = jumpForceVar;
        }

        if (!facingRight && moveInput > 0 && canMove) {
            Flip();
        } else if (facingRight && moveInput < 0 && canMove) {
            Flip();
        }

        if ((moveInput > 0f || moveInput < 0f) && canMove)
        {
            anim.SetBool("moving", true);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey("joystick button 4")) {
                rb.velocity = new Vector2(moveInput * (speed * 1.55f), rb.velocity.y);
                jumpForce = jumpForceVar * 1.05f;
            }
        }
        else
        {
            anim.SetBool("moving", false);
        }

        if (jumpInput || jumpInputPad && canMove)
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
            jumps = (extraJumps + 1);
        }

        if ( (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("joystick button 0")) && extraJumps > 0 && canMove) {
            if (unlimitedJumpMode == true) {
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
                soundMan.PlaySoundEffect("jump");
            } else {
                CreateDust();
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
                if (jumps == 2) {
                    jumps--;
                    soundMan.PlaySoundEffect("jump");
                } else if (jumps == 1) {
                    jumps--;
                    soundMan.PlaySoundEffect("jump2");
                }
            }
        } else if ( (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("joystick button 0")) && extraJumps == 0 && isGrounded && canMove) {
            if (unlimitedJumpMode) {
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
                soundMan.PlaySoundEffect("jump");
            } else {
                rb.velocity = Vector2.up * jumpForce;
            }
        }


        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2")) {
            if (!DialogueUI.isOpen) {
                Interactable?.Interact(this);
            }
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        if (isGrounded) {
            CreateDust();
        }
    }

    void CreateDust() {
        dust.Play();
    }
}
