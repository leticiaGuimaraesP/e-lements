using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

  [SerializeField]
  private Stat health;

  public BarScript healthBar;

  private void Awake()
  {
    health.Initialize();
  }

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      health.CurrentValue -= 10;
    }
  }
}