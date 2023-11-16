using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition {get; private set;}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos){
        this.GridPosition = gridPos;
    }

    private void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            PlaceTower();
        }
    }

    private void PlaceTower(){
        Instantiate(GameManager.Instance.TowerPrefab, transform.position, Quaternion.identity);
    }
}
