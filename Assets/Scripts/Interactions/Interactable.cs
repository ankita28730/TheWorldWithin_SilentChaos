using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    public SenseType senseType;
    public bool interactsOnButtonPress;
    public bool hasbeenCollected = false;
    public InteractionType interactionType;
    public string sceneToLoad;
    public string interactionID;
    [TextArea] public string interactionMessage;

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Grounding:
            if(hasbeenCollected)
            return;

            if((SenseType)GroundingManager.Instance.currentStage != senseType)
            {
                GroundingManager.Instance.ShowInteractionMessage($"Right now, focus on things you can {GroundingManager.Instance.currentStage.ToString().ToLower()}");
                return;
            }

            hasbeenCollected = true;
            GameData.collectedObjects.Add(interactionID);
            GroundingManager.Instance.ShowInteractionMessage(interactionMessage);
            GroundingManager.Instance.RegisterSense(senseType);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if(sr != null)
            {
                sr.color = Color.green;
            }
            transform.localScale *= 1.1f;
            Debug.Log("Interacted with "+ gameObject.name);
            break;

            case InteractionType.SceneTransition:
            SceneLoader.Instance.LoadScene(sceneToLoad);
            break;

            case InteractionType.FinalReward:
            if(GroundingManager.Instance.currentStage != GroundingStage.Complete)
            {
                GroundingManager.Instance.ShowInteractionMessage("Complete the Grounding Interactions first..");
                return;
            }

            EndingManager.Instance.ShowEnding();
            break;
        }
        
    }

    private void Start() {
        if(GameData.collectedObjects.Contains(interactionID))
        {
            hasbeenCollected = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if(sr != null)
            {
                sr.color = Color.green;
            }
            transform.localScale *= 1.1f;
        }
    }
    
}
