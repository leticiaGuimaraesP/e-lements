# E-LEMENTS - Tower Defense Game 

Projeto desenvolvido em grupo para a matéria Trabalho Interdisciplinar 4, do curso Ciência da Computação da PUC Minas.

Sobre o jogo: Utilizando torres de cada elemento, os jogadores devem empregar estratégias para repelir os inimigos. Com o objetivo enriquecer a jogabilidade, exploramos conceitos avançados de Grafos e Inteligência Artificial:

### Grafos

- O grafo foi gerado a partir de uma matriz, em que o número de linhas e colunas indicam os vértices e arestas.
- Dividimos o grafo em 4 quadrantes, semelhante a um mapa cartesiano.
- Selecionamos aleatoriamente dois vértices em cada quadrante.
- Realizamos buscas em largura para conectar diferentes pontos em quadrantes distintos, gerando caminhos variados até o final do mapa.

### Algoritmo A*

- O algoritmo é executado a cada wave de inimigos.
- Calculamos o caminho a partir dos possíveis caminhos gerados na busca em largura para encontrar a rota mais eficiente.
- Utilizamos a heurística euclidiana, uma heurística admissível.
- O peso das arestas é calculado com base na quantidade de torres presentes no caminho.

Estas implementações de grafos e do algoritmo A* foram fundamentais para a dinâmica e estratégia do nosso jogo de Tower Defense.

### Funcionamento do Jogo

Nesta demonstração, diferentes caminhos são gerados a cada início de partida. Enquanto isso, ondas de inimigos são geradas dinamicamente, desafiando o jogador a posicionar estrategicamente suas torres defensoras para proteger seu território. <br>
<img src="https://github.com/leticiaGuimaraesP/e-lements/blob/main/image1.png" width="400" height="400">

Torres sendo compradas e posicionadas. <br>
<img src="https://github.com/leticiaGuimaraesP/e-lements/blob/main/image4.png" width="400" height="400">

Os inimigos adotando uma abordagem inteligente ao escolher o caminho com menos torres defensoras para alcançar o seu destino. <br>
<img src="https://github.com/leticiaGuimaraesP/e-lements/blob/main/image.png" width="400" height="400"> 

Fim de jogo! <br>
<img src="https://github.com/leticiaGuimaraesP/e-lements/blob/main/image2.png" width="400" height="400">
