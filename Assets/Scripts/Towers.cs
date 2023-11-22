using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{

    [SerializeField] private string projectileType;
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
    private SpriteRenderer mySpriteRenderer;

    private Enemy target;

    public Enemy Target{
        get{
            return target;
        }
    }

    private Queue<Enemy> enemies = new Queue<Enemy>();

    private bool canAttack = true;

    private float attackTimer;
    [SerializeField] private float attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (target) Debug.Log(target);
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    private void Attack()
    {

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if (target == null && enemies.Count > 0)
        {
            target = enemies.Dequeue();

        }
        if (target != null /*&& target.IsActive()*/)
        {
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }

    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = new Vector2(transform.position.x, transform.position.y + .55f);

        projectile.Initialize(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Enqueue(other.GetComponent<Enemy>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
        }
    }
}
