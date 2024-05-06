using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    InteractInput InteractInput;
    AttackHandler AttackHandler;

    private void Awake()
    {
        InteractInput = GetComponent<InteractInput>();
        AttackHandler = GetComponent<AttackHandler>();
    }

    public bool AttackCooldownCheck()
    {
        return AttackHandler.CheckAttack();
    }

    public bool AttackTargetCheck()
    {
        return InteractInput.attackTarget != null;
    }
}
