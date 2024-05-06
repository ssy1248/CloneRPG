using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIContoroller : MonoBehaviour
{
    [SerializeField]
    AIAgentGroup agentGroup;
    [SerializeField]
    GameObject targetToAttack;

    private void Update()
    {
        if(agentGroup.agents == null)
        {
            return;
        }

        for(int i = 0; i < agentGroup.agents.Count; i++)
        {
            agentGroup.agents[i].UpdateAgent(targetToAttack);
        }
    }
}
