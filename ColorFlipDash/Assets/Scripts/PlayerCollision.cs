using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstacleColor obstacle = other.GetComponent<ObstacleColor>();
        if (obstacle != null)
        {
            bool playerIsBlack = spriteRenderer.color == Color.black;
            bool obstacleIsBlack = obstacle.type == ObstacleColor.ObstacleType.Black;

            if (playerIsBlack == obstacleIsBlack)
            {
                // Correct match
                ObstacleMover mover = other.GetComponent<ObstacleMover>();
                if (mover != null)
                    mover.MarkAsHandled();

                Destroy(other.gameObject);
            }
            else
            {
                // Wrong match
                GameManager.Instance.GameOver();
            }
        }
    }
}
