using UnityEngine;

public class Item : MonoBehaviour
{
    public int valor = 1;
    public bool itemBom = true;

    public GameObject popupBom;
    public GameObject popupRuim;

    void Start()
    {
        valor = itemBom ? 1 : -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        GameManager.instance.Coletar(valor);

        GameObject popupEscolhido = itemBom ? popupBom : popupRuim;

        if (popupEscolhido != null)
        {
            Instantiate(
                popupEscolhido,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}