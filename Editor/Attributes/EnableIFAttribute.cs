// Author   : Litsch

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ran.utilities.attribute
{
    /// <summary>
    /// Enable a field in the inspector if the condition is met.
    /// </summary>
    public class EnableIFAttribute : PropertyAttribute
    {
        public string ConditionalFieldName { get; private set; }

        public EnableIFAttribute(string conditionalFieldName)
        {
            ConditionalFieldName = conditionalFieldName;
        }
    }

    [CustomPropertyDrawer(typeof(EnableIFAttribute))]
    public class EnableIFDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnableIFAttribute enableIF = attribute as EnableIFAttribute;

            bool enabled = GetConditionalField(property, enableIF.ConditionalFieldName);
            bool previousState = GUI.enabled;
            GUI.enabled = enabled;

            EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = previousState;
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
