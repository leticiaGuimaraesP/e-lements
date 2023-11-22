using static System.Math;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Graph : MonoBehaviour
{
    [SerializeField] private GameObject emptyTile, pathTile, entryTile, endTile;

    public static Entry Entry { get; set; }

    private Point entrySpawn;

    [SerializeField] public Node tileNode;

    [SerializeField] public GameObject Instance_point, tower, pointPrefab, Spawner, Destroyer;

    [SerializeField] private GameObject tree, flower, grass, grass2;

    private int radius = 24;

    private List<Node> Q1, Q2, Q3, Q4;

    private Node source1, source2, destination1, destination2;

    private List<Node> requiredVertices = new List<Node>();
    private List<Node> bestPath;

    public List<Node> BestPath{
        get{
            if(bestPath == null){
                bestPath = FindBestPath(source1, destination1, destination2);
                printPath(bestPath);
            }
            return new List<Node>(new List<Node>(bestPath));
        }
    }

    public List<List<Transform>> path_transform = new List<List<Transform>>();

    [SerializeField] public int life;

    public GameObject gameOver;
    public GameObject gameOverFade;

    bool gameOverInvokeBool = false;

    private MatrixGraph matrix;

    [SerializeField] private Transform map;


    // start is called before the first frame update
    void Awake()
    {
        life = 1;
        GameObject textLife = GameObject.Find("Canvas/Image/Text _life");
        //textLife.AddComponent<Player_life>();
        Vector3 novaEscala = new Vector3((float)2, (float)2.5, 0);

        // Atribua a nova escala ao GameObject
        tower.transform.localScale = novaEscala;

        matrix = new MatrixGraph(radius);

        Q1 = matrix.getQuadrant(1);
        Q2 = matrix.getQuadrant(2);
        Q3 = matrix.getQuadrant(3);
        Q4 = matrix.getQuadrant(4);

        sortSourceAndDestination();
        sortQVertex();

        List<Node> path1 = new List<Node>();
        List<Node> path2 = new List<Node>();
        List<Node> path3 = new List<Node>();
        List<Node> path4 = new List<Node>();

        //Busca do Q1
        path1.AddRange(BreadthFirstPaths.BFS(source1, requiredVertices, requiredVertices[1]));
        path2.AddRange(BreadthFirstPaths.BFS(source1, requiredVertices, requiredVertices[0]));

        //Busca do Q2
        path3.AddRange(BreadthFirstPaths.BFS(source1, requiredVertices, requiredVertices[2]));
        path4.AddRange(BreadthFirstPaths.BFS(source1, requiredVertices, requiredVertices[3]));

        //Busca do Q3
        path3.AddRange(BreadthFirstPaths.BFS(requiredVertices[2], requiredVertices, requiredVertices[5]));
        path4.AddRange(BreadthFirstPaths.BFS(requiredVertices[3], requiredVertices, requiredVertices[4]));

        //Busca ao Destino 1
        path3.AddRange(BreadthFirstPaths.BFS(requiredVertices[5], requiredVertices, destination1));
        path4.AddRange(BreadthFirstPaths.BFS(requiredVertices[4], requiredVertices, destination1));

        //Busca do Q4
        path1.AddRange(BreadthFirstPaths.BFS(requiredVertices[1], requiredVertices, requiredVertices[6]));
        path2.AddRange(BreadthFirstPaths.BFS(requiredVertices[0], requiredVertices, requiredVertices[7]));

        //Busca ao Destino 2
        path1.AddRange(BreadthFirstPaths.BFS(requiredVertices[6], requiredVertices, destination2));
        path2.AddRange(BreadthFirstPaths.BFS(requiredVertices[7], requiredVertices, destination2));

        printMap();

        //Encontra o melhor caminho (mais curto) da origem até o destino
        bestPath = FindBestPath(source1, destination1, destination2);
        printPath(bestPath);

        printPath(path1);
        printPath(path2);
        printPath(path3);
        printPath(path4);
    }

    public List<int> GenerateUniqueRandomNumbers(int min, int max, int count)
    {
        if (count > max - min + 1 || max < min)
        {
            // Handle invalid input
            return null;
        }

        List<int> numbers = new List<int>();
        HashSet<int> uniqueNumbers = new HashSet<int>();

        while (uniqueNumbers.Count < count)
        {
            int randomNumber = Random.Range(min, max + 1);

            if (!uniqueNumbers.Contains(randomNumber))
            {
                uniqueNumbers.Add(randomNumber);
                numbers.Add(randomNumber);
            }
        }

        return numbers;
    }

    public void printMap()
    {
        Node tileToPrint = matrix.head.bottom;
        for (int i = 0; i < radius; i++)
        {
            Node firstPosition = tileToPrint;

            for (int j = 0; j < radius; j++)
            {

                if (!tileToPrint.isPath)
                {
                    GameObject newTile = Instantiate(emptyTile);
                    newTile.transform.position = new Vector3(tileToPrint.x * 2, -(tileToPrint.y * 2), 0);
                    newTile.GetComponent<TileScript>().Setup(new Point(tileToPrint.x, tileToPrint.y), map);
                    tileToPrint.TileRef = newTile.GetComponent<TileScript>();

                    if (!tileToPrint.canRecieveTower)
                    {
                        //  int x = Random.Range(0, 10);
                        //  if (x % 2 == 0)
                        //  {
                        //     Instantiate(flower, new Vector2(tileToPrint.x * 2, -(tileToPrint.y * 2) + (float)0.1), Quaternion.identity);
                        //  }
                        //  else if (x % 3 == 0)
                        //  {
                        //      Instantiate(grass, new Vector2(tileToPrint.x * 2, -(tileToPrint.y * 2) + (float)0.1), Quaternion.identity);
                        //  }
                        //  else if (x % 5 == 0 || x % 7 == 0)
                        //  {
                        //    GameObject newTree = Instantiate(tree, new Vector2(tileToPrint.x * 2, -(tileToPrint.y * 2)), Quaternion.identity);
                        //    // float treeZPosition = 1.5f; // Defina a posição Z desejada para a árvore
                        //    //newTree.transform.position = new Vector3(newTree.transform.position.x, newTree.transform.position.y, treeZPosition);

                        //  }
                        //  else
                        // {
                        //      Instantiate(grass2, new Vector2(tileToPrint.x * 2, -(tileToPrint.y * 2) + (float)0.1), Quaternion.identity);
                        //  }
                    }

                }

                tileToPrint = tileToPrint.right;
            }


            tileToPrint = firstPosition.bottom;
        }
    }

    public void printPath(List<Node> path)
    {
        int index = 0;
        List<Transform> path1 = new List<Transform>();

        foreach (Node n in path)
        {

            if (!n.isEntry && !n.isLast)
            {
                GameObject newTile = Instantiate(pathTile);
                newTile.transform.position = new Vector3(n.x * 2, -(n.y * 2), 0);
                newTile.transform.SetParent(map);
                path1.Add(newTile.transform);
            }
            else if (n.isEntry == true)
            {
                entrySpawn = new Point(n.x * 2, -(n.y * 2));
                GameObject tmp = Instantiate(entryTile);
                tmp.transform.position = new Vector3(n.x * 2, -(n.y * 2), 0);
                tmp.transform.SetParent(map);
                Entry = tmp.GetComponent<Entry>();
                Entry.name = "Entry";
                path1.Add(tmp.transform);
            }
            else
            {
                GameObject newTile = Instantiate(endTile);
                newTile.transform.position = new Vector3(n.x * 2, -(n.y * 2), 0);
                newTile.transform.SetParent(map);
                path1.Add(newTile.transform);
            }

            index++;

        }
        path_transform.Add(path1);
    }


    public void sortSourceAndDestination()
    {
        int x;
        x = Random.Range(0, matrix.n - 7);
        source1 = Q1[x];

        x = Random.Range(Q3.Count - (matrix.n - 1), Q3.Count - 6);
        destination1 = Q3[x];

        x = Random.Range(Q4.Count - (matrix.n - 6), Q4.Count - 1);
        destination2 = Q4[x];

        source1.isPath = destination1.isPath = destination2.isPath = true;
        source1.isEntry = destination1.isLast = destination2.isLast = true;

        destination1.isLast = destination2.isLast = true;
    }
    public void sortQVertex()
    {
        int pos;
        int offset;
        int temp;

        //Sorteio dos vértices Q1
        offset = Random.Range((radius / 3), (radius / 2));
        temp = offset * (radius / 2);
        offset = Random.Range((radius / 8), (radius / 4));
        pos = offset + temp;
        this.requiredVertices.Add(Q1[pos]);

        offset = Random.Range((radius / 8), (radius / 4));
        temp = offset * (radius / 2);
        offset = Random.Range((radius / 3), (radius / 2));
        pos = offset + temp;
        requiredVertices.Add(Q1[pos]);


        //Sorteio dos vértices Q2
        offset = Random.Range((radius / 8), (radius / 4));
        temp = offset * (radius / 2);
        pos = offset + temp;
        this.requiredVertices.Add(Q2[pos]);

        offset = Random.Range((radius / 3), (radius / 2));
        temp = offset * (radius / 2);
        pos = offset + temp;
        requiredVertices.Add(Q2[pos]);


        //Sorteio dos vértices Q3
        offset = Random.Range((radius / 8), (radius / 4));
        temp = offset * (radius / 2);
        pos = offset + temp;
        this.requiredVertices.Add(Q3[pos]);

        offset = Random.Range((radius / 3), (radius / 2));
        temp = offset * (radius / 2);
        pos = offset + temp;
        requiredVertices.Add(Q3[pos]);


        //Sorteio dos vértices Q4
        offset = Random.Range((radius / 3), (radius / 2));
        temp = offset * (radius / 2);
        offset = Random.Range((radius / 8), (radius / 4));
        pos = offset + temp;
        requiredVertices.Add(Q4[pos]);

        offset = Random.Range((radius / 8), (radius / 4));
        temp = offset * (radius / 2);
        offset = Random.Range((radius / 3), (radius / 2));
        pos = offset + temp;
        this.requiredVertices.Add(Q4[pos]);


        foreach (Node n in requiredVertices)
        {
            n.isPath = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            // A condição para Game Over foi atendida
            // Carregue a cena de Game Over
            gameOver.SetActive(true);
            gameOverFade.SetActive(true);
            if (!gameOverInvokeBool)
            {
                gameOverInvokeBool = true;
                gameOverFade.SetActive(false);
                Invoke("callMenu", 3f);
            }

        }
    }

    void callMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    //Algoritmo A*
    public List<Node> FindBestPath(Node entry, Node destiny1, Node destiny2) //Origem e os dois destinos
    {
        List<Node> bestPath = new List<Node>();
        bestPath = null;

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(entry);
        entry.parent = null;
        entry.distEntry = 0;
        entry.heuristic = CalculateHeuristic(entry, destiny1, destiny2);
        entry.funcN = CalculateFunction(entry);


        while (openSet.Count > 0) //Não sei se esse teste está correto
        {
            Node current = openSet[0];

            // Encontra o nó com o menor custo total F
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].funcN < current.funcN || (openSet[i].funcN == current.funcN && openSet[i].heuristic < current.heuristic))
                {
                    current = openSet[i];
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == destiny1 || current == destiny2) //Se o caminho chegou no destino
            {
                bestPath = ReconstructPath(current);
                return bestPath;
            }

            // Para cada vizinho do nó atual
            foreach (Node neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                if (neighbor.isPath)
                { //Valida se o vizinho faz parte de algum caminho
                    int tentativeDist = current.distEntry + 1; // Custo de movimento é assumido como 1

                    // Se o vizinho não está no conjunto aberto ou tem um custo menor do que calculado anterriormente
                    if (!openSet.Contains(neighbor) || tentativeDist < neighbor.distEntry)
                    {
                        //atualiza os dados
                        neighbor.parent = current;
                        neighbor.distEntry = tentativeDist;
                        neighbor.heuristic = CalculateHeuristic(neighbor, destiny1, destiny2);
                        neighbor.funcN = CalculateFunction(neighbor);

                        // Se o vizinho não está no conjunto aberto, adiciona
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
        return bestPath;
    }


    static List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        if (node.top != null)
            neighbors.Add(node.top);

        if (node.bottom != null)
            neighbors.Add(node.bottom);

        if (node.left != null)
            neighbors.Add(node.left);

        if (node.right != null)
            neighbors.Add(node.right);

        return neighbors;
    }

    public double CalculateHeuristic(Node a, Node b, Node c)
    {
        //Heuristica em relação ao destino 1
        double val1 = Pow(Abs(b.x - a.x), 2);
        double val2 = Pow(Abs(b.y - a.y), 2);
        double result1 = Sqrt(val1 + val2);

        //Heuristica em relação ao destino 2
        val1 = Pow(Abs(c.x - a.x), 2);
        val2 = Pow(Abs(c.y - a.y), 2);
        double result2 = Sqrt(val1 + val2);

        //Retorna a menor Heuristica
        if (result1 < result2)
        {
            return result1;
        }
        else
        {
            return result2;
        }
    }

    public double CalculateFunction(Node a)
    {
        return a.distEntry + a.heuristic;
    }

    public int CalculateDist(Node a)
    {
        int dist;
        if (a.parent == null)
        { // Se o no for a raiz
            dist = 0;
        }
        else
        {
            dist = a.parent.distEntry + 1; // Toda aresta possui custo 1
        }
        return dist;
    }

    // Reconstroi o caminho a partir do no final
    static List<Node> ReconstructPath(Node node)
    {
        List<Node> path = new List<Node>();
        while (node != null)
        {
            //Debug.Log("entrou");
            path.Add(node);
            node = node.parent;
        }

        // Adiciona o ultimo nó (origem do caminho)
        path.Reverse(); // Reverte a ordem para obter do início ao fim

        return path;
    }

}


class MatrixGraph
{
    private int radius;
    public int n;
    private int x;
    public Node head;


    public MatrixGraph(int radius)
    {
        this.radius = radius;

        this.n = (int)radius / 2;
        this.x = (int)n * n;

        //head = head Node 
        head = new Node();
        head.bottom = new Node(0, 0);

        Node columnCurrent = head.bottom, lineCurrent;

        // First line 
        for (int i = 0; i < radius - 1; i++)
        {
            columnCurrent.right = new Node(i + 1, 0);
            columnCurrent.right.left = columnCurrent;
            columnCurrent = columnCurrent.right;
        }

        for (int i = 1; i < radius; i++)
        {
            while (columnCurrent.left != null)
            {
                columnCurrent = columnCurrent.left;
            }

            // next line
            columnCurrent.bottom = new Node(0, i);
            lineCurrent = columnCurrent.bottom;
            lineCurrent.top = columnCurrent;

            // building line`s column
            for (int j = 0; j < radius - 1; j++)
            {
                lineCurrent.right = new Node(j + 1, i);
                lineCurrent.right.left = lineCurrent;

                lineCurrent = lineCurrent.right;
                columnCurrent = columnCurrent.right;

                lineCurrent.top = columnCurrent;
                columnCurrent.bottom = lineCurrent;
            }

            // point to last node in line
            columnCurrent = lineCurrent;
        }

    }

    public Node getQuadrantStart(int q)
    {
        Node ret = head.bottom;
        if (q == 1)
        {
            return ret;
        }
        else if (q == 2)
        {
            while (ret.x != n)
            {
                ret = ret.right;
            }
            return ret;
        }
        else if (q == 3)
        {
            while (ret.y != (n))
            {
                ret = ret.bottom;
            }
            return ret;
        }
        else if (q == 4)
        {
            while (ret.x != n)
            {
                ret = ret.right;
            }
            while (ret.y != (n))
            {
                ret = ret.bottom;
            }
            return ret;
        }

        return null;

    }

    public List<Node> getQuadrant(int q)
    {
        List<Node> list = new List<Node>();
        Node curr = getQuadrantStart(q);
        Node first = curr;

        int count = 0;
        if (curr != null)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    list.Add(curr);
                    curr = curr.right;
                }
                curr = first.bottom;
                first = curr;

            }
        }

        return list;
    }

}