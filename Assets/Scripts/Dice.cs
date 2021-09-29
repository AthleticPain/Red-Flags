using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] Sprite[] DiceSpriteArray;
    [SerializeField] bool useCustomMovement = false;
    [SerializeField] int customMovementNumber = 1;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = DiceSpriteArray[5];
    }

    public void RollDie()
    {
        FindObjectOfType<MainCanvas>().DisableRollButton();
        StartCoroutine(GenerateRollNumber());
    }

    IEnumerator GenerateRollNumber()
    {
        if (useCustomMovement == true)
        {
            FindObjectOfType<BoardController>().FindDestinationWaypoint(customMovementNumber-1);
        }
        else
        {
            int randomSide = 0;
            for (int i = 0; i < 18; i++)
            {
                randomSide = Random.Range(0, 6);
                SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = DiceSpriteArray[randomSide];
                yield return new WaitForSeconds(0.05f);
            }
            FindObjectOfType<BoardController>().FindDestinationWaypoint(randomSide);
        }
    }
}
