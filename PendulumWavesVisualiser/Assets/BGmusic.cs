using UnityEngine;

public class BGmusic : MonoBehaviour
{
    private AudioSource bgMusic;
    
    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();  // Start music on awake
    }

    public void ToggleMusic()
    {
        bgMusic.mute = !bgMusic.mute; // Toggle mute on/off
    }
}
