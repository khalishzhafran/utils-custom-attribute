// Author   : Litsch

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ran.utilities.attribute
{
    /// <summary>
    /// Disable a field in the inspector if the condition is met.
    /// </summary>
    public class DisableIFAttribute : PropertyAttribute
    {
        public string ConditionalFieldName { get; private set; }

        public DisableIFAttribute(string conditionalFieldName)
        {
            ConditionalFieldName = conditionalFieldName;
        }
    }

    [CustomPropertyDrawer(typeof(DisableIFAttribute))]
    public class DisableIFDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DisableIFAttribute disableIF = attribute as DisableIFAttribute;

            bool enabled = GetConditionalField(property, disableIF.ConditionalFieldName);
            bool previousState = GUI.enabled;
            GUI.enabled = !enabled;

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
