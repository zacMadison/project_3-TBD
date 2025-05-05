using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    public void Respawn()
    {
        if (currentCheckpoint)
        {
            transform.position = currentCheckpoint.position; // move player to checkpoint
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
}
