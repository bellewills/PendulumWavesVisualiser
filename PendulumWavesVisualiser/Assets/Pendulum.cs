using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float length = 2.0f;  // Length of the pendulum
    public float gravity = 9.81f; // Gravity acceleration
    public float maxAngle = 30.0f; // Max swing angle
    public float dampingFactor = 0.99f; // Lower = faster stop (0.99 = slow, 0.95 = fast)
    public float offsetAngle = 0.0f; // 🛠 Offset for wave effect

    private Rigidbody rb;
    private float angularFrequency;
    private float elapsedTime = 0.0f;
    private bool isDragging = false;
    private float currentMaxAngle; // Keeps track of gradually reducing swing

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate angular frequency (SHM formula)
        angularFrequency = Mathf.Sqrt(gravity / length);

        // Apply initial offset for wave-like effect
        elapsedTime = offsetAngle / maxAngle / angularFrequency;

        // Set initial max angle
        currentMaxAngle = maxAngle;
    }

    void Update()
    {
        if (!isDragging)
        {
            elapsedTime += Time.deltaTime;

            // Reduce max angle over time (simulating air resistance)
            if (currentMaxAngle > 0.1f) // Prevents infinite movement
            {
                currentMaxAngle *= dampingFactor; // Gradually reduce swing amplitude
            }
            else
            {
                currentMaxAngle = 0f; // Stop completely
            }

            // Simple Harmonic Motion (SHM) with damping
            float angle = currentMaxAngle * Mathf.Cos(angularFrequency * elapsedTime);

            // Apply rotation to pendulum
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Called from PendulumDrag.cs when user starts dragging
    public void StartDragging()
    {
        isDragging = true;
        rb.useGravity = false;
    }

    // Called from PendulumDrag.cs when user releases drag
    public void StopDragging()
    {
        isDragging = false;
        rb.useGravity = true;
        elapsedTime = 0.0f; // Reset motion timer
        currentMaxAngle = maxAngle; // Reset damping effect
    }
}
