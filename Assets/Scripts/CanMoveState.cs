using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveState : MonoBehaviour
{
    public bool isAttacking;

    public bool Check()
    {
        return (isAttacking) == false;
    }
}
