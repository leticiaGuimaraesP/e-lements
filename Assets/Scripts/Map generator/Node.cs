using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Node : ScriptableObject
{
    public GameObject tile;

    public Node top = null;
    public Node bottom = null;
    public Node left = null;
    public Node right = null;

    public bool canRecieveTower = false;
    public bool canRecieveDecoration;
    public bool isPath = false;
    public bool Visited = false;

    public bool isEntry = false;

    public bool isLast = false;

    public int x;
    public int y;

    public Node(int x, int y){
        this.x = x;
        this.y = y;
    }

    public Node(){}


}
