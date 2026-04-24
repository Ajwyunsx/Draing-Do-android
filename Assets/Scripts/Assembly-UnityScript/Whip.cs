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
            if ((bool)whipObj)
            {
                whipObj.SendMessage("Crusher", null, SendMessageOptions.DontRequireReceiver);
            }
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
        if (!Global.Boomerang && (bool)whipObj)
        {
            SyncWhipPosition();
        }
    }

    public virtual void MouseControl()
    {
        if (trans == null || Camera.main == null || transform.parent == null)
        {
            return;
        }
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

        GameObject prefab = SlotLib.GetToolForSkill(CurrentSkillIndex(), Global.skillGOName);
        if (!(bool)prefab)
        {
            WhipTimer = 0;
            return;
        }

        whipObj = UnityEngine.Object.Instantiate(prefab, trans.position, Quaternion.identity);
        NormalizeWhipVisuals(whipObj);
        ApplyInitialWhipTransform();
        RestartWhipAnimators(whipObj);
    }

    private void SyncWhipPosition()
    {
        if (!(bool)whipObj)
        {
            return;
        }
        whipObj.transform.position = trans.position + new Vector3(0f, 0f, -0.02f);
    }

    private void ApplyInitialWhipTransform()
    {
        if (!(bool)whipObj)
        {
            return;
        }

        SyncWhipPosition();
        Vector3 localEulerAngles = whipObj.transform.localEulerAngles;
        localEulerAngles.z = -90;
        whipObj.transform.localEulerAngles = localEulerAngles;

        int direction = CurrentDirection();
        float y = Mathf.Abs(whipObj.transform.localScale.y) * (float)direction;
        Vector3 localScale = whipObj.transform.localScale;
        localScale.y = y;
        whipObj.transform.localScale = localScale;
    }

    private int CurrentDirection()
    {
        int direction = 1;
        if ((bool)transform.parent)
        {
            direction = (int)Mathf.Sign(transform.parent.transform.lossyScale.z);
        }
        if (direction == 0)
        {
            direction = Global.LastDirection;
        }
        if (direction == 0)
        {
            direction = 1;
        }
        return direction;
    }

    private int CurrentSkillIndex()
    {
        try
        {
            return Convert.ToInt32(Global.Var["Skill"]);
        }
        catch (Exception)
        {
            return -1;
        }
    }

    private void NormalizeWhipVisuals(GameObject obj)
    {
        LineRenderer[] lines = obj.GetComponentsInChildren<LineRenderer>(true);
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].enabled = true;
            lines[i].SetColors(Color.white, Color.white);
            NormalizeMaterialWhite(lines[i].material, true);
        }

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);
        for (int j = 0; j < renderers.Length; j++)
        {
            if (IsStrikeWaveVisual(renderers[j].transform))
            {
                renderers[j].enabled = true;
                NormalizeRendererWhite(renderers[j], true);
            }
        }

        Animator[] animators = obj.GetComponentsInChildren<Animator>(true);
        for (int k = 0; k < animators.Length; k++)
        {
            if (IsStrikeWaveVisual(animators[k].transform))
            {
                animators[k].enabled = true;
            }
        }

        ParticleSystem[] existingParticles = obj.GetComponentsInChildren<ParticleSystem>(true);
        for (int l = 0; l < existingParticles.Length; l++)
        {
            ParticleSystem.MainModule main = existingParticles[l].main;
            main.startColor = new Color(1f, 1f, 1f, 0.55f);
        }
    }

    private bool IsStrikeWaveVisual(Transform target)
    {
        string name = target.name.ToLower();
        return name.Contains("strikefx") || name.Contains("wave") || name.Contains("waver") || name.Contains("fog") || name.Contains("light");
    }

    private void RestartWhipAnimators(GameObject obj)
    {
        Animator[] animators = obj.GetComponentsInChildren<Animator>(true);
        for (int i = 0; i < animators.Length; i++)
        {
            if ((bool)animators[i])
            {
                animators[i].Rebind();
                animators[i].Update(0f);
            }
        }
    }

    private void NormalizeRendererWhite(Renderer renderer, bool forceParticleShader)
    {
        Material[] materials = renderer.materials;
        for (int i = 0; i < materials.Length; i++)
        {
            NormalizeMaterialWhite(materials[i], forceParticleShader);
        }
    }

    private void NormalizeMaterialWhite(Material material, bool forceParticleShader)
    {
        if (!(bool)material)
        {
            return;
        }

        if (forceParticleShader)
        {
            Shader shader = Shader.Find("Particles/Additive");
            if (!(bool)shader)
            {
                shader = Shader.Find("Legacy Shaders/Particles/Additive");
            }
            if ((bool)shader)
            {
                material.shader = shader;
            }
        }

        Color baseColor = new Color(1f, 1f, 1f, 0.45f);
        Color emissionColor = new Color(0.65f, 0.65f, 0.65f, 1f);
        Color lifeColor = new Color(0.93f, 0.93f, 0.93f, 1f);

        if (material.HasProperty("_Color"))
        {
            material.SetColor("_Color", baseColor);
        }
        if (material.HasProperty("_TintColor"))
        {
            material.SetColor("_TintColor", baseColor);
        }
        if (material.HasProperty("_EmissionColor"))
        {
            material.SetColor("_EmissionColor", emissionColor);
        }
        if (material.HasProperty("_EmissionColorUI"))
        {
            material.SetColor("_EmissionColorUI", emissionColor);
        }
        if (material.HasProperty("_LifeColor"))
        {
            material.SetColor("_LifeColor", lifeColor);
        }
    }

    public virtual void Main()
    {
    }
}
