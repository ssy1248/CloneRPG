using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionInteractableObject : MonoBehaviour
{
    [SerializeField]
    string sceneName;

    void Start()
    {
        GetComponent<InteractableObject>().Subscribe(Transition);
    }

    public void Transition(Character character)
    {
        GameSceneManager.instance.StartTransition(sceneName);
    }
}
