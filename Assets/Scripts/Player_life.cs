using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_life : MonoBehaviour
{
    public TMP_Text textMeshPro;
    // Start is called before the first frame update
     void Start()
    {
         textMeshPro = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
     {
         textMeshPro.text = GameObject.Find("graph").GetComponent<Graph>().life.ToString();
     }
}
