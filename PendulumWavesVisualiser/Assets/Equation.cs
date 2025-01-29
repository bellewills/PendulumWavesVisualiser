using UnityEngine;
using UnityEngine.UI;

public class EquationInfo : MonoBehaviour
{
    public GameObject infoPanel; // Assign a UI Panel in the Inspector

    void Start()
    {
        // Ensure the panel is hidden at the start
        infoPanel.SetActive(false);
    }

    public void ShowExplanation()
    {
        // Toggle the panel visibility
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
