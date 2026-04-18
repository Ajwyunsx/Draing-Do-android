using System;
using UnityEngine;

[Serializable]
public class Whip : MonoBehaviour
{
    private Transform trans;

    private GameObject whipObj;

    private int WhipTimer;

    private int MaxWhipTimer;

    public virtual void Start()
    {
        trans = transform;
    }

    public virtual void FixedUpdate()
    {
        MouseControl();
        if (WhipTimer <= 0)
        {
            return;
        }

        WhipTimer++;

        if (WhipTimer == 2)
        {
            CreateWhip();
        }

        if (Global.Boomerang)
        {
            return;
        }

        if (WhipTimer > 2 && WhipTimer < 8)
        {
            whipObj.SendMessage("Crusher", null, SendMessageOptions.DontRequireReceiver);
        }

        if (WhipTimer >= MaxWhipTimer)
        {
            WhipTimer = 0;
            if ((bool)whipObj && !Global.skillNoDelete)
            {
                UnityEngine.Object.Destroy(whipObj);
            }
        }
    }

    public virtual void Update()
    {
        if (!Global.Boomerang && (bool)whipObj && !Global.skillNoDelete)
        {
            whipObj.transform.position = trans.position + new Vector3(0f, 0f, -0.02f);
        }
    }

    public virtual void MouseControl()
    {
        int num = 90;
        int num2 = (int)Mathf.Sign(transform.parent.transform.lossyScale.z);
        Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z - Camera.main.transform.position.z));

        if (num2 == -1)
        {
            num = -90;
        }

        int num3 = 1;
        Vector3 localScale = trans.localScale;
        localScale.z = num3;
        trans.localScale = localScale;

        int num5 = 1;
        Vector3 localScale2 = trans.localScale;
        localScale2.y = num5;
        trans.localScale = localScale2;

        Vector3 vector6 = vector - trans.position;

        if (num2 == -1)
        {
            int num7 = -1;
            Vector3 localScale3 = trans.localScale;
            localScale3.z = num7;
            trans.localScale = localScale3;

            int num9 = -1;
            Vector3 localScale4 = trans.localScale;
            localScale4.y = num9;
            trans.localScale = localScale4;
        }
    }

    public virtual void StrikeWhip(int t)
    {
        if ((bool)whipObj)
        {
            UnityEngine.Object.Destroy(whipObj);
        }

        WhipTimer = 1;
        MaxWhipTimer = t;
    }

    public virtual void CreateWhip()
    {
        if ((bool)whipObj)
        {
            UnityEngine.Object.Destroy(whipObj);
        }

        whipObj = UnityEngine.Object.Instantiate(
            SlotLib.Tool[Convert.ToInt32(Global.Var["Skill"])],
            trans.position,
            Quaternion.identity
        );

        Vector3 localEulerAngles = whipObj.transform.localEulerAngles;
        localEulerAngles.z = -90;
        whipObj.transform.localEulerAngles = localEulerAngles;

        float y = whipObj.transform.localScale.y * Mathf.Sign(transform.parent.transform.lossyScale.z);
        Vector3 localScale = whipObj.transform.localScale;
        localScale.y = y;
        whipObj.transform.localScale = localScale;
    }

    public virtual void Main()
    {
    }
}