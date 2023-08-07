using UnityEngine;

public class CircleCollision : MonoBehaviour
{
    public GameObject circle;
    public float destroyInterval = 3.0f; // Time interval after collision to destroy the circle
    private float timeElapsed = 0.0f;
    private bool destroyStarted = false;

    private void Update()
    {
        if (destroyStarted)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= destroyInterval)
            {
                Destroy(circle);
            }
        }
    }

    public void StartDestroy()
    {
        destroyStarted = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDestroy();
        }
    }
}
