using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    // Configurações do pulo
    public float jumpHeight = 3f;
    public float jumpDuration = 0.8f;
    public int maxJumps = 2;

    private Rigidbody2D rb;
    private Animator anim;

    private float moveInput;
    private int jumpCount;
    private bool isGrounded;

    private bool isJumping;
    private float jumpTime;
    private float startY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        // Animação de andar
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            StartJump();
        }

        // Atualiza a parábola
        if (isJumping)
        {
            UpdateJump();
        }

        // Latido
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Bark();
        }

        anim.SetBool("Grounded", isGrounded);

        // Virar personagem
        if (moveInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        // Movimento horizontal
        rb.linearVelocity = new Vector2(
            moveInput * speed,
            rb.linearVelocity.y
        );
    }

    void StartJump()
    {
        isJumping = true;
        jumpTime = 0f;

        // Guarda a altura em que começou o pulo
        startY = transform.position.y;

        jumpCount++;

        // Desliga temporariamente a física vertical
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        isGrounded = false;
    }

    void UpdateJump()
    {
        jumpTime += Time.deltaTime / jumpDuration;

        // Garante que fique entre 0 e 1
        float t = Mathf.Clamp01(jumpTime);

        // PARÁBOLA
        float height = 4f * jumpHeight * t * (1f - t);

        // Mantém a posição horizontal e altera somente Y
        transform.position = new Vector3(
            transform.position.x,
            startY + height,
            transform.position.z
        );

        // Terminou o pulo
        if (t >= 1f)
        {
            isJumping = false;

            transform.position = new Vector3(
                transform.position.x,
                startY,
                transform.position.z
            );

            rb.gravityScale = 1f;

            isGrounded = true;
        }
    }

    void Bark()
    {
        anim.SetTrigger("Bark");
        Debug.Log("Latido!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isJumping)
            {
                isGrounded = true;
                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isJumping)
            {
                isGrounded = false;
            }
        }
    }
}