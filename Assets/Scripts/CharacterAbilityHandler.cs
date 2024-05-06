using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AbilityContainer
{
    public Ability ability;
    public float currentCooldown;

    public float CooldownNormalized 
    {
        get { return 1f - currentCooldown / ability.cooldown; } 
    }

    public AbilityContainer(Ability ability)
    {
        this.ability = ability; 
    }

    internal void Cooldown()
    {
        currentCooldown = ability.cooldown;
    }

    public void ReduceCooldown(float deltaTime)
    {
        if(currentCooldown > 0f)
        {
            currentCooldown -= deltaTime;
        }
    }
}

public class CharacterAbilityHandler : MonoBehaviour
{
    [SerializeField]
    Ability startingAbility;

    List<AbilityContainer> abilities;

    public UnityEvent<AbilityContainer, int> onAbilityChange;
    public UnityEvent<float, int> onCooldownUpdate;

    private void Start()
    {
        AddAbility(startingAbility);
    }

    private void AddAbility(Ability abilityToAdd)
    {
        if(abilities == null)
        {
            abilities = new List<AbilityContainer>();
        }

        AbilityContainer abilityContainer = new AbilityContainer(abilityToAdd);
        abilities.Add(abilityContainer);
        onAbilityChange?.Invoke(abilityContainer, abilities.Count - 1);
    }

    private void Update()
    {
        ProcessCooldown();
    }

    private void ProcessCooldown()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].ReduceCooldown(Time.deltaTime);
            onCooldownUpdate?.Invoke(abilities[i].CooldownNormalized, i);
        }
    }

    public void ActivateAbility(AbilityContainer ability)
    {
        if(ability.currentCooldown > 0f)
        {
            return;
        }

        Debug.Log("Activate : " + ability.ability.name);
        ability.Cooldown();
    }

    public void ActivateAbility(int abilityID)
    {
        if(abilityID >= abilities.Count)
        {
            return;
        }
        if (abilities[abilityID] == null)
        {
            return;
        }
        AbilityContainer abilityContainer = abilities[abilityID];
        ActivateAbility(abilityContainer);
    }
}
