using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State;

    public static event Action<GameState> OnGameStateChange;
    public List<GameObject> insideCups;
    public List<GameObject> outsideCups;

    public Transform insideParent;

    public List<GameObject> clickedOn = new();

        readonly List<(float x,float y)> pos = new();

    // Fisher-Yates shuffle algorithm
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    void Start()
    {
        Shuffle(insideCups);
        pos.Add((-3f,-2.5f));
        pos.Add((-1f,-2.5f));
        pos.Add((1f,-2.5f));
        pos.Add((3f,-2.5f));
        ScrambleSetInsideCups();
        UpdateGameState(GameState.Trying);
    }

    void Update()
    {

    }

    void ScrambleSetInsideCups()
{
    int i = 0;
    foreach (GameObject x in GameManager.Instance.insideCups)
    {
        Instantiate(x, new Vector3(pos[i].x,pos[i].y), Quaternion.identity, GameManager.Instance.insideParent);
        i++;
    }
}

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.Trying:
                break;
            case GameState.Win:
                break;
        }
    
    OnGameStateChange?.Invoke(newState);

    }

}


public enum GameState
{
    Trying,
    Win,
}