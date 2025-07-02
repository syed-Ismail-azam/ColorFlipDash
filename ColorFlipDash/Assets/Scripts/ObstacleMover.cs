using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private bool isHandled = false;

    void Update()
    {
        if (isHandled) return;

        float speed = GameManager.Instance.GetCurrentObstacleSpeed();
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        if (transform.position.y < -6f)
        {
            isHandled = true;
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }

    public void MarkAsHandled()
    {
        isHandled = true;
    }
}
