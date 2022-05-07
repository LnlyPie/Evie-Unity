using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    SoundManager soundMan = new SoundManager();

    [Header("Dialogues")]
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    [Header("Movement")]
    private float inputX;
    private float inputY;

    public float speedVar;
    public float jumpForceVar;
    private float speed;
    private float jumpForce;
    public static bool canMove = true;
    private bool jumpInput;
    private bool jumpInputPad;
    private bool facingRight = true;

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
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (!canMove) {
            speed = 0f;
            jumpForce = 0f;
        } else {
            speed = speedVar;
            jumpForce = jumpForceVar;
        }

        if ((!facingRight && inputX > 0 && canMove) || (facingRight && inputX < 0 && canMove)) {
            Flip();
        }

        if (isGrounded == true) {
            extraJumps = extraJumpsValue;
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (canMove) {
            inputX = context.ReadValue<Vector2>().x;

            anim.SetBool("moving", true);
        }

        if (anim.GetBool("jumping")) {
            anim.SetBool("moving", false);
        }

        if (context.canceled) {
            anim.SetBool("moving", false);
        }
    }

    public void OnSprint(InputAction.CallbackContext context) {
        rb.velocity = new Vector2(inputX * (speed * 1.55f), rb.velocity.y);
        jumpForce = jumpForceVar * 1.05f;
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (canMove && extraJumps > 0) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // soundMan.PlaySoundEffect("jump");
            extraJumps--;
            CreateDust();

            anim.SetBool("jumping", true);
        }

        if (context.canceled) {
            anim.SetBool("jumping", false);
        }
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (!DialogueUI.isOpen) {
            Interactable?.Interact(this);
        }
    }

    public void OnMouse(InputAction.CallbackContext context) {
        if (context.started) {
            Mouse.CursorClicked();
        } else
        {
            Mouse.CursorNormal();
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




    public void OnEsc(InputAction.CallbackContext context) {
        ESCMenu.ESCMenuCheck();
    }
}
