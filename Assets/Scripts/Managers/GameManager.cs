using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    private int currency;

    private int lives;

    [SerializeField] private Text currencyTxt;
    [SerializeField] private Text livesTxt;

    [SerializeField] private GameObject waveBtn;

    [SerializeField] private GameObject sellBtn;

    [SerializeField] public Image gameOver;
    [SerializeField] public Image gameOverBack;
    [SerializeField] public GameObject restartButton;

    [SerializeField]  private  Text sellText;
    [SerializeField] public Text lifeText;

    private List<Enemy> activeEnemies = new List<Enemy>();

    //the current selected tower
    private Towers selectedTower;

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get
        {
            return activeEnemies.Count > 0;
        }
    }

    private int wave = 0;

    [SerializeField] private Text waveTxt;
    private Graph graph;
    private List<Node> bestPath;

    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            this.currency = value;
            this.currencyTxt.text = value.ToString() + " <color=lime>$</color>";
        }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lives = 5;
        lifeText.text = string.Format("<color=red>{0}</color>", GameManager.Instance.getLifes());
        Currency = 25;
        sellText.enabled = false;
        gameOver.enabled = false;
        gameOverBack.enabled = false;
        restartButton.SetActive(false);
        sellBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if (Currency >= towerBtn.Price)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;

            Hover.Instance.Deactivate();
        }
    }

    public void SelectTower(Towers tower)
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();

        sellText.enabled = true;
        sellText.text = "+" + (selectedTower.Price/2);

        sellBtn.SetActive(true);
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;

        sellText.enabled = false;
        sellBtn.SetActive(false);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        wave++;
        waveTxt.text = string.Format("WAVE:<color=lime>{0}</color>", wave);
        lifeText.text = string.Format("<color=red>{0}</color>", lives);

        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        GameObject graphObject = GameObject.Find("Graph");
        graph = graphObject.GetComponent<Graph>();

        graph.path_transform.Clear();
        bestPath = graph.FindBestPath(graph.source1, graph.destination1, graph.destination2);
        graph.printPath(bestPath);

        for (int i = 0; i < wave*3; i++)
        {
            int enemyIndex;

            if(wave < 3) {
                enemyIndex = Random.Range(0, 2);
            } else {
                enemyIndex = Random.Range(0, 3);
            }

            string type = string.Empty;

            switch (enemyIndex)
            {
                case 0:
                    type = "enemy1";
                    break;
                case 1:
                    type = "enemy2";
                    break;
                case 2:
                    type = "enemy3";
                    break;
            }

            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
            enemy.Spawn();

            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(2f);
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }

    public void UpdateCurrency(Enemy enemy){
        if(enemy.name == "enemy1"){
            Currency += 1;
        }
        else if(enemy.name == "enemy2"){
            Currency += 2;
        }
        else{
            Currency += 3;
        }
    }

    public void removeLife(int quant) {
        if (lives > 0) {
            lives = lives - quant;
        }
    }

    public void addLife(int quant) {
        lives = lives + quant;
    }

    public int getLifes() {
        return lives;
    }


    public void SellTower(){
        if(selectedTower!=null){
            Currency += selectedTower.Price/2;
            selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;
            Destroy(selectedTower.transform.parent.gameObject);
            sellText.enabled = false;
            DeselectTower();
        }
    }
}