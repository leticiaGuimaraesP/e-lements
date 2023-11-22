using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[Serializable]
public class Stat
{

  [SerializeField]
  private BarScript bar;

  [SerializeField]
  private float maxValue;

  [SerializeField]
  private float currentValue;

  public float CurrentValue
  {
    get
    {
      return currentValue;
    }
    set
    {
      this.currentValue = value;
      bar.Value = currentValue;
    }
  }

  public float MaxValue
  {
    get
    {
      return maxValue;
    }
    set
    {
      this.maxValue = value;
      bar.MaxValue = maxValue;
    }
  }

  public void Initialize()
  {
    this.MaxValue = maxValue;
    this.CurrentValue = currentValue;
  }
}