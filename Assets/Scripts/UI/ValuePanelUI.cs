using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValuePanelUI : MonoBehaviour
{
    [SerializeField]
    List<ValueUI> attributeValueUIElements;
    [SerializeField]
    List<ValueUI> statsValueUIElements; 
    [SerializeField]
    Character targetCharacter;

    private void Update()
    {
        UpdatePanel(targetCharacter);
    }

    public void UpdatePanel(Character character)
    {
        for(int i = 0; i < attributeValueUIElements.Count; i++)
        {
            attributeValueUIElements[i].ShowCharacterValue(character);
        }

        for(int i = 0; i < statsValueUIElements.Count; i++)
        {
            statsValueUIElements[i].ShowCharacterValue(character);
        }
    }
}
