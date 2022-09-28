using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class death_script : MonoBehaviour
{
    bool gameFinished = false;
    public Text outputTextField;

    void OnTriggerEnter(Collider col) {
        if (!gameFinished) {
            outputTextField.text = col.gameObject.name + "hat verloren";
            print(col.gameObject.name + "hat verloren");
        }
        gameFinished = true;
    }
}
