using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendulumManager : MonoBehaviour
{
    public float maxOffset = 10.0f; // Maximum offset angle (in degrees)
    public float offsetStep = 2.0f; // Step decrement for offsets

    private Pendulum[] pendulums;
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    public Button resetButton;  // Assign Reset Button in Inspector
    public Toggle soundToggle;  // Assign Sound Toggle in Inspector

    void Start()
    {
        // Find all pendulums in the scene
        pendulums = FindObjectsOfType<Pendulum>();

        // Store original positions and rotations for resetting
        initialPositions = new Vector3[pendulums.Length];
        initialRotations = new Quaternion[pendulums.Length];

        // Sort pendulums by x-position for a smooth wave effect
        System.Array.Sort(pendulums, (a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

        float currentOffset = maxOffset;

        for (int i = 0; i < pendulums.Length; i++)
        {
            pendulums[i].offsetAngle = Mathf.Max(0, currentOffset);
            currentOffset -= offsetStep;

            // Store initial transform values for reset functionality
            initialPositions[i] = pendulums[i].transform.position;
            initialRotations[i] = pendulums[i].transform.rotation;
        }

        // Assign button functionality
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetPendulums);

        if (soundToggle != null)
            soundToggle.onValueChanged.AddListener(ToggleSound);
    }

    // 🔄 Reset All Pendulums
    public void ResetPendulums()
    {
        for (int i = 0; i < pendulums.Length; i++)
        {
            pendulums[i].transform.position = initialPositions[i];
            pendulums[i].transform.rotation = initialRotations[i];

            Rigidbody rb = pendulums[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    // 🔊 Toggle Sound On/Off
    public void ToggleSound(bool isOn)
    {
        foreach (PendulumDrag drag in FindObjectsOfType<PendulumDrag>())
        {
            if (drag.audioSource != null)
            {
                drag.audioSource.mute = !isOn;
            }
        }
    }
}
