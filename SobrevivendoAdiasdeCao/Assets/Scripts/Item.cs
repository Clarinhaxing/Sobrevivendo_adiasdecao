using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Configuração do Item")]

    public int valor = 1;
    public bool itemBom = true;


    // =========================================================
    // TIPO DO RECURSO
    // =========================================================

    public enum TipoRecurso
    {
        Comida,
        Remedio,
        Diversao
    }


    [Header("Tipo do Recurso")]

    public TipoRecurso tipoRecurso;


    // =========================================================
    // POPUPS
    // =========================================================

    [Header("Popups")]

    public GameObject popupBom;
    public GameObject popupRuim;


    // =========================================================
    // INÍCIO
    // =========================================================

    void Start()
    {
        // Item bom = +1
        // Item ruim = -1

        valor = itemBom ? 1 : -1;
    }


    // =========================================================
    // COLETA
    // =========================================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;


        // =====================================================
        // ITEM BOM
        // =====================================================

        if (itemBom)
        {
            AdicionarAoInventario();
        }


        // =====================================================
        // ITEM RUIM
        // =====================================================

        if (!itemBom)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.Coletar(valor);
            }
        }


        // =====================================================
        // POPUP
        // =====================================================

        GameObject popupEscolhido =
            itemBom ? popupBom : popupRuim;

        if (popupEscolhido != null)
        {
            Instantiate(
                popupEscolhido,
                transform.position,
                Quaternion.identity
            );
        }


        // =====================================================
        // DESTRUIR ITEM
        // =====================================================

        Destroy(gameObject);
    }


    // =========================================================
    // ADICIONAR RECURSO
    // =========================================================

    private void AdicionarAoInventario()
    {
        // Se não existir inventário,
        // mostramos um erro no Console.

        if (ResourceInventory.instance == null)
        {
            Debug.LogError(
                "ResourceInventory não encontrado na cena!"
            );

            return;
        }


        switch (tipoRecurso)
        {
            case TipoRecurso.Comida:

                ResourceInventory.instance.AdicionarComida(valor);

                break;


            case TipoRecurso.Remedio:

                ResourceInventory.instance.AdicionarRemedio(valor);

                break;


            case TipoRecurso.Diversao:

                ResourceInventory.instance.AdicionarDiversao(valor);

                break;
        }


        Debug.Log(
            "Recurso coletado: " + tipoRecurso
        );
    }
}