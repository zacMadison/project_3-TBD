using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Determine if player hit spike
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("die");
        }
    }
}
