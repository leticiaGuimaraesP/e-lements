using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private List<Node> path;

    public Point GridPosition { get; set; }

    private  Vector3 destination;

    private void Update(){
        Move();
    }

    public void Spawn(){
        transform.position = Graph.Entry.transform.position;
    }

    private void Move(){
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if(transform.position == destination){
            if(path != null && path.Count > 0){
                //GridPosition = path[0].GridPosition;
                destination = path[0].WorldPosition;
                path[0] = null;
                path = ReorganizeList();
            }
        }
    }

    public List<Node> ReorganizeList(){
        List<Node> tmp = new List<Node>();

        foreach(Node n in path){
            if(n != null){
                tmp.Add(n);
            }
        }

        return tmp;

    }
}
