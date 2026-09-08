using UnityEngine;

public class ChefeCarrocinhaTutorial : MonoBehaviour
{
    [Header("Configuração")]
    public Transform player;
    public float velocidadeRonda = 2f;

    [Header("Estado")]
    public bool chefeAtivo = false;

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;

        // O chefe começa parado/inativo durante o intervalo
        chefeAtivo = false;
    }

    void Update()
    {
        if (!chefeAtivo)
            return;

        FazerRonda();
    }

    // =====================================================
    // RONDA
    // =====================================================

    void FazerRonda()
    {
        // Por enquanto o chefe anda horizontalmente.
        // Vamos melhorar a ronda depois.

        transform.Translate(
            Vector2.right * velocidadeRonda * Time.deltaTime
        );
    }

    // =====================================================
    // LIBERAR CHEFE
    // =====================================================

    public void LiberarChefe()
    {
        chefeAtivo = true;

        Debug.Log("🐶 O chefe voltou e começou a ronda!");
    }

    // =====================================================
    // DESATIVAR
    // =====================================================

    public void DesativarChefe()
    {
        chefeAtivo = false;
    }
}