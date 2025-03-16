using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractionType { ChangeColor, Destroy, SpawnObject }
    public InteractionType interactionType;
    public GameObject objectToSpawn;

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.ChangeColor:
                ChangeColor();
                break;
            case InteractionType.Destroy:
                Destroy(gameObject);
                break;
            case InteractionType.SpawnObject:
                SpawnNewObject();
                break;
        }
    }

    void ChangeColor()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Random.ColorHSV();
        }
    }

    void SpawnNewObject()
    {
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
}
