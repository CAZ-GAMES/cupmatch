using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHandler : MonoBehaviour, IPointerClickHandler
{

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
        // get index of each object selected and change its position in outsideCups list
        int indexA = GameManager.Instance.outsideCups.FindIndex(c => c == GameManager.Instance.clickedOn[0]);
        int indexB = GameManager.Instance.outsideCups.FindIndex(c => c == GameManager.Instance.clickedOn[1]);

        // check to see if items exist
        if (indexA == -1 || indexB == -1)
        {
            Debug.LogError($"Object not found! indexA: {indexA}, indexB: {indexB}");
            return;
        }

        (GameManager.Instance.outsideCups[indexB], GameManager.Instance.outsideCups[indexA]) =
            (GameManager.Instance.outsideCups[indexA], GameManager.Instance.outsideCups[indexB]);
    }

    //Call to turn box and show that the player was right or in challenging rounds how many they missed
    public IEnumerator RotateToReveal(float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180, 0);
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            GameManager.Instance.boxContainer.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
