using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChestInteractableObject : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    ItemDropList dropList;

    [SerializeField]
    float itemDropRange = 2f;

    bool isOpened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<InteractableObject>().Subscribe(OpenChest);
    }

    public void OpenChest(Character character)
    {
        if (isOpened) 
        {
            return;
        }

        //���ڰ� ���� �� ������ �ݶ��̴��� �����Ͽ� �߰� ��ȣ�ۿ��� ����
        GetComponent<Collider>().enabled = false;

        isOpened = true;

        animator.SetBool("Open", true);
        ItemSpawnManager.instance.SpawnItem(SelectedRandomPosition(), dropList.GetDrop(), transform);
    }

    private Vector3 SelectedRandomPosition()
    {
        Vector3 pos = transform.position;

        pos += Vector3.right * UnityEngine.Random.Range(-itemDropRange, itemDropRange);
        pos += Vector3.forward * UnityEngine.Random.Range(-itemDropRange, itemDropRange);

        return pos;
    }
}
