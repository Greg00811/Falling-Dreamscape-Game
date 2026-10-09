using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; //gets player position
    [SerializeField] private Rigidbody2D playerRb; //gets access to player rigidbody component
    [SerializeField] private float followSpeed = 3f; //defines camera follow speed
    [SerializeField] private float verticalOffset = 1.5f; //sets a camera offset

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        float fallingSpeed = Mathf.Abs(playerRb.linearVelocity.y); //stores how fast the player is falling
        float cameraSpeed = fallingSpeed + followSpeed; //calculates speed of camera 

        //stores camera's x and z value as well as the player's position on the y-axis
        Vector3 targetPosition = new Vector3(
            transform.position.x, 
            player.position.y - verticalOffset, 
            transform.position.z
        );

        //calculates a position between current and desired location to allow for smooth following
        transform.position = Vector3.MoveTowards(
            transform.position, //camera current position
            targetPosition, //desired camera position
            cameraSpeed * Time.deltaTime //multiplies the follow speed by how much time has passed since the last frame
        );
    }
}
