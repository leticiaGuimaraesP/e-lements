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

    private SpriteRenderer renderer;

    [SerializeField]
    private Stat health;

    public BarScript healthBar;

    private void Awake()
    {
        health.Initialize();
        renderer = GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    public void Spawn()
    {
        transform.position = Graph.Entry.transform.position;

        Graph graph = GameObject.Find("Graph").GetComponent<Graph>();
        if (graph != null)
        {
            pathTransform = graph.path_transform;

            path_size = pathTransform[0].Count;

            path = new Transform[path_size];

            for (int i = 0; i < path_size - 1; i++)
            {
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
            GameManager.Instance.RemoveEnemy(this);
            Destroy(gameObject);
        }

        if (health.CurrentValue <= 0)
        {
            Die();
        }
    }

    public void Harm(int damage)
    {
        if (gameObject.activeSelf)
        {
            health.CurrentValue -= damage;
            renderer.color = Color.red;
            StartCoroutine(resetColor());
        }
    }

    private IEnumerator resetColor(){
         yield return new WaitForSeconds(.3f);
         renderer.color = Color.white;
    }

    private void Die()
    {
        anim.SetTrigger("dead");
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        anim.ResetTrigger("dead");
    }
}
