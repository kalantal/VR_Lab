using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

    private Color startcolor;
    private Renderer renderer;
    void Start()
    {
        renderer = gameObject.GetComponent <Renderer>();
    }
    void OnMouseEnter()
    {
        startcolor = renderer.material.color;
        renderer.material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        renderer.material.color = startcolor;
    }
}
