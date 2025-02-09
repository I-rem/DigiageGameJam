using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnTrigger : MonoBehaviour
{
    public GameObject thing;
    public int type = 0;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        thing.SetActive(true);
        if (collider.tag == "Player" && type == 1)
            FindObjectOfType<CharacterSwitcher>().Die();
    }
}
