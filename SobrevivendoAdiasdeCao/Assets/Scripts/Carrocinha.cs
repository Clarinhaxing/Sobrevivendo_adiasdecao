using UnityEngine;

public class Carrocinha : MonoBehaviour
{
    [Header("Perseguição")]
    public Transform player;
    public float speed = 4f;

    [Header("Ataque")]
    public int dano = 1;
    public float intervaloEntreGolpes = 1.2f;

    private float proximoAtaque = 0f;

    private PlayerHealth playerHealth;

    void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Persegue Sandy
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    // =====================================================
    // ATAQUE
    // =====================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        TentarGolpear(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        TentarGolpear(collision.gameObject);
    }

    void TentarGolpear(GameObject objetoPlayer)
    {
        if (Time.time < proximoAtaque)
            return;

        if (playerHealth == null)
        {
            playerHealth = objetoPlayer.GetComponent<PlayerHealth>();
        }

        if (playerHealth == null)
        {
            Debug.LogWarning(
                "A Sandy não possui PlayerHealth!"
            );

            return;
        }

        // Aplica dano
        playerHealth.LevarDano(dano);

        // Define quando poderá atacar novamente
        proximoAtaque =
            Time.time + intervaloEntreGolpes;

        Debug.Log(
            "Carrocinha golpeou Sandy!"
        );
    }
}