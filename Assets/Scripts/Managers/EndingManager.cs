using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;
    [SerializeField] private GameObject EndingPanel;

    private void Awake() {
        Instance = this;
    }
    
    public void ShowEnding()
    {
        EndingPanel.SetActive(true);
    }
}
