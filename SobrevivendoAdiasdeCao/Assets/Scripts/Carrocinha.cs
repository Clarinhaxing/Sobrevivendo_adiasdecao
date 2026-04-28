using UnityEngine;

public class Carrocinha : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Derrota();
        }
    }
}