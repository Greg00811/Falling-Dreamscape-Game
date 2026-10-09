using UnityEngine;

public class Clouds : MonoBehaviour
{

    private float speed;
    private float previousCameraY;

    private GameManager gameManager;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.localScale = transform.localScale * Random.Range(0.5f, 1.5f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Random.Range(0.1f, 0.7f));
        speed = Random.Range(1f, 3f) * gameManager.cloudMove;
        cameraTransform = Camera.main.transform;
        previousCameraY = cameraTransform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float cameraYDelta = cameraTransform.position.y - previousCameraY;

        transform.position += new Vector3(
            0f,
            cameraYDelta + speed * Time.deltaTime,
            0f
        );

        previousCameraY = cameraTransform.position.y;

        if (transform.position.y > cameraTransform.position.y + gameManager.verticalScreenSize)
        {
            transform.position = new Vector3(
                Random.Range(-gameManager.horizontalScreenSize, gameManager.horizontalScreenSize),
                cameraTransform.position.y - gameManager.verticalScreenSize - 1f,
                transform.position.z
            );
        }
        
    }
}
