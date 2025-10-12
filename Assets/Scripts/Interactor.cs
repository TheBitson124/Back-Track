using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;

    public float range;

    public KeyCode InteractKey = KeyCode.E;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    public bool hitSomething;
    void Update()
    {
        hitSomething = false;
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactionText.text = interactObj.getDescription();
                hitSomething = true;
                if (Input.GetKeyDown(InteractKey))
                {
                    print("hit something");
                    interactObj.Interact();

                }
            }
        }
        interactionUI.SetActive(hitSomething);
    }
}

