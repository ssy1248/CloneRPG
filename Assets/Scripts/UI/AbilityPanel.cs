using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField]
    List<AbilitySlotButton> slotButtons;
    public UnityEvent<int> onAbilityActivate;

    public void ActivateAbility_1(InputAction.CallbackContext context)
    {
        if(context.started == true)
        {
            ActivateAblity(0);
        }
    }

    public void ActivateAbility_2(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            ActivateAblity(1);
        }
    }

    public void ActivateAbility_3(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            ActivateAblity(2);
        }
    }
    public void ActivateAbility_4(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            ActivateAblity(3);
        }
    }
    public void ActivateAbility_5(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            ActivateAblity(4);
        }
    }
    public void ActivateAbility_6(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            ActivateAblity(5);
        }
    }

    public void ActivateAblity(int abilitySlot)
    {
        Debug.Log("Activated ability Num" + abilitySlot.ToString());
        onAbilityActivate?.Invoke(abilitySlot);
    }

    public void ActivatePotion(int potionSlot)
    {
        Debug.Log("Activated potion Num" + potionSlot.ToString());
    }

    public void UpdateAbility(AbilityContainer ability, int abilitySlotID)
    {
        slotButtons[abilitySlotID].UpdateAbility(ability);
    }

    public void UpdateCooldown(float cooldownNormalized, int abilitySlotId)
    {
        slotButtons[abilitySlotId].UpdateCooldown(cooldownNormalized);
    }
}
