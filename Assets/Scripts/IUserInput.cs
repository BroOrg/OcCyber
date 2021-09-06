using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Header("===== output signals =====")]
    public float Jup;
    public float Jright;
    public float Dup;
    public float Dright;
    public Vector3 DVec;
    public float Dmag;

    //持续
    public bool run;
    public bool walk;
    //一次性trigger
    public bool jump;
    protected bool lastJump = false;
    public bool action;
    //public bool attack;
    public bool rAttack;


    protected bool lastAttack = false;
    public bool defense = false;
    public bool roll = false;
    public bool lockOn = false;

    //double Trigger

    [Header("===== others =====")]
    public bool inputEnabled = true;

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
    protected void UpdateDmagDvec(float Dup2, float Dright2)
    {
        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        DVec = Dright2 * transform.right + Dup2 * transform.forward;
    }
}
