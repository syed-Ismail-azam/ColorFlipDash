using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.3f); // destroy after 0.1 seconds
    }
}
