using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtmosphereManager : MonoBehaviour
{
    private Camera mainCamera;
    public static AtmosphereManager Instance;
    public Image atmosphereOverlay;
    public TMP_Text stateText;
    
    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        if(mainCamera == null)
        mainCamera = Camera.main;

        ApplyAtmosphere();
    }

    public void SetAtmosphere(AtmosphereState state)
    {
        GameData.currentAtmosphere = state;
        ApplyAtmosphere();
    }

    public void ApplyAtmosphere()
    {
        if(atmosphereOverlay == null || stateText == null)
        return;

        if(mainCamera == null)
        mainCamera = Camera.main;

        switch(GameData.currentAtmosphere)
        {
            case AtmosphereState.Collapsed:
            mainCamera.backgroundColor = new Color(0.15f, 0.15f, 0.18f);
            atmosphereOverlay.color = new Color(0f, 0f, 0f, 0.35f);
            stateText.text = "State: Collapsed";
            break;

            case AtmosphereState.Panicked:
            mainCamera.backgroundColor = new Color(0.25f, 0.18f, 0.18f);
            atmosphereOverlay.color = new Color(0.4f, 0f, 0f, 0.25f);
            stateText.text = "State: Panicked";
            break;

            case AtmosphereState.Uneased:
            mainCamera.backgroundColor = new Color(0.35f, 0.35f, 0.35f);
            atmosphereOverlay.color = new Color(0.2f, 0.2f, 0.2f, 0.15f);
            stateText.text = "State: Uneased";
            break;

            case AtmosphereState.Calm:
            mainCamera.backgroundColor = new Color(0.5f, 0.7f, 0.8f);
            atmosphereOverlay.color = new Color(0.4f, 0.8f, 1f, 0.05f);
            stateText.text = "State: Calm";
            break;
        }
    }
}
