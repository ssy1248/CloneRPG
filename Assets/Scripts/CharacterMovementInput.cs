using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    [SerializeField]
    MouseInput MouseInput;
    CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    public void MoveInput()
    {
        characterMovement.SetDestination(MouseInput.rayToWorldIntersectionPoint);
    }
}
