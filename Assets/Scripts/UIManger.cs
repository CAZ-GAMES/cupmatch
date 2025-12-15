using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManger : MonoBehaviour
{
    public TextMeshProUGUI amountCorrect;
    public Button checkButton;

    int fullCorrect = 4;

    void Awake()
    {
        GameManager.OnGameStateChange += Won;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChange -= Won;
    }
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
            // compare material color to compare item in list is in correct order
            if (inside[i].GetComponent<Renderer>().sharedMaterial == outside[i].GetComponent<Renderer>().sharedMaterial)
            {
                print(inside[i].GetComponent<Renderer>().sharedMaterial);
                correct++;
            }
        }
        UpdateCorrect(correct);
        if (correct == fullCorrect)
        {
            GameManager.Instance.UpdateGameState(GameState.Win);
        }
    }

    public void UpdateCorrect(int correct)
    {
        amountCorrect.GetComponent<TextMeshProUGUI>().enabled = true;
        amountCorrect.text = "Amount Correct: " + correct;
    }

    void Won(GameState state)
    {
        if (state == GameState.Win)
        {
            checkButton.gameObject.SetActive(false);
            print("You won");
        }
    }
}
