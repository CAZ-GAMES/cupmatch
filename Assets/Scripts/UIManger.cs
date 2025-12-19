using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;


public class UIManger : MonoBehaviour
{
    public TextMeshProUGUI amountCorrect;
    public TextMeshProUGUI moveCount;
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
        GameManager.Instance.moves++;
        MoveCountUpdate();

        for (int i = 0; i < inside.Count; i++)
        {
            // compare material color to compare item in list is in correct order
            if (inside[i].GetComponent<Renderer>().sharedMaterial == outside[i].GetComponent<Renderer>().sharedMaterial)
            {
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
            StartCoroutine(this.gameObject.AddComponent<ObjectHandler>().RotateToReveal(0.5f));
            checkButton.gameObject.SetActive(false);
            print("You won");
            
            // RotateToReveal();
        }
    }

    public void MoveCountUpdate()
    {
        // update moveCount when checking list
        this.moveCount.text = "Moves: " + GameManager.Instance.moves;
    }
}
