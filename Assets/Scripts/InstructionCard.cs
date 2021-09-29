using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionCard : MonoBehaviour
{
    GameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        //SetScale();
        float flipSpeed = FindObjectOfType<CardController>().GetFlipSpeed();
        float currentRotationY = transform.rotation.eulerAngles.y;
        StartCoroutine(RevealCard(flipSpeed, currentRotationY));
    }

    IEnumerator RevealCard(float flipSpeed, float maxRotation)
    {
        //Debug.Log("Max rotation = " + maxRotation);
        /*while(true)
        {
            Quaternion desiredRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 1f * Time.deltaTime);
            yield return null;
        }*/
        while (transform.rotation.eulerAngles.y <= maxRotation)
        {
            //Debug.Log("Current rotation = " + transform.rotation.eulerAngles.y);
            transform.Rotate(new Vector3(0, -flipSpeed * Time.deltaTime, 0));
            yield return null;
        }
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetColors(Color cardColor, Color textColor)
    {
        //Debug.Log("Set color method called");
        GetComponent<SpriteRenderer>().color = cardColor;
        CardText cardText = GetComponentInChildren<CardText>();
        cardText.SetTextColor(textColor);
    }

    public void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }
}
