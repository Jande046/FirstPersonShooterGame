using UnityEditor;


[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable))
        {

        }
        else
        {

         base.OnInspectorGUI();
            if(interactable.useEvents)
        {
            if(interactable.GetComponent<InteractionEvent>() == null)
                interactable.gameObject.AddComponent<InteractionEvent>();
        }
        else
        {
            if(interactable.GetComponent<InteractionEvent>() != null)
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());

        }
        }
    }
}
