using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var value= PlayerPrefs.GetInt("Score");
        this.GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
