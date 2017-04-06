using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour {
    [SerializeField]
    private Transform restPostion, activePosition, objectToMove;
    [SerializeField]
    private float moveRate;
    [SerializeField]
    private MenuInteraction twinInteraction;
    private IEnumerator coroutineOpen, coroutineClose;
    public bool isStartGame;

    void Start()
    {
        if (restPostion && activePosition && objectToMove)
        {
            objectToMove.position = restPostion.position;
            objectToMove.rotation = restPostion.rotation;
        }
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        if (restPostion && activePosition && objectToMove)
        {
            coroutineOpen = OpenEffect();
            if (coroutineClose != null)
            {
                StopCoroutine(coroutineClose);
            }
            StartCoroutine(coroutineOpen);
        }
        if (twinInteraction)
        {
            twinInteraction.Open();
        }
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        if (restPostion && activePosition && objectToMove)
        {
            coroutineClose = CloseEffect();
            if (coroutineOpen != null)
            {
                StopCoroutine(coroutineOpen);
            }
            StartCoroutine(coroutineClose);
        }
        if (twinInteraction)
        {
            twinInteraction.Close();
        }
    }
    void OnMouseDown()
    {
        if (isStartGame)
        {
            StartGame();
        }
        else
        {
            QuitGame();
        }
    }
    private IEnumerator OpenEffect()
    {
        
            while (objectToMove.rotation != activePosition.rotation || objectToMove.position != activePosition.position)
            {
            objectToMove.position = Vector2.Lerp(objectToMove.position, activePosition.position, moveRate);
                objectToMove.rotation = Quaternion.Lerp(objectToMove.rotation, activePosition.rotation, moveRate);
                yield return null;
            }
        


    }

    private IEnumerator CloseEffect()
    {

            while (objectToMove.rotation != restPostion.rotation || objectToMove.position != restPostion.position)
            {

                objectToMove.position = Vector2.Lerp(objectToMove.position, restPostion.position, moveRate);
                objectToMove.rotation = Quaternion.Lerp(objectToMove.rotation, restPostion.rotation, moveRate);
                yield return null;
            }

 
    }
    
    public void Open()
    {
        coroutineOpen = OpenEffect();
        if (coroutineClose != null)
        {
            StopCoroutine(coroutineClose);
        }
        StartCoroutine(coroutineOpen);
    }
    public void Close()
    {
        coroutineClose = CloseEffect();
        if (coroutineOpen != null)
        {
            StopCoroutine(coroutineOpen);
        }
        StartCoroutine(coroutineClose);
    }
    void QuitGame()
    {
        Application.Quit();
    }
    void StartGame()
    {
        Application.LoadLevel("Main");
    }
}
