using UnityEngine;
using UnityEngine.UI;

public class InstructionText : MonoBehaviour
{
    public Text instructionText;
    public Text healthText;
    public Text livesText;
    public float displayTime = 3f;

    void Start()
    {
        instructionText.gameObject.SetActive(true);
        healthText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

        Invoke(nameof(HideInstructions), displayTime);
    }

    void HideInstructions()
    {
        instructionText.gameObject.SetActive(false);
        healthText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
    }
}
