using UnityEngine;
using UnityEngine.Events;

public class CharacterDefeatHandler : MonoBehaviour
{
    public UnityEvent onDefeated;
    public UnityEvent onRespawned;

    public void Deafeated()
    {
        onDefeated?.Invoke();    
    }

    internal void Respawn()
    {
        onRespawned?.Invoke();
    }

}
