// Author   : Litsch

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ran.utilities.attribute
{
    /// <summary>
    /// Show a field in the inspector if the condition is met.
    /// </summary>
    public class ShowIFAttribute : PropertyAttribute
    {
        public string ConditionalFieldName { get; private set; }

        public ShowIFAttribute(string conditionalFieldName)
        {
            ConditionalFieldName = conditionalFieldName;
        }
    }

    [CustomPropertyDrawer(typeof(ShowIFAttribute))]
    public class ShowIFDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIFAttribute showIf = attribute as ShowIFAttribute;
            bool enabled = GetConditionalField(property, showIf.ConditionalFieldName);

            return enabled ? EditorGUI.GetPropertyHeight(property, label) : 0;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIFAttribute showIf = attribute as ShowIFAttribute;
            bool enabled = GetConditionalField(property, showIf.ConditionalFieldName);

            if (enabled) EditorGUI.PropertyField(position, property, label, true);
        }

        private bool GetConditionalField(SerializedProperty property, string fieldName)
        {
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(fieldName);
            if (sourcePropertyValue == null)
            {
                Debug.LogError($"Property {fieldName} not found");
                return false;
            }

            return sourcePropertyValue.boolValue;
        }
    }
}
#endif
