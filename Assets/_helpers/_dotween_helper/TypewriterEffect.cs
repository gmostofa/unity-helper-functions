using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    [TextArea]
    public string fullText;
    public float typingSpeed = 0.05f;

    private bool isTyping = true; // Flag to control the loop


    public void StartTypingLoop(string text)
    {
        isTyping = true;
        //textMeshPro.text = text;
        StartCoroutine(TypeTextLoop());
    }

    public void StartTypingOnce(string text)
    {
        //textMeshPro.text = text;
        StartCoroutine(TypeText());
    }

    public void StopTyping()
    {
        isTyping = false;
    }

    private IEnumerator TypeTextLoop()
    {
        while (isTyping)
        {
            yield return StartCoroutine(TypeText());
            yield return new WaitForSeconds(1f); // Pause before restarting the text
        }
    }
  
    

    private IEnumerator TypeText()
    {
        textMeshPro.text = ""; // Clear text
        foreach (char letter in fullText)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void Activate()
    {
       gameObject.SetActive(true);
    }
}