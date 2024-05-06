using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AIAgentGroup : ScriptableObject
{
    public List<AiEmeny> agents;

    public void Add(AiEmeny agent)
    {
        if(agents == null)
        {
            agents = new List<AiEmeny>();
        }

        agents.Add(agent);
    }

    public void Remove(AiEmeny agent)
    {
        agents.Remove(agent);
    }
}
