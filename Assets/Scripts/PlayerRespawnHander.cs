using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnHander : MonoBehaviour
{
    Vector3 respawnPoint;
    string respawnSceneName;
    CharacterDefeatHandler characterDefeatHandler;
    [SerializeField] 
    Animator animator;

    private void Awake()
    {
        characterDefeatHandler = GetComponent<CharacterDefeatHandler>();
    }

    private void Start()
    {
        respawnPoint = transform.position;
    }

    public void Respawn()
    {
        gameObject.transform.position = respawnPoint;
        characterDefeatHandler.Respawn();
        animator.Play("Idle");
        animator.SetBool("defeated", false);
    }
}
