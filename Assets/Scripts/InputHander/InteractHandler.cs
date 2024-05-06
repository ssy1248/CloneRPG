using CharacterCommand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour, ICommandHandle
{
    CharacterMovement characterMovement;
    Character character;

    [SerializeField]
    float interactRange = 0.5f;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        character = GetComponent<Character>();
    }

    public void ProcessCommand(Command command)
    {
        float distance = Vector3.Distance(transform.position, command.target.transform.position);

        if (distance < interactRange)
        {
            command.target.GetComponent<InteractableObject>().Interact(character);
            characterMovement.Stop();
            command.isComplete = true;
        }
        else
        {
            characterMovement.SetDestination(command.target.transform.position);
        }
    }
}
