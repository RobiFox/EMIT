using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    public TMPro.TextMeshProUGUI dialogText;
    public const float StayTime = 3;
    private const float TextAppearTime = 0.125f;
    private const float SentenceEnd = 1;

    public void ShowDialog(String text) {
        dialogText.text = "";
        string[] words = text.ToUpper().Split(' ');
        float wait = 0;
        for(int i = 0; i < words.Length; i++) {
            Debug.Log("Loop: " + words[i]);
            Debug.Log("Wait: " + wait);
            StartCoroutine(AddText(words[i], wait));
            wait += TextAppearTime;
            if (words[i].EndsWith("?") || words[i].EndsWith(".") || words[i].EndsWith("!")) {
                wait += SentenceEnd;
            }
        }

        //StartCoroutine(ClearText(words.Length + wait));
    }

    private IEnumerator AddText(String add, float wait) {
        yield return new WaitForSeconds(wait);
        dialogText.text += " " + add;
    }

    private IEnumerator ClearText(float num) {
        yield return new WaitForSeconds(num + StayTime);
        dialogText.text = "";
    }
}
