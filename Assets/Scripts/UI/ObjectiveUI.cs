using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI interactionText;

    private void Start() 
    {
        if(GroundingManager.Instance != null)
        {
            GroundingManager.Instance.SetObjectiveUI(this);
        }
    }

    public void ShowInteractionMessage(string message)
    {
        if(interactionText != null)
        {
            interactionText.text = message;
        }
    }

    public void UpdateObjective(string message)
    {
        objectiveText.text = message;
    }
}
