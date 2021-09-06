using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{

    [Header("===== key settings =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA = "space";
    public string keyB = "f";
    public string keyC = "mouse 0";
    public string keyD = "mouse 1";
    public string keyMiddle = "mouse 2";
    public string keyI = "i";//inventory
    public string keyAlt = "left alt";//jog


    [Header("===== others =====")]
    public float mouseSensitivity = 1.0f;
    private ActorControl ac;

    // Use this for initialization
    void Start()
    {
        ac = GetComponent<ActorControl>();
    }

    // Update is called once per frame
    void Update()
    {

        Jup = Input.GetAxis("Mouse Y") * 3.0f * mouseSensitivity;
        Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivity;

        //Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        //Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);
        //ac.acDup = targetDup;
        //ac.acDright = targetDright;

        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        //Vector2 tempVector = SquareToCircle(new Vector2(Dright, Dup));
        //float Dup2 = tempVector.y;
        //float Dright2 = tempVector.x;

        //Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        DVec = new Vector2(Dright, 0);
        Dmag = Dright;

        //run = (Key_A.IsPressing && !Key_A.IsDelaying) || Key_A.IsExtending;
        //walk = Key_Alt.IsPressing;
        //defense = Key_D.IsPressing;//举盾的话只能放在左手并且是轻攻击
        //jump = Key_A.IsExtending && Key_A.OnPressed;
        //rAttack = Key_C.OnPressed;
        //lAttack = Key_D.OnPressed;
        ////重攻击
        //lt = Key_LCtrl.OnPressed;

        ////archer
        //rAim = Key_C.IsPressing;
        //lAim = Key_D.IsPressing;
        //roll = Key_A.IsDelaying && Key_A.OnReleased;
        //lockOn = Key_Middle.OnPressed;
        //inventory = Key_I.OnPressed;
        //changeWeapon = Key_F.OnPressed;
        //canPickUp = Key_E.OnPressed;

        //action = Key_N.OnPressed;


    }

}
