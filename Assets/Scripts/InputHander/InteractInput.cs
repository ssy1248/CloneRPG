using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractInput : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textOnScreen;

    [SerializeField]
    UIPoolBar hpBar;

    GameObject currentHoverOverObjcet;

    [HideInInspector]
    public InteractableObject hoveringOverObject;
    [HideInInspector]
    public IDamageable attackTarget;

    InteractHandler interactHandler;

    Vector2 mousePositon;

    private void Awake()
    {
        interactHandler = GetComponent<InteractHandler>();
    }

    private void Update()
    {
        CheckInteractObject();
    }

    public void MousePositionInput(InputAction.CallbackContext callbackContext)
    {
        mousePositon = callbackContext.ReadValue<Vector2>();
    }

    private void CheckInteractObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePositon);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if(currentHoverOverObjcet != hit.transform.gameObject) 
            {
                currentHoverOverObjcet = hit.transform.gameObject;
                UpdateInteractableObject(hit);
            }
        }
    }

    private void UpdateInteractableObject(RaycastHit hit)
    {
        InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            hoveringOverObject = interactableObject;
            attackTarget = interactableObject.GetComponent<IDamageable>();
            textOnScreen.text = hoveringOverObject.objectName;
        }
        else
        {
            attackTarget = null;
            hoveringOverObject = null;
            textOnScreen.text = "";
        }
        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        if(attackTarget != null)
        {
            hpBar.Show(attackTarget.GetLifePool());
        }
        else
        {
            hpBar.Clear();
        }
    }

    internal bool InteractCheck()
    {
        return hoveringOverObject != null;   
    }

}
