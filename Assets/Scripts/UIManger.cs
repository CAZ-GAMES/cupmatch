using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManger : MonoBehaviour
{
    public TextMeshProUGUI amountCorrect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckLists()
    {
        List<GameObject> inside = GameManager.Instance.insideCups;
        List<GameObject> outside = GameManager.Instance.outsideCups;
        int correct = 0;

        for (int i = 0; i < inside.Count; i++)
        {
            if (inside[i].name == outside[i].name)
            {
                correct++;
            }
        }
        UpdateCorrect(correct);
    }

    public void UpdateCorrect(int correct)
    {
        amountCorrect.GetComponent<TextMeshProUGUI>().enabled = true;
        amountCorrect.text = "Amount Correct: " + correct;
    }
}
