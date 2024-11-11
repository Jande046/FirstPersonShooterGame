using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template method pattern
public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //template to be overwritten by our subclasses
    }
}
