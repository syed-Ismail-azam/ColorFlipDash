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

          
            if (obstacle.type == ObstacleColor.ObstacleType.Fraud)
            {
                GameManager.Instance.GameOver();  // Touching fraud ? Game over
                return;
            }

            bool playerIsGreen = spriteRenderer.color == new Color(33f / 255f, 255f / 255f, 255f / 255f);
            bool obstacleIsGreen = obstacle.type == ObstacleColor.ObstacleType.green;

            if (playerIsGreen == obstacleIsGreen)
            {

                //  Spawn effect
                if (GameManager.Instance.hitEffectPrefab != null)
                {
                    Instantiate(GameManager.Instance.hitEffectPrefab, other.transform.position, Quaternion.identity);
                }

                // Correct match
                ObstacleMover mover = other.GetComponent<ObstacleMover>();
                if (mover != null)
                    mover.MarkAsHandled();

                GameManager.Instance.AddScore(1); // ? Add score here
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
