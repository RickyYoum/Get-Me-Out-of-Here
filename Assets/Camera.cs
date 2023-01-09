using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float offset;
    public float offsetSmoothing = 0.125f;
    private Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
        // Get the player's position
        playerPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // If the player is facing right, change player position towards the right
        if (player.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        // Vice versa
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset*5, playerPosition.y, playerPosition.z);
        }
        
        // Switch the position of the camera
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
