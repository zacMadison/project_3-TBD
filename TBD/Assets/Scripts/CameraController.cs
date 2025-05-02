using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceAhead;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (distanceAhead * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}
