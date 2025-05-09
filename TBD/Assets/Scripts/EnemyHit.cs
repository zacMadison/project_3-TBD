using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Determine if player hit spike
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("die");
        }
    }
}
