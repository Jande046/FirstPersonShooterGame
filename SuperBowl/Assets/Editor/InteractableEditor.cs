using UnityEditor;


[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnly Interact can only use Unityevents", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>()  == null)
            {
                interactable.useEvents = true;
                interactable.GetComponent<InteractionEvent>();
            }
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
