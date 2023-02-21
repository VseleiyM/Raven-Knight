using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterBase : MonoBehaviour
{
    public float offset;
    private float sortingOrderBase = 0;
    private Renderer _renderer;


    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }


    void LateUpdate()
    {
        _renderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);
    }
}
