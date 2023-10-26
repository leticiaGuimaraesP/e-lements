using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log("Hereee");
        // Verifique se o objeto que entrou no trigger é um personagem ou qualquer outro objeto que você deseja destruir
        if (other.CompareTag("Character")) // Substitua "CharacterTag" pelo nome da tag do objeto que deseja destruir
        {
            // Destrua o objeto que entrou no trigger
            Destroy(other.gameObject);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
       
    // }

    void Start()
    {
        Collider2D c = gameObject.GetComponent<Collider2D>();
        c.enabled = (true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
