using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public GameObject InteractionText;
    private bool interactable = false;

    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            // To Do: Probably a dialogue box
            if (gameObject.CompareTag("Poseidon"))
                FindObjectOfType<CharacterSwitcher>().UnlockCharacter("Poseidon");
            else
                FindObjectOfType<CharacterSwitcher>().UnlockCharacter("Demeter");
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter");
        Debug.Log(other.tag);

        if (other.CompareTag("Player"))
            InteractionText.SetActive(true);
        interactable = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit");
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
            InteractionText.SetActive(false);
        interactable = false;
    }
}
