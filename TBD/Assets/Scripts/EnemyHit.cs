using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Determine if player hit spike
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
