using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    private int currency;

    [SerializeField] private Text currencyTxt;

    [SerializeField] private GameObject waveBtn;

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
        Currency = 20;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if (Currency >= towerBtn.Price && !WaveActive)
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
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
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
        waveTxt.text = string.Format("Wave: <color=lime>{0}</color>", wave);

        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        //RECALCULATE PATH WITH A*

        for (int i = 0; i < wave*3; i++)
        {
            int enemyIndex = Random.Range(0, 3);

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
}
