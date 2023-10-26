using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstPaths : MonoBehaviour{

    public static List<Node> BFS(Node start, List<Node> requiredVertices, Node destination)
    {
        Dictionary<Node, Node> parent = new Dictionary<Node, Node>(); //Nó pai de cada nó visitado

        BFSsearch(start, destination, ref parent, requiredVertices);

        List<Node> path = new List<Node>(); //Caminho entre source e destination
        destination.isPath = true; //Nó pertence ao caminho
        path.Add(destination); //Começa adicionando o nó de destino

        //O caminho será restaurado, partindo do destino até a origem

        Node current = destination; //Nó atual a ser rastreado

        while (current != start) //Enquanto não for o nó de origem
        {   
            //Debug.Log(parent.ContainsKey(current));
            Node parentNode = parent[current];
            path.Add(parentNode); //Adicionar o nó pai ao caminho
            current = parentNode; //Atualizar o nó atual para o nó pai
            current.isPath = true; //Nó pertence ao caminho
        }

        path.Reverse(); //Inverter a lista para obter a ordem correta do caminho

        return path;
    }

    private static bool BFSsearch(Node start, Node destination, ref Dictionary<Node, Node> parent, List<Node> requiredVertices)
    {
        Queue<Node> queue = new Queue<Node>(); //Nós a serem explorados
        HashSet<Node> visited = new HashSet<Node>(); //Conjunto de nós visitados durante a busca

        queue.Enqueue(start); //Adicionar o nó de início à lista de nós a serem explorados

        if(!visited.Contains(start)){
            visited.Add(start); //Adicionar o nó de início ao conjunto
        }

        while (queue.Count != 0) //Enquanto houver nó a ser explorado
        {
            Node currentVertex = queue.Dequeue(); //Obtém o nó atual da fila

            List<Node> adjecent = new List<Node>();
            if(currentVertex.right != null){
                adjecent.Add(currentVertex.right);
            }
            if(currentVertex.top != null){
                adjecent.Add(currentVertex.top);
            }
            if(currentVertex.bottom != null){
                adjecent.Add(currentVertex.bottom);
            }
            if(currentVertex.left != null){
                adjecent.Add(currentVertex.left);
            }

            //Debug.Log(currentVertex.x + " - " + currentVertex.y);

            foreach (Node neighbor in adjecent) //Itera sobre os vizinhos do nó atual
            {
                if(!requiredVertices.Contains(neighbor) || ((neighbor.x == destination.x) && (neighbor.y == destination.y))){ 
                    if (!visited.Contains(neighbor)) //Se o vizinho não foi visitado
                    {
                        //Debug.Log("VIZINHO " + neighbor.x + " - " + neighbor.y);
                        if(!parent.ContainsKey(neighbor)){
                            parent.Add(neighbor, currentVertex); //filho, pai
                        }

                        visited.Add(neighbor); //Marca o vizinho como visitado

                        queue.Enqueue(neighbor); //Adiciona o vizinho a ser explorado nas próxims iterações

                        if ((neighbor.x == destination.x) && (neighbor.y == destination.y)) //Verifica se o vizinho é o destino
                        {
                           // Debug.Log("CHEGUEI");
                            return true; //Existe caminho
                        }
                    }
                }
            }
        }

        return false; //Não existe caminho
    }

}
