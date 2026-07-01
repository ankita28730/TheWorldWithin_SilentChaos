using UnityEngine;

public class GroundingManager : MonoBehaviour
{
    public static GroundingManager Instance;
    public static GroundingStage SavedStage = GroundingStage.See;
    public static int SavedCount = 0;
    public static int SavedRequiredCount = 5;
    public GroundingStage currentStage = GroundingStage.See;
    public int currentCount = 0;
    public int requiredCount = 5;
    [SerializeField] private ObjectiveUI objectiveUI;
    private void Start() {
        currentStage = SavedStage;
        currentCount = SavedCount;
        requiredCount = SavedRequiredCount;

        UpdateObjective();
    }
    private void UpdateObjective()
    {
        switch(currentStage)
        {
            case GroundingStage.See:
            if(objectiveUI != null)
            objectiveUI.UpdateObjective($"Notice 5 things you can see.\n{currentCount}/{requiredCount}");
            break;

            case GroundingStage.Touch:
            if(objectiveUI != null)
            objectiveUI.UpdateObjective($"Touch 4 things around you.\n{currentCount}/{requiredCount}");
            break;

            case GroundingStage.Hear:
            if(objectiveUI != null)
            objectiveUI.UpdateObjective($"listen for 3 sounds.\n{currentCount}/{requiredCount}");
            break;

            case GroundingStage.Smell:
            if(objectiveUI != null)
            objectiveUI.UpdateObjective($"Notice 2 scents nearby.\n{currentCount}/{requiredCount}");
            break;

            case GroundingStage.Taste:
            if(objectiveUI != null)
            objectiveUI.UpdateObjective($"Focus on 1 taste.\n{currentCount}/{requiredCount}");
            break;

            case GroundingStage.Complete:
            if(objectiveUI != null)
            objectiveUI.UpdateObjective("Grounding quest is completed!");
            break;
        }
    }

    public void ShowInteractionMessage(string message)
    {
        if(objectiveUI != null)
        {
            objectiveUI.ShowInteractionMessage(message);
        }
    }

    public void SetObjectiveUI(ObjectiveUI ui)
    {
        objectiveUI = ui;
        UpdateObjective();
    }
    private void Awake() {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void RegisterSense(SenseType sense)
    {
        // Ignore Wrong Interactions
        if((SenseType)currentStage != sense)
        return;

        currentCount++;
        SavedCount = currentCount;
        Debug.Log($"{currentStage}: {currentCount}/{requiredCount}");
        UpdateObjective();

        if(currentCount >= requiredCount)
        {
            AdvanceStage();
        }
    }

    void AdvanceStage()
    {
        currentCount = 0;
        switch(currentStage)
        {
            case GroundingStage.See:
            currentStage = GroundingStage.Touch;
            requiredCount = 4;
            if(GameData.currentAtmosphere == AtmosphereState.Collapsed)
            {
                AtmosphereManager.Instance.SetAtmosphere(AtmosphereState.Panicked);
            }
            else
            {
                AtmosphereManager.Instance.SetAtmosphere(AtmosphereState.Uneased);
            }
            GroundingManager.Instance.ShowInteractionMessage("Good! Now focus on 4 things you can touch..");
            break;

            case GroundingStage.Touch:
            currentStage = GroundingStage.Hear;
            requiredCount = 3;
            if(GameData.currentAtmosphere == AtmosphereState.Panicked)
            {
                AtmosphereManager.Instance.SetAtmosphere(AtmosphereState.Uneased);
            }
            GroundingManager.Instance.ShowInteractionMessage("Bravo! Now focus on 3 things you can hear..");
            break;

            case GroundingStage.Hear:
            currentStage = GroundingStage.Smell;
            requiredCount = 2;
            AtmosphereManager.Instance.SetAtmosphere(AtmosphereState.Calm);
            GroundingManager.Instance.ShowInteractionMessage("Amazing! Now focus on 2 scents you can smell..");
            break;

            case GroundingStage.Smell:
            currentStage = GroundingStage.Taste;
            requiredCount = 1;
            AtmosphereManager.Instance.SetAtmosphere(AtmosphereState.Calm);
            GroundingManager.Instance.ShowInteractionMessage("Excellent! Now focus on 1 things you can taste..");
            break;

            case GroundingStage.Taste:
            currentStage = GroundingStage.Complete;
            GroundingManager.Instance.ShowInteractionMessage("You feel more grounded...\nThe city seems different, you can explore and collect your watch now.");
            break;

        }

        Debug.Log($"Current stage: {currentStage}");
        UpdateObjective();

        SavedStage = currentStage;
        SavedCount = currentCount;
        SavedRequiredCount = requiredCount;
    }
}
