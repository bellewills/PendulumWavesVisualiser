using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPendulum : MonoBehaviour
{
    public void ResetScene()
    {
        Debug.Log("Reset button clicked! Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
