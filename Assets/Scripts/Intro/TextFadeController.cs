using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TextFadeController : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public Button nextButton;

    public TextContent[] textSets;
    private int currentSetIndex = 0;
    private bool isLastSet = false;  // Flag to check if the last set is being displayed

    private void Start()
    {
        // Assign the button click listener
        nextButton.onClick.AddListener(OnNextButtonPressed);
        //text2.GetComponent<Button>().onClick.AddListener(OnNextButtonPressed);

        // Start the fade in process for the first set of texts
        StartCoroutine(FadeInTexts());
    }

    private IEnumerator FadeInTexts()
    {
        text1.text = textSets[currentSetIndex].text1;
        text2.text = textSets[currentSetIndex].text2;

        text1.alpha = 0;
        text2.alpha = 0;

        yield return FadeTextIn(text1);
        yield return FadeTextIn(text2);
    }

    private IEnumerator FadeTextIn(TextMeshProUGUI text)
    {
        while (text.alpha < 1.0f)
        {
            text.alpha += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeTextOut(TextMeshProUGUI text)
    {
        while (text.alpha > 0.0f)
        {
            text.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    public void OnNextButtonPressed()
    {
        if (!isLastSet)  // Only proceed if not on the last set
        {
            StartCoroutine(SwitchTextSets());
        }
        else
        {
            StartCoroutine(FadeOutAndTransition());
        }
    }

    private IEnumerator SwitchTextSets()
    {
        yield return FadeTextOut(text1);
        yield return FadeTextOut(text2);

        currentSetIndex++;

        if (currentSetIndex < textSets.Length - 1)
        {
            StartCoroutine(FadeInTexts());
        }
        else
        {
            isLastSet = true;  // Set flag when the last set is reached
            StartCoroutine(FadeInTexts());
        }
    }

    private IEnumerator FadeOutAndTransition()
    {
        yield return FadeTextOut(text1);
        yield return FadeTextOut(text2);
        GoToNextScene();
    }

    private void GoToNextScene()
    {
        // Implement your scene transition logic here
        SceneManager.LoadScene("Game");
    }
}
