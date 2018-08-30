using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    [Header("Стартові характеристики")]
    public float maxHealth;
    public float maxRangeMove;
    public float rangeAttack;
    public float damage;
    public float moveSpeed = 5;
    
    [Header("Теперешні характеристики")]
    public float curHealth;
    public float curRangeMove;

    [Header("Інше")]
    public string curAnim;
    public float distanceToTarget;
    public InfinityManager infinityManager;

    private bool move = false;
    private Rigidbody2D rb2d;
    private Vector3 direction;
    private Vector3 pointPos;
    private Animator anim;
    private bool anyHappen = false;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curHealth = maxHealth;
        curRangeMove = maxRangeMove;
    }
	
	// Update is called once per frame
	void Update () {
         anim.Play(curAnim);
    }

    private void FixedUpdate()
    {
        if (move)
        {
            curAnim = "walk";

            if (pointPos.x > transform.position.x)
                GetComponent<SpriteRenderer>().flipX = false;
            else GetComponent<SpriteRenderer>().flipX = true;

            distanceToTarget = Vector2.Distance(transform.position, pointPos);
            rb2d.velocity = direction;// двигаєм фіз обєкт в сторону 
        }
        else if(!anyHappen) curAnim = "idle";

        if (distanceToTarget < 0.1)// коли дистанція буде досягнута - зупинитися
        {
            move = false;
            rb2d.velocity = Vector2.zero;
        }

        
    }

    public void SetAnimationIdle()
    {
        anyHappen = false;
        curAnim = "idle";
    }

    public void SetAnimation(string nameAnim)
    {
        curAnim = nameAnim;
        anyHappen = true;
    }

    public void PointToMove(Vector3 point)// точка до якої рухатися
    {
        pointPos = point;

        distanceToTarget = Vector2.Distance(transform.position, pointPos);// визначаєм дистанцію до цілі

        curRangeMove -= distanceToTarget;// віднімаєм дистанція 
        if (curRangeMove < 1)
            curRangeMove = 0;

        direction = (point - transform.position).normalized * moveSpeed;// напрям руху

        move = true;
    }

    public void SetDamageAnimEnemy()
    {
        infinityManager.secondUnit.SetAnimation("damage");
        infinityManager.GetDamageEnemy();
    }
}
