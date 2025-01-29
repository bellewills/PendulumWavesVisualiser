using UnityEngine;

public class PendulumCollision : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the pendulum's AudioSource
    public AudioClip collisionSound; // Assign a ding sound in the Inspector

    void Start()
    {
        // Ensure we have an AudioSource attached
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only play the sound if colliding with another pendulum
        if (collision.gameObject.CompareTag("Pendulum"))
        {
            if (audioSource && collisionSound)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}
