using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;
    private void OnTriggerEnter2D(Collider2D other) {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Near: "+ interactable.name);
            if(!interactable.interactsOnButtonPress)
            {
                interactable.Interact();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable == null)
        return;
        
        if(interactable == currentInteractable)
        {
            Debug.Log(interactable.name + " Exited!");
            currentInteractable = null;
        }
    }
    public void TryInteract()
    {
        if(currentInteractable == null)
        return;

        if(!currentInteractable.interactsOnButtonPress)
        return;

        currentInteractable.Interact();
    }
    
}
