using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [HideInInspector] public static float progress;

    private Slider progressBar;

    private void Awake()
    {
        progressBar = GetComponent<Slider>();

        progressBar.value = progress;
    }

    public void EditProgress(int amountOfDanger)
    {
        progress += progressBar.maxValue / amountOfDanger;
        //progress += progressAmount;
        progressBar.value = progress;
    }
}
