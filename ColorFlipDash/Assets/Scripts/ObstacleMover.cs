using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Destroy if it moves too far off screen
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
