using System.Collections;
using UnityEngine;

public class HandLogic : MonoBehaviour
{
    public float handGrabTimer;
    public float minGrabTime;
    public float maxGrabTime;

    public Vector3 startPoint, endPoint;
    public float moveTime;

    public Transform playerTransform;

    private void Start()
    {
        //transform.position = startPoint;
        //HardSearchAgain();

        StopCoroutine(HardSearchMode());
    }

    private void HardSearchAgain()
    {
        handGrabTimer = Random.Range(minGrabTime, maxGrabTime);
        StopCoroutine(HardGrabber(endPoint, moveTime));
        StartCoroutine(HardGrabber(endPoint, moveTime));
    }
    
    private IEnumerator HardGrabber(Vector3 destination, float duration)
    {
        //yield return new WaitForSeconds(handGrabTimer);

        Vector3 startPos = transform.position;
        float timer = 0;

        while (timer < duration)
        {
            // Increment the timer by the time passed since the last frame
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / duration);

            transform.position = Vector3.Lerp(startPos, destination, t);

            yield return null;
        }

        //ResetPositions();

        //HardSearchAgain();
    }

    private void ResetPositions()
    {
        transform.position = startPoint;
    }

    private IEnumerator HardSearchMode()
    {
        transform.parent = playerTransform;




        yield return null;


    }

}
