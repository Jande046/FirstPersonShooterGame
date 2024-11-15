using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template method pattern
public abstract class Interactable : MonoBehaviour
{
    //add or remove interactionEvents component to this game object
    public bool useEvents;
    [SerializeField]
    public string promptMessage;

    public void BaseInteract()
    {
        if(useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {
        //template to be overwritten by our subclasses
    }
}
