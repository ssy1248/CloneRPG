using CharacterCommand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEmeny : MonoBehaviour
{
    [SerializeField]
    AIAgentGroup aiGroup;
    CommandHandler commandHandler;

    private void Awake()
    {
        commandHandler = GetComponent<CommandHandler>();
    }

    [SerializeField] Character target;
    float timer = 4f;

    private void Start()
    {
        target = GameManager.Instance.playerObjcet.GetComponent<Character>();
        aiGroup.Add(this);
    }

    private void OnDestroy()
    {
        aiGroup.Remove(this);
    }

    internal void UpdateAgent(GameObject targetToAttack)
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            timer = 4f;

            commandHandler.SetCommand(new Command(CommandType.Attack, targetToAttack));
        }
    }
}
