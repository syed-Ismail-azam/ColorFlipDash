using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private bool isHandled = false;

    void Update()
    {
        if (isHandled) return;

        float speed = GameManager.Instance.GetCurrentObstacleSpeed();
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Reached bottom of screen
        if (transform.position.y < -6f)
        {
            ObstacleColor colorType = GetComponent<ObstacleColor>();

            if (colorType != null && colorType.type != ObstacleColor.ObstacleType.Fraud)
            {
                GameManager.Instance.GameOver(); // Only trigger game over if it's NOT fraud
            }

            Destroy(gameObject);
        }
    }

    public void MarkAsHandled()
    {
        isHandled = true;
    }
}
