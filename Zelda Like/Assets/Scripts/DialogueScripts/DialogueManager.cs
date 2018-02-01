using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentencesQueue = new Queue<string>();
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image dialoguePortrait;
    public GameObject dialogueBox;
    public float letterDelay;
    public string sentence;
    
    public void StartDialogue(Dialogue _dialogue)
    {
        dialogueBox.SetActive(true);
        sentence = null;
        sentencesQueue.Clear();
        nameText.text = _dialogue.name;
        dialoguePortrait.sprite = _dialogue.portrait;


        foreach(string sentence in _dialogue.sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        sentence = sentencesQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));


    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }

    IEnumerator TypeSentence(string _sentence)
    {
        dialogueText.text = "";
        foreach(char letter in _sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
