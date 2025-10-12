using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string description;
    public string getDescription()
    {
        return description;
    }

    public void Interact()
    {
        print("Test interaction");
    }
}
