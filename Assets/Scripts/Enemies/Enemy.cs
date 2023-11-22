using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private int path_size = 0;

    private int pathIndex;

    private Transform[] path;

    private List<List<Transform>> pathTransform;

    private Animator anim;

    public void Spawn()
    {
        transform.position = Graph.Entry.transform.position;

        Graph graph = GameObject.Find("Graph").GetComponent<Graph>();
        if (graph != null){
            pathTransform = graph.path_transform;

            path_size = pathTransform[0].Count;

            path = new Transform[path_size];

            for (int i = 0; i < path_size - 1; i++){
                path[i] = pathTransform[0][i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[pathIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, path[pathIndex].position) < 0.1f)
        {
            pathIndex++;
        }

        if (pathIndex == path_size - 1)
        {
            Destroy(gameObject);
        }
    }

    public bool IsActive(){
        return true;
    }
}
