using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    public Image storyImage;
    public TextMeshProUGUI subtitleText;
    //public AudioSource narrationAudio;

    public GameObject[] images; // Assign images in Inspector
    //public AudioClip[] narrationClips; // Assign audio clips in Inspector
    public string[] subtitles; // Assign subtitle text in Inspector
    public float displayTime = 5f; // Time per slide

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(PlayStory());
    }

    IEnumerator PlayStory()
    {
        while (currentIndex < images.Length)
        {
            //storyImage.sprite = images[currentIndex];
            subtitleText.text = subtitles[currentIndex];
            if (currentIndex != 0)
                images[currentIndex - 1].SetActive(false);
            images[currentIndex].SetActive(true);
            //narrationAudio.clip = narrationClips[currentIndex];
            //narrationAudio.Play();
            if (currentIndex == 2 || currentIndex == 4 || currentIndex == 5)
                yield return new WaitForSeconds(7);
            else
                yield return new WaitForSeconds(displayTime);

            currentIndex++;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
