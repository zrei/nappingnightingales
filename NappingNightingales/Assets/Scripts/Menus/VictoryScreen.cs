using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    private TMP_Text deathText;

    void Start()
    {
        deathText = GetComponent<TMP_Text>();
        int deaths = EventManager.GetDeaths();
        deathText.text = "Deaths: " + deaths;
    }
}