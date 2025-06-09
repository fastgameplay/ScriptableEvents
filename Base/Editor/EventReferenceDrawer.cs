using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EventReference))]
[CustomPropertyDrawer(typeof(EventReference<>))]
[CustomPropertyDrawer(typeof(EventReference<,>))]
public class EventReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Indent label
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Get properties
        SerializedProperty typeProp = property.FindPropertyRelative("_type");
        SerializedProperty externalEventProp = property.FindPropertyRelative("_externalEvent");

        // Draw _type field
        EditorGUI.PropertyField(new Rect(position.x, position.y, 80, position.height), typeProp, GUIContent.none);

        // Draw corresponding field based on _type
        EventType eventType = (EventType)typeProp.enumValueIndex;
        switch (eventType)
        {
            case EventType.Internal:
                // Draw internal event field
                // EditorGUI.PropertyField(new Rect(position.x + 80, position.y, position.width - 80, position.height), property.FindPropertyRelative("_internalEvent"), GUIContent.none);
                break;
            case EventType.External:
                // Draw external event field
                EditorGUI.PropertyField(new Rect(position.x + 80, position.y, position.width - 80, position.height), externalEventProp, GUIContent.none);
                break;
        }

        // Reset indent level
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
