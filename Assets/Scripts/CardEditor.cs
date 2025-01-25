using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Card card = (Card)target;

        // Display the Type field
        card.Type = (CardType)EditorGUILayout.EnumPopup("Card Type", card.Type);

        // Conditionally display the Color field
        if (card.Type != CardType.Wild && card.Type != CardType.Wild4)
        {
            card.Color = EditorGUILayout.TextField("Color", card.Color);
        }

        // Conditionally display the Value field
        if (card.Type == CardType.Number)
        {
            card.Number = EditorGUILayout.IntField("Value", card.Number);
        }

        // Save changes to the object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(card);
        }
    }
}
