using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("�����¼�")]
    public SceneLoadEventSO loadEvent;
    public VoidEventSO afterSceneLoadedEvent;

    public PlayerInputControl inputControl;
    private Vector2 inputDirection;
    public Rigidbody2D rb;
    public CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    //public��¶����ڣ�����ֱ���Ͻ�������privateֻ���ڴ����ڸ�ֵ
    public SpriteRenderer spriteRenderer;
    [Header("��������")]
    public float speed;
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;
    public float jumpForce;
    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]
    public bool isCrouch;
    private Vector2 originalOffset;
    private Vector2 originalSize;
    //public bool isRun = false;
    public bool isHurt;
    public float hurtForce;

    public bool isDead;
    public bool isAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        physicsCheck = GetComponent<PhysicsCheck>();

        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        originalOffset = coll.offset;
        originalSize = coll.size;

        inputControl = new PlayerInputControl();
        
        //��Ծ
        inputControl.Gameplay.Jump.started += Jump;

        #region ǿ����·
        runSpeed = speed;
        //����K�������ܲ�
        inputControl.Gameplay.Run.performed += ctx => 
        {
            if (physicsCheck.isGround)
                speed = walkSpeed;
        };

        inputControl.Gameplay.Run.canceled += ctx =>
        {
            if (physicsCheck.isGround)
                speed = runSpeed;
        };
        #endregion

        //����
        inputControl.Gameplay.Attack.started += PlayerAttack;

        //���֮ǰҪ��ȡ
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayAttack();
        isAttack = true;

    }

    private void OnEnable()
    {
        inputControl.Enable();
        //loadEvent.LoadRequestEvent += onLoadEvent;
        //afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();
        //loadEvent.LoadRequestEvent -= onLoadEvent;
        //afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }



    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        CheckState();
    }

    private void FixedUpdate()
    {
        if(!isHurt && !isAttack)
            Move();
    }


    //���ؽ���֮����������
    private void OnAfterSceneLoadedEvent()
    {
        //inputControl.Gameplay.Enable();
    }

    //�������ع���ֹͣ����
    private void onLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        //inputControl.Gameplay.Disable();
    }
    public void Move()
    {
        if(!isCrouch)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0) 
            faceDir = -1;

        //���﷭ת
        transform.localScale = new Vector3(faceDir, 1, 1);

        //�¶�
        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch)
        {
            //�޸���ײ���С��λ��
            coll.offset = new Vector2(-0.05f, 0.85f);
            coll.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            //��ԭ֮ǰ��ײ�����
            coll.size = originalSize;
            coll.offset = originalOffset;
        }

        //ͨ��spriteRendererʵ�ַ�ת�ķ�ʽ
        //����ע�Ϳ�ݼ�ctrl+shift+/
/*        if (faceDir == 1)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;*/
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //throw new NotImplementedException();
        //Debug.Log("JUMP");
        if(physicsCheck.isGround == true)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
    
    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround?normal : wall;
    }
    //run����ȥ֮�� isRunû�������߼�
/*    private void Run(InputAction.CallbackContext obj)
    {
        isRun = true;
    }*/
}

   