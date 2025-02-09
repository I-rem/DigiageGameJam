using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppearOnTrigger : MonoBehaviour
{
    public GameObject thing;
    public int type = 0;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        thing.SetActive(true);
        if (collider.tag == "Player" && type == 1)
            FindObjectOfType<CharacterSwitcher>().Die();
        if (collider.tag == "Player" && type == 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
