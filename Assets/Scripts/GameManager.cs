using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.ComponentModel.Design.Serialization;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> insideCups;
    public List<GameObject> outsideCups;

    public Transform insideParent;

    public List<GameObject> clickedOn = new();



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

    }

    void Update()
    {

    }




}
