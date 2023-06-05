using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhysicsCheck))]
public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    [HideInInspector]public Animator anim;
    [HideInInspector]public PhysicsCheck physicsCheck;
    [Header("��������")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    public Transform attacker;

    [Header("���")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("��ʱ��")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    //chaseת���walk��ʱ��
    public float lostTime;
    public float lostTimeCounter;


    [Header("״̬")]
    public bool isHurt;
    public bool isDead;

    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected virtual void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        //waitTimeCounter = waitTime;

        //���β���ȷ����ײ
        Physics2D.IgnoreCollision(boxCollider2D, GameObject.Find("dina").GetComponent<CapsuleCollider2D>());
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LoginUpdate();
        TimeCounter();
        

    }
    private void FixedUpdate()
    {
        if(!isHurt && !isDead &&!wait)
            Move();
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    //��ʱ��
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }

        if (!FoundPlayer()&&lostTimeCounter>0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        //else
        //{
        //    
        //}

    }
    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position+(Vector3)centerOffset,checkSize,0,faceDir,checkDistance,attackLayer);

    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }


    #region �¼�ִ�з���
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //ת��
        if(attackTrans.position.x - transform.position.x>0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);

        //���˱�����
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = true;

    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance*-transform.localScale.x,0), 0.2f);
    }
    
}
