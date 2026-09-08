using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    [FormerlySerializedAs("energiaMaxima")]
    public int vidaMaxima = 5;

    [FormerlySerializedAs("energiaAtual")]
    public int vidaAtual;

    [Header("UI")]
    [FormerlySerializedAs("barraEnergia")]
    public Slider barraVida;

    [Header("Velocidade")]
    public float velocidadeBase = 7f;

    // Redução da velocidade conforme a vida diminui
    public float velocidadeVida5 = 1f;
    public float velocidadeVida4 = 0.9f;
    public float velocidadeVida3 = 0.8f;
    public float velocidadeVida2 = 0.7f;
    public float velocidadeVida1 = 0.6f;

    private PlayerMovement movement;

    private bool morreu = false;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();

        vidaAtual = vidaMaxima;

        AtualizarUI();
        AtualizarVelocidade();
    }

    // =====================================================
    // RECEBER DANO
    // =====================================================

    public void LevarDano(int dano)
    {
        if (morreu)
            return;

        // Diminui a vida
        vidaAtual -= dano;

        // Impede que fique negativa
        vidaAtual = Mathf.Max(vidaAtual, 0);

        // Atualiza somente porque Sandy foi atacada
        AtualizarUI();
        AtualizarVelocidade();

        Debug.Log("Sandy perdeu vida!");
        Debug.Log("Vida atual: " + vidaAtual);
        Debug.Log("Velocidade atual: " + movement.speed);

        // Se acabou a vida
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    // =====================================================
    // ATUALIZAR BARRA DE VIDA
    // =====================================================

    void AtualizarUI()
    {
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaAtual;
        }
    }

    // =====================================================
    // ATUALIZAR VELOCIDADE
    // =====================================================

    void AtualizarVelocidade()
    {
        if (movement == null)
            return;

        float multiplicador = 1f;

        switch (vidaAtual)
        {
            case 5:
                multiplicador = velocidadeVida5;
                break;

            case 4:
                multiplicador = velocidadeVida4;
                break;

            case 3:
                multiplicador = velocidadeVida3;
                break;

            case 2:
                multiplicador = velocidadeVida2;
                break;

            case 1:
                multiplicador = velocidadeVida1;
                break;

            case 0:
                multiplicador = 0f;
                break;
        }

        movement.speed = velocidadeBase * multiplicador;
    }

    // =====================================================
    // MORTE
    // =====================================================

    void Morrer()
    {
        if (morreu)
            return;

        morreu = true;

        Debug.Log("Sandy foi capturada!");

        if (movement != null)
        {
            movement.speed = 0f;
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.Derrota();
        }
    }
}