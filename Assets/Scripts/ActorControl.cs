using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ROLES
//{
//    SABER,
//    AECHER,
//    HAND
//}
public class ActorControl : MonoBehaviour
{

    //public GameObject model;
    //public CameraControl camCon;
    ////private StateManager sm;
    //private GlobalControl GC;
    //public ROLES role;

    [Space(10)]
    [Header("===== speed control =====")]
    public float walkSpeed = 4.0f;
    public float jumpSpeed = 3.0f;
    public float rollSpeed = 3.0f;
    public float runMultipl = 1.7f;//the parameter of runspeed
    public float walkMultipl = 0.8f;//the parameter of walkspeed
    public float acDup;
    public float acDright;

    //[Space(10)]
    //[Header("===== friction settings =====")]
    //public PhysicMaterial frictionOne;
    //public PhysicMaterial frictionZero;

    public IUserInput Pi;
    public Animator anim;//这些被破坏封装完整性的以后要考虑变回去（get方法等等）

    private Rigidbody2D rigid2d;


    private Vector2 moveVelocity;
    private bool lockVelocity = false;
    private bool trackDirection = false;//锁定模型方向
    public bool isFall = false;
    private Vector2 thrustVector = Vector2.zero;
    private bool canAttack = false;
    private bool ground = true;

    ////private float targetLerp;
    private Vector2 deltaPos;


    // Use this for initialization
    void Awake()
    {
        //GC = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GlobalControl>();
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                Pi = input;
                break;
            }
        }
        //anim = model.GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        //capCol = GetComponent<CapsuleCollider>();

        //telePage = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TeleportPage>();
        //camCon = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {


        #region
        ////根据玩家角色的不同，选择不同的移动与攻击动作---------------------------------------------
        //if (isArcher)
        //{
        //    anim.SetInteger("Roles", 1);
        //    role = ROLES.AECHER;
        //}
        //else if (isHand)
        //{
        //    anim.SetInteger("Roles", 2);
        //    role = ROLES.HAND;
        //}
        //else
        //{
        //    anim.SetInteger("Roles", 0);
        //    role = ROLES.SABER;
        //}

        ////移动动画速度------------------------------------------------------------------------------
        //if (camCon.lockState == false)
        //{
        //    anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), Pi.Dmag * ((Pi.run && sm.Energy > 0) ? 2.0f : 1.0f) * (Pi.walk ? 0.5f : 1.0f), 0.3f));
        //    anim.SetFloat("right", 0);
        //}
        //else
        //{
        //    Vector3 localDvec = transform.InverseTransformVector(Pi.DVec);
        //    anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), localDvec.z, 0.3f));
        //    anim.SetFloat("right", localDvec.x);


        //}


        //anim.SetBool("defense", Pi.defense);

        ////Base Layer 动画状态------------------------------------------------------------------------
        //if (Pi.jump && sm.Energy > 5)
        //{
        //    anim.SetTrigger("jump");
        //    canAttack = false;
        //}
        //if (Pi.roll && sm.Energy > 5)
        //{
        //    anim.SetTrigger("roll");
        //    anim.SetBool("Aim", false);
        //    canAttack = false;
        //}
        //if (rigid.velocity.y < -8.0f)
        //{
        //    anim.SetTrigger("fallLand");
        //}
        //if (Pi.canPickUp && (sm.pressEtype == "Door"))
        //{
        //    anim.SetTrigger("push");
        //    canAttack = false;
        //}
        //if (Pi.canPickUp && sm.pressEtype == "Item")
        //{
        //    anim.SetTrigger("pick");
        //    canAttack = false;
        //}


        ////攻击动画设置---------------------------------------------------------------------------------
        //if (!isArcher)
        //{
        //    if ((Pi.lAttack || Pi.rAttack) && (CheckStateTag("ground") || CheckStateTag("attackL") || CheckStateTag("attackR")) && canAttack && (!telePage.isActiveAndEnabled))
        //    {

        //        if (Pi.rAttack)
        //        {
        //            anim.SetBool("R0L1", false);
        //            anim.SetTrigger("attack");
        //        }
        //        if (Pi.lAttack && !leftIsShield)
        //        {
        //            anim.SetBool("R0L1", true);
        //            anim.SetTrigger("attack");
        //        }

        //    }
        //    if ((Pi.lt || Pi.rt) && (CheckStateTag("ground") || CheckStateTag("attackL") || CheckStateTag("attackR")) && canAttack)
        //    {
        //        if (Pi.rt)
        //        {
        //            //右手重攻击
        //        }
        //        else
        //        {
        //            if (!leftIsShield)
        //            {
        //                //左手重攻击
        //            }
        //            else
        //            {
        //                anim.SetTrigger("counterBack");
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if ((CheckStateTag("ground") || CheckStateTag("shoot")) && canAttack)
        //    {
        //        if (Pi.rAim)
        //        {
        //            //anim.SetBool("R0L1", false);
        //            anim.SetBool("Aim", true);
        //        }
        //        else
        //        {
        //            anim.SetBool("Aim", false);
        //        }
        //        if (Pi.lAim)
        //        {
        //            //anim.SetBool("R0L1", false);
        //            camCon.archerAim = true;
        //        }
        //        else
        //        {
        //            camCon.archerAim = false;
        //        }
        //    }
        //}

        ////shield----------------------------------------------------------------------------------
        //if (leftIsShield)
        //{
        //    if (CheckStateTag("ground") || CheckState("blocked"))
        //    {
        //        anim.SetBool("defense", Pi.defense);
        //        anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 1);
        //    }
        //    else
        //    {
        //        anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 0);
        //        anim.SetBool("defense", false);
        //    }
        //}
        //else
        //{
        //    anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 0);
        //}

        ////锁定
        //if (Pi.lockOn)
        //{
        //    camCon.LockUnLock();
        //}


        /**---------------------------------------------------------------------------------------**/
        //    //移动速度设置,模型方向设置
        //    if (camCon.lockState == false)
        //    {
        //        if (Pi.inputEnabled)
        //        {
        //            if (Pi.Dmag > 0.1f)
        //            {
        //                model.transform.forward = Vector3.Slerp(model.transform.forward, Pi.DVec, 0.3f);
        //            }
        //        }
        //        if (lockVelocity == false)
        //        {
        //            moveVelocity = Pi.Dmag * model.transform.forward * walkSpeed * ((Pi.run && sm.Energy > 0) ? runMultipl : 1.0f) * (Pi.walk ? walkMultipl : 1.0f)
        //                * (CheckStateTag("shoot") ? walkMultipl : 1.0f) * (Pi.state == "chase" ? runMultipl / 1.5f : 1.0f);
        //        }
        //    }
        //    else
        //    {
        //        //如果需要锁住方向（翻滚，跳跃等）
        //        if (trackDirection == true)
        //        {
        //            model.transform.forward = moveVelocity.normalized;
        //        }
        //        else
        //        {
        //            //当有输入移动的时候，要让模型与Handler一致，重新移动，否则锁定时只跟随视角，不跟随模型
        //            if (Pi.Dmag > 0.1f)
        //            {
        //                anim.SetBool("lockTurn", false);
        //                model.transform.forward = transform.forward;
        //            }
        //            //当没有输入，且当模型与角度相差比较大的时候，模型需要转向动作
        //            else if (CheckStateTag("ground"))
        //            {
        //                GameObject target = camCon.GetLockTarget();
        //                Vector3 receiveDir = target.transform.position - model.transform.position;
        //                //利用法线将angle实现正负值
        //                float lockAngle = FindAngle(model.transform.forward, receiveDir);
        //                bool lockTurn = Mathf.Abs(lockAngle) > 20;
        //                //anim.SetFloat("angle", lockAngle);
        //                if (lockTurn)
        //                {
        //                    anim.SetFloat("forward", 0.5f);
        //                    model.transform.forward = transform.forward;
        //                }
        //                //else
        //                //{
        //                //    anim.SetBool("lockTurn", false);
        //                //    anim.SetFloat("angle", 0);
        //                //}
        //            }

        //        }
        //        //没有所速度的话（不在跳跃，翻滚中）需要由pi决定速度
        //        if (lockVelocity == false)
        //        {
        //            moveVelocity = Pi.DVec * (camCon.isAI ? walkSpeed / 2 : walkSpeed) * (Pi.run ? runMultipl : 1.0f) * (CheckStateTag("shoot") ? 0.5f : 1.0f);
        //        }

        //    }

        //    //换武器动画
        //    if (Pi.changeWeapon)
        //    {
        //        if (CheckStateTag("ground"))
        //        {
        //            anim.SetTrigger("changeWP");
        //        }
        //    }

        //    //action设定（背刺）
        //    if (Pi.action)
        //    {
        //        OnAction.Invoke();
        //    }



        //}
        #endregion

        moveVelocity = Pi.DVec * walkSpeed * (Pi.run ? runMultipl : 1.0f);
        

        #region  
        //public bool CheckState(string animName, string layerName = "Base Layer")
        //{
        //    return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(animName);
        //}
        //public bool CheckStateTag(string tagName, string layerName = "Base Layer")
        //{
        //    return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
        //}
        ////
        ////

        //public void isIssueTrigger(string triggerName)
        //{
        //    anim.SetTrigger(triggerName);
        //}
        //public void SetBool(string boolName, bool value)
        //{
        //    anim.SetBool(boolName, value);
        //}
        ////
        ////
        ////


        //public void OnJumpEnter()
        //{
        //    Pi.inputEnabled = false;
        //    lockVelocity = true;
        //    trackDirection = true;
        //    thrustVector = new Vector3(0, jumpSpeed, 0);
        //    sm.Energy -= 5.0f;
        //    sm.canRecover = false;
        //}
        //public void OnGroundEnter()
        //{
        //    isFall = false;
        //    Pi.inputEnabled = true;
        //    lockVelocity = false;
        //    trackDirection = false;
        //    canAttack = true;
        //    capCol.material = frictionOne;
        //    sm.canRecover = true;
        //}
        //public void OnGroundExit()
        //{
        //    capCol.material = frictionZero;

        //}
        //public void OnFallEnter()
        //{
        //    isFall = true;
        //    Pi.inputEnabled = false;
        //    lockVelocity = true;
        //    trackDirection = true;
        //    sm.canRecover = true;
        //}
        //public void OnFallLandEnter()
        //{
        //    Pi.inputEnabled = false;
        //    moveVelocity = Vector3.zero;
        //}
        //public void OnRollEnter()
        //{
        //    if (!isFall)
        //    {
        //        model.transform.forward = (acDright * transform.right + acDup * transform.forward);
        //    }
        //    isFall = false;
        //    Pi.inputEnabled = false;
        //    lockVelocity = true;
        //    trackDirection = true;
        //    thrustVector = new Vector3(0, rollSpeed, 0);
        //    sm.Energy -= 10.0f;
        //    sm.canRecover = false;
        //}
        //public void OnRollExit()
        //{
        //    moveVelocity = (acDright * transform.right + acDup * transform.forward) * walkSpeed;

        //}
        //public void OnJabEnter()
        //{
        //    Pi.inputEnabled = false;
        //    lockVelocity = true;
        //    sm.Energy -= 3.0f;
        //    sm.canRecover = false;
        //}
        //public void OnJabUpdate()
        //{
        //    thrustVector = model.transform.forward * (anim.GetFloat("jabVelocity"));
        //}
        //public void OnPickEnter()
        //{
        //    Pi.inputEnabled = false;
        //    if (role == ROLES.SABER)
        //    {
        //        leftIsShield = false;
        //    }
        //    sm.currentWeaponL.SetActive(false);
        //    sm.currentWeaponR.SetActive(false);
        //    sm.canRecover = true;
        //}
        //public void OnPickExit()
        //{
        //    if (role == ROLES.SABER)
        //    {
        //        leftIsShield = true;
        //    }
        //    sm.currentWeaponL.SetActive(true);
        //    sm.currentWeaponR.SetActive(true);
        //}
        //public void OnPushEnter()
        //{
        //    Pi.inputEnabled = false;
        //    if (role == ROLES.SABER)
        //    {
        //        leftIsShield = false;
        //    }
        //    sm.currentWeaponL.SetActive(false);
        //    sm.currentWeaponR.SetActive(false);
        //}
        //public void OnPushExit()
        //{
        //    if (role == ROLES.SABER)
        //    {
        //        leftIsShield = true;
        //    }
        //    sm.currentWeaponL.SetActive(true);
        //    sm.currentWeaponR.SetActive(true);
        //}
        //public void OnAttackEnter()
        //{
        //    sm.canRecover = false;
        //    sm.Energy -= 20.0f;
        //}

        //public void OnAttack1hAEnter()
        //{
        //    Pi.inputEnabled = false;
        //    //targetLerp = 1.0f;

        //}
        //public void OnAttack1hAUpdate()
        //{
        //    //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack Layer")), targetLerp, 0.4f));
        //    thrustVector = model.transform.forward * (anim.GetFloat("attack1hAVelocity"));
        //}
        //public void OnAttack1hBUpdate()
        //{
        //    thrustVector = model.transform.forward * (anim.GetFloat("attack1hBVelocity"));
        //}
        //public void OnAttackExit()
        //{
        //    model.SendMessage("WeaponDisable");
        //}
        //public void OnDelaySlashUpdate()
        //{
        //    thrustVector = model.transform.forward * (anim.GetFloat("delaySlashVelocity"));
        //}


        //public void OnHitEnter()
        //{
        //    Pi.inputEnabled = false;
        //    moveVelocity = Vector3.zero;
        //    //防止万一两个人正好打在一块然后没关掉武器
        //    model.SendMessage("WeaponDisable");
        //    model.SendMessage("CounterBackDisable");
        //}
        //public void OnDieEnter()
        //{
        //    Pi.inputEnabled = false;
        //    moveVelocity = Vector3.zero;
        //    //防止万一两个人正好打在一块然后没关掉武器
        //    model.SendMessage("WeaponDisable");
        //    rigid.velocity = Vector3.zero;
        //    transform.Translate(Vector3.zero);
        //    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //    this.enabled = false;
        //}
        //public void OnBlockedEnter()
        //{
        //    Pi.inputEnabled = false;
        //    thrustVector = model.transform.forward * -5.0f;
        //    sm.Energy -= 5.0f;
        //}
        //public void OnBlockedExit()
        //{
        //    Pi.inputEnabled = true;
        //}
        //public void OnAimingEnter()
        //{
        //    drawTime = true;
        //    Pi.inputEnabled = false;
        //}
        //public void OnAimingExit()
        //{
        //    Pi.inputEnabled = true;
        //}
        //public void OnShootEnter()
        //{
        //    Pi.inputEnabled = false;
        //    shootTime = true;
        //}
        //public void OnShootExit()
        //{
        //    Pi.inputEnabled = true;
        //}

        //public void OnStunnedEnter()
        //{
        //    Pi.inputEnabled = false;
        //    moveVelocity = Vector3.zero;
        //}
        //public void OnCounterBackEnter()
        //{
        //    Pi.inputEnabled = false;
        //    moveVelocity = Vector3.zero;
        //}
        //public void OnLockEnter()
        //{
        //    Pi.inputEnabled = false;
        //    moveVelocity = Vector3.zero;
        //    model.SendMessage("WeaponDisable");
        //}

        //public void OnRootMotionUpdate(object _deltaPos)
        //{
        //    if (CheckState("attack1hC"))
        //        deltaPos += deltaPos * 0.5f + (Vector3)_deltaPos * 0.5f;
        //}
        ////
        ////
        ////
        //public void IsGround()
        //{
        //    anim.SetBool("isGround", true);
        //    ground = true;
        //}
        //public void IsNotGround()
        //{
        //    anim.SetBool("isGround", false);
        //    ground = false;
        //}
        #endregion
    }

    void FixedUpdate()
    {
        //rigid.position += moveVelocity * Time.fixedDeltaTime;
        rigid2d.position += deltaPos;
        rigid2d.velocity = new Vector2(moveVelocity.x, rigid2d.velocity.y) + thrustVector;
        transform.rotation = Quaternion.Euler(0.0f, Pi.Dmag > 0 ? 0 : -180.0f, 0.0f);
        thrustVector = Vector2.zero;
        deltaPos = Vector2.zero;
    }
}
