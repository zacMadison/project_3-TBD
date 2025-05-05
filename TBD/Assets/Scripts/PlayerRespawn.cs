using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Animator anim;
    private float startAnimationTime = 0.5f;
    private float animationTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Respawn()
    {
        if (currentCheckpoint)
        {
            anim.SetTrigger("die");
            transform.position = currentCheckpoint.position; // move player to checkpoint
            StartCoroutine(DelayResetTrigger(anim));
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Store checkpoint player moves over
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
        }
    }

    private IEnumerator DelayResetTrigger(Animator anim)
    {
        animationTime = startAnimationTime;

        while (animationTime > 0)
        {
            animationTime -= Time.deltaTime;
            yield return null;
        }

        anim.ResetTrigger("die");
        anim.Play("Idle");

    }
}
