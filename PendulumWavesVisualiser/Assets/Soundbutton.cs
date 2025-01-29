using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Toggle soundToggle;   // Assign this in the Inspector
    public AudioSource bgMusic;  // Assign your background music AudioSource

    void Start()
    {
        // Auto-assign the AudioSource if not set in Inspector
        if (bgMusic == null)
        {
            bgMusic = FindObjectOfType<AudioSource>(); // Finds the first AudioSource in the scene
        }

        // Auto-assign the Toggle if not set
        if (soundToggle == null)
        {
            soundToggle = GetComponent<Toggle>();
        }

        if (soundToggle != null && bgMusic != null)
        {
            soundToggle.onValueChanged.AddListener(ToggleSound);
            soundToggle.isOn = !bgMusic.mute;  // Set toggle state based on mute status
        }
        else
        {
            Debug.LogError("SoundToggle: Missing Toggle or AudioSource!");
        }
    }

    void ToggleSound(bool isOn)
    {
        if (bgMusic == null) return;

        bgMusic.mute = !isOn; // Mutes when unchecked, plays when checked
    }
}
