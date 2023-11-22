using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    private SpriteRenderer mySpriteRenderer;

    void Start () 
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update () 
    {

    }

    public void Select() {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    } 
}