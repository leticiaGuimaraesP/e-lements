using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

  // [SerializeField]
  // private Stat health;

  // public BarScript healthBar;

  // private Animator anim;


  // private void Awake()
  // {
  //   health.Initialize();
  // }

  // void Start()
  // {
  //   anim = this.GetComponent<Animator>();
  // }

  // void Update()
  // {
  //   if (health.CurrentValue <= 0)
  //   {
  //     Die();
  //   }
  // }

  // public void Harm(int damage)
  // {
  //   if (gameObject.activeSelf)
  //   {
  //     health.CurrentValue -= damage;
  //   }
  // }

  // private void Die()
  // {
  //   anim.SetTrigger("dead");
  //   StartCoroutine(Dying());
  // }

  // private IEnumerator Dying(){
  //   yield return new WaitForSeconds(1f);
  //   GameManager.Instance.Pool.ReleaseObject(gameObject);
  // }
}