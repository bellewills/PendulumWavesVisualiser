using UnityEngine;

public class PendulumDrag : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;
    private Plane dragPlane;
    private Camera mainCamera;

    // Dragging Sensitivity & Limits
    public float dragForceMultiplier = 20f; // Adjusts how strong the pull is
    public float maxSwingAngle = 45f;       // Limits max rotation
    public float dampingFactor = 0.99f;     // Slows down swing naturally

    // Visual Feedback
    private Material originalMaterial;
    public Material highlightMaterial;
    private Renderer pendulumRenderer;

    // Audio Feedback
    public AudioSource audioSource;
    public AudioClip dragSound;
    public AudioClip releaseSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        pendulumRenderer = GetComponent<Renderer>();
        originalMaterial = pendulumRenderer.material;
    }

    void OnMouseDown()
    {
        isDragging = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        dragPlane = new Plane(Vector3.forward, transform.position);
        pendulumRenderer.material = highlightMaterial;

        if (audioSource && dragSound)
        {
            audioSource.PlayOneShot(dragSound);
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);

                // Convert movement to torque
                float forceAmount = (targetPosition.x - transform.position.x) * dragForceMultiplier;
                rb.AddTorque(Vector3.forward * Mathf.Clamp(forceAmount, -maxSwingAngle, maxSwingAngle), ForceMode.Acceleration);
            }
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            rb.useGravity = true;
            pendulumRenderer.material = originalMaterial;

            if (audioSource && releaseSound)
            {
                audioSource.PlayOneShot(releaseSound);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDragging)
        {
            // Apply damping to slow down motion naturally
            rb.angularVelocity *= dampingFactor;
        }
    }
}
