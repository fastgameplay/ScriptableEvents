using UnityEngine;
using UnityEditor;

namespace ScriptableEvents.Editor
{
    [CustomPropertyDrawer(typeof(EventReferenceBase<,>), true)]
    public class EventReferenceDrawer : PropertyDrawer
    {
        const float k_TypeWidth = 80f;
        const float k_Space     = 4f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // draw the prefix label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // save & clear indent 
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // fetch our two serialized fields from the base class
            var typeProp     = property.FindPropertyRelative("_type");
            var externalProp = property.FindPropertyRelative("_externalEvent");

            // draw the enum popup
            var typeRect = new Rect(position.x, position.y, k_TypeWidth, position.height);
            EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

            // if it's External, draw the object‐field for the ScriptableEvent asset
            if ((EventType)typeProp.enumValueIndex == EventType.External)
            {
                var fieldX    = position.x + k_TypeWidth + k_Space;
                var fieldW    = position.width - k_TypeWidth - k_Space;
                var fieldRect = new Rect(fieldX, position.y, fieldW, position.height);
                EditorGUI.PropertyField(fieldRect, externalProp, GUIContent.none);
            }

            // restore indent
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
