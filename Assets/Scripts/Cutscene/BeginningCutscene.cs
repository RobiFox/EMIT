using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogManager))]
public class BeginningCutscene : MonoBehaviour {
    private DialogManager _dialogManager;
    void Start() {
        _dialogManager = GetComponent<DialogManager>();
        StartCoroutine(Dialog());
    }

    IEnumerator Dialog() {
        yield return new WaitForSeconds(15f);
        _dialogManager.ShowDialog("If you had the power...");
        yield return new WaitForSeconds(3);
        _dialogManager.ShowDialog("... Would you rewind time?");
        // TODO TUTORIAL
    }
}
