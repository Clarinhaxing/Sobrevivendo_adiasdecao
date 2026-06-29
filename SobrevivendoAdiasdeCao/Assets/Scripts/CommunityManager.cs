using UnityEngine;
using UnityEngine.UI;

public class CommunityManager : MonoBehaviour
{
    public static CommunityManager instance;

    [Header("Status da Comunidade")]
    [Range(0, 100)] public float alimentacao = 100f;
    [Range(0, 100)] public float saude = 100f;
    [Range(0, 100)] public float felicidade = 100f;

    private float bonusFelicidade = 0f;

    [Header("Barras")]
    public Slider barraAlimentacao;
    public Slider barraSaude;
    public Slider barraFelicidade;

    [Header("Configuração")]
    public float perdaAlimentacaoPorSegundo = 4f;

    public float perdaSaudeNatural = 0.5f;
    public float recuperacaoSaude = 1.5f;
    public float perdaSaudeLeve = 2f;
    public float perdaSaudeGrave = 5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AtualizarUI();
    }

    private void Update()
    {
        AtualizarComunidade();
        AtualizarUI();
    }

    void AtualizarComunidade()
    {
        // Alimentação sempre cai
        alimentacao -= perdaAlimentacaoPorSegundo * Time.deltaTime;

        // Saúde naturalmente se desgasta
        saude -= perdaSaudeNatural * Time.deltaTime;

        // Alimentação afeta saúde
        if (alimentacao >= 70)
        {
            saude += recuperacaoSaude * Time.deltaTime;
        }
        else if (alimentacao < 50 && alimentacao >= 25)
        {
            saude -= perdaSaudeLeve * Time.deltaTime;
        }
        else if (alimentacao < 25)
        {
            saude -= perdaSaudeGrave * Time.deltaTime;
        }

        // Felicidade depende da comunidade
        felicidade = ((alimentacao + saude) / 2f) + bonusFelicidade;

        // bônus desaparece aos poucos
        bonusFelicidade -= 2f * Time.deltaTime;

        alimentacao = Mathf.Clamp(alimentacao, 0, 100);
        saude = Mathf.Clamp(saude, 0, 100);
        felicidade = Mathf.Clamp(felicidade, 0, 100);
        bonusFelicidade = Mathf.Clamp(bonusFelicidade, 0, 30);
    }

    void AtualizarUI()
    {
        if (barraAlimentacao != null)
            barraAlimentacao.value = alimentacao;

        if (barraSaude != null)
            barraSaude.value = saude;

        if (barraFelicidade != null)
            barraFelicidade.value = felicidade;
    }

    // =====================
    // RECOMPENSAS DAS FASES
    // =====================

    public void AdicionarComida(float valor)
    {
        alimentacao += valor;
        alimentacao = Mathf.Clamp(alimentacao, 0, 100);
    }

    public void AdicionarRemedios(float valor)
    {
        saude += valor;
        saude = Mathf.Clamp(saude, 0, 100);
    }

    public void AdicionarDiversao(float valor)
    {
        bonusFelicidade += valor;
        bonusFelicidade = Mathf.Clamp(bonusFelicidade, 0, 30);
    }

    // =====================
    // CONSULTAS
    // =====================

    public bool FomeCritica()
    {
        return alimentacao < 15;
    }

    public bool FomeBaixa()
    {
        return alimentacao < 40;
    }

    public bool SaudeCritica()
    {
        return saude < 15;
    }

    public bool SaudeBaixa()
    {
        return saude < 40;
    }

    public bool FelicidadeCritica()
    {
        return felicidade < 15;
    }

    public bool FelicidadeBaixa()
    {
        return felicidade < 40;
    }
}