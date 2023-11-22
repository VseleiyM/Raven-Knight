using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScore : MonoBehaviour
{
    [SerializeField] private Transform textScore;
    [SerializeField] private float speed = 1;
    [SerializeField] private float riseDistanse = 1;

    private Vector3 startPosition;

    private void Start()
    {
        StartCoroutine(LifeTimeScore());
    }

    private IEnumerator LifeTimeScore()
    {
        startPosition = textScore.position;
        Vector3 offset = textScore.position - startPosition;
        while (offset.magnitude < riseDistanse)
        {
            textScore.position += Vector3.up * Time.fixedDeltaTime * speed;
            offset = textScore.position - startPosition;
            yield return new WaitForEndOfFrame();
        }
        Destroy(textScore.gameObject);
    }
}
