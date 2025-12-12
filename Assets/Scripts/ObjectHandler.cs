using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHandler : MonoBehaviour, IPointerClickHandler
{
    readonly List<(float x,float y)> pos = new();

    // Fisher-Yates shuffle algorithm
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.clickedOn.Count == 0)
        {
            GameManager.Instance.clickedOn.Add(this.gameObject);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f);
        }
        else
        {
            // check to see if item already exists in list
            if (GameManager.Instance.clickedOn.Contains(this.gameObject))
            {
                // remove item from list to be swapped and reset position
                GameManager.Instance.clickedOn.Remove(this.gameObject);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f);
            }
            // doesn't exist in list so push to list and swap
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f);
                GameManager.Instance.clickedOn.Add(this.gameObject);
                Swap();
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shuffle(GameManager.Instance. insideCups);
        pos.Add((-3f,-2.5f));
        pos.Add((-1f,-2.5f));
        pos.Add((1f,-2.5f));
        pos.Add((3f,-2.5f));
        ScrambleSetInsideCups();
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

        public void Swap()
    {
        // use this as midpoint to create pivot point to revolve objects
        var objDistance = (GameManager.Instance.clickedOn[0].transform.position + GameManager.Instance.clickedOn[1].transform.position) / 2;
        StartCoroutine(RotateTimed(0.5f, GameManager.Instance.clickedOn[0], objDistance));
        StartCoroutine(RotateTimed(.5f, GameManager.Instance.clickedOn[1], objDistance));
        SwapListPos();
        GameManager.Instance.clickedOn.Clear();
    }

    IEnumerator RotateTimed(float duration, GameObject clicked, Vector3 objDistance)
    {
        float timer = 0f;
        float startAngle = 0f;
        float currentAngle;
        while (timer < duration)
        {
            // clicked.transform.RotateAround(objDistance, Vector3.up, 1440 * Time.deltaTime);
            timer += Time.deltaTime;

            // Lerp rotation progress
            currentAngle = Mathf.Lerp(startAngle, 180f, timer / duration);

            // Rotate difference since last frame
            clicked.transform.RotateAround(objDistance, Vector3.up, currentAngle - startAngle);

            startAngle = currentAngle;

            yield return null;
        }
        // set cups back down
        clicked.transform.position = new Vector3(clicked.transform.position.x, clicked.transform.position.y - 0.25f);
    }

    public void SwapListPos()
    {
        // find index of items that are clicked on to be swapped
        int indexA = GameManager.Instance.insideCups.FindIndex(c => c.name == GameManager.Instance.clickedOn[0].name);
        int indexB = GameManager.Instance.insideCups.FindIndex(c => c.name == GameManager.Instance.clickedOn[1].name);

        // swap items in the list to then be compared
        (GameManager.Instance.insideCups[indexB], GameManager.Instance.insideCups[indexA]) = (GameManager.Instance.insideCups[indexA], GameManager.Instance.insideCups[indexB]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
