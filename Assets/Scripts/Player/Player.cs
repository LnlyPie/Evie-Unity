using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    SoundManager soundMan = new SoundManager();

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
    private bool facingRight = true;

    private int extraJumps;
    public int extraJumpsValue;
    private int jumps;
    public static float gravity = 2.0f;

    public float dashDistance = 15f;
    bool isDashing;

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
    public ParticleSystem dash;

    // Cheats
    public static bool ghost_mode = false;
    public static bool unlimitedJumpMode = false;
    public static bool fly_mode = false;

    private void Start() {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = gravity;
    }

    private void Update() {
        // Less-Movement related stuff
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("joystick button 0"));

        if (!canMove) {
            speed = 0f;
            jumpForce = 0f;
        } else if (isDashing) {
            speed = (speedVar * 2);
        } else {
            speed = speedVar;
            jumpForce = jumpForceVar;
        }

        if ((!facingRight && (moveInput > 0f) && canMove) || (facingRight && (moveInput < 0f) && canMove)) {
            Flip();
        }

        if (isGrounded) {
            extraJumps = extraJumpsValue;
        }


        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))) {
            if (!DialogueUI.isOpen) {
                Interactable?.Interact(this);
            }
        }

        // More-Movement related stuff
        //   Movement
        if (!isDashing) {
            if ((moveInput > 0f || moveInput < 0f) && canMove) {
                anim.SetBool("moving", true);

                if ( (Input.GetKey(KeyCode.LeftShift) || Input.GetKey("joystick button 4")) ) {
                    rb.velocity = new Vector2(moveInput * (speed * 1.40f), rb.velocity.y);
                    jumpForce = jumpForceVar * 1.05f;
                }
            } else {
                anim.SetBool("moving", false);
            }
        }

        //   Jumping
        if (jumpInput) {
            if (canMove && extraJumps > 0) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                soundMan.PlaySoundEffect("jump");
                extraJumps--;
                CreateDust();
            } else if (unlimitedJumpMode && canMove) {
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, speed+5);
            }
        }

        //   Dashing
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown("joystick button 5")) && moveInput > 0f) {
            StartCoroutine(Dash(1f));
        } else if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown("joystick button 5")) && moveInput < 0f) {
            StartCoroutine(Dash(-1f));
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

    private IEnumerator Dash(float direction) {
        CreateDashWind();
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;
    }

    void CreateDust() {
        dust.Play();
    }

    void CreateDashWind()
    {
        dash.Play();
    }
}
