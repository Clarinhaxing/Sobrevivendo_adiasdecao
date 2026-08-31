using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    public static ResourceInventory instance;


    [Header("Recursos coletados")]

    public int comida = 0;
    public int remedios = 0;
    public int diversao = 0;


    [Header("Valor de cada recurso")]

    public float valorComida = 5f;
    public float valorRemedio = 5f;
    public float valorDiversao = 5f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // =========================================================
    // ADICIONAR
    // =========================================================

    public void AdicionarComida(int quantidade)
    {
        comida += quantidade;

        Debug.Log(
            "Comida coletada! Total: " + comida
        );
    }


    public void AdicionarRemedio(int quantidade)
    {
        remedios += quantidade;

        Debug.Log(
            "Remédios coletados! Total: " + remedios
        );
    }


    public void AdicionarDiversao(int quantidade)
    {
        diversao += quantidade;

        Debug.Log(
            "Itens de diversão coletados! Total: " + diversao
        );
    }


    // =========================================================
    // ENTREGAR RECURSOS
    // =========================================================

    public void EntregarRecursos()
    {
        if (CommunityManager.instance == null)
        {
            Debug.LogError(
                "CommunityManager não encontrado!"
            );

            return;
        }


        // COMIDA

        if (comida > 0)
        {
            float valorTotal =
                comida * valorComida;

            CommunityManager.instance.AdicionarComida(
                valorTotal
            );
        }


        // REMÉDIOS

        if (remedios > 0)
        {
            float valorTotal =
                remedios * valorRemedio;

            CommunityManager.instance.AdicionarRemedios(
                valorTotal
            );
        }


        // DIVERSÃO

        if (diversao > 0)
        {
            float valorTotal =
                diversao * valorDiversao;

            CommunityManager.instance.AdicionarDiversao(
                valorTotal
            );
        }


        Debug.Log(
            "Recursos entregues! " +
            "Comida: " + comida +
            " | Remédios: " + remedios +
            " | Diversão: " + diversao
        );


        LimparInventario();
    }


    // =========================================================
    // LIMPAR INVENTÁRIO
    // =========================================================

    public void LimparInventario()
    {
        comida = 0;
        remedios = 0;
        diversao = 0;
    }
}