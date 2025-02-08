using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject hades;
    public GameObject poseidon;
    public GameObject demeter;

    private GameObject[] characters;
    private int currentCharacterIndex = 0;

    void Start()
    {
        characters = new GameObject[] { hades, poseidon, demeter };

        hades.SetActive(true);
        poseidon.SetActive(false);
        demeter.SetActive(false);

        poseidon.tag = "Locked";
        demeter.tag = "Locked";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCharacter();
        }
    }

    void SwitchCharacter()
    {
        int startIndex = currentCharacterIndex;
        do
        {
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
        }
        while (characters[currentCharacterIndex].tag == "Locked" && currentCharacterIndex != startIndex);
        if (characters[currentCharacterIndex].tag != "Locked" && currentCharacterIndex != startIndex)
            FindObjectOfType<AudioManager>().Play("Whoosh");
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == currentCharacterIndex);
        }
    }

    public void UnlockCharacter(string characterName)
    {
        if (characterName == "Poseidon")
        {
            //poseidon.SetActive(true);
            poseidon.tag = "Player"; 
        }
        else if (characterName == "Demeter")
        {
            //demeter.SetActive(true);
            demeter.tag = "Player";
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
            FindObjectOfType<AudioManager>().Play("Water1");
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
            FindObjectOfType<AudioManager>().Play("Water2");
    }
}
