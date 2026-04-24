using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class KillAfterTime : MonoBehaviour
{
	public bool randomAngle;

	public int time;

	public int timeRandom;

	public bool isThunder;

	private int oldTime;

	public AudioClip[] SFXBoom;

	public KillAfterTime()
	{
		time = 10;
	}

	public virtual void Start()
	{
		if (randomAngle)
		{
			float z = transform.eulerAngles.z + (float)UnityEngine.Random.Range(-90, 90);
			Vector3 eulerAngles = transform.eulerAngles;
			float num = (eulerAngles.z = z);
			Vector3 vector = (transform.eulerAngles = eulerAngles);
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				float x = transform.localScale.x * -1f;
				Vector3 localScale = transform.localScale;
				float num2 = (localScale.x = x);
				Vector3 vector3 = (transform.localScale = localScale);
			}
		}
		BuildLegacyHitParticleIfNeeded();
		oldTime = time;
		time += UnityEngine.Random.Range(0, timeRandom);
	}

	public virtual void FixedUpdate()
	{
		time--;
		if (time > 0)
		{
			return;
		}
		if (!isThunder)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		else if (UnityEngine.Random.Range(0, 100) < 30)
		{
			int num = UnityEngine.Random.Range(-180, 180);
			Vector3 eulerAngles = transform.eulerAngles;
			float num2 = (eulerAngles.z = num);
			Vector3 vector = (transform.eulerAngles = eulerAngles);
			transform.position += new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), 0f);
			time = oldTime + UnityEngine.Random.Range(0, timeRandom);
		}
		else
		{
			if (Extensions.get_length((System.Array)SFXBoom) > 0)
			{
				Global.CreateSFX(SFXBoom[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)SFXBoom))], transform.position, UnityEngine.Random.Range(0.85f, 1.1f), UnityEngine.Random.Range(0.8f, 1f));
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}

	private void BuildLegacyHitParticleIfNeeded()
	{
		if (transform.parent != null)
		{
			return;
		}

		string lowerName = gameObject.name.ToLower();
		if (!lowerName.Contains("darkplasm") && !lowerName.Contains("ectoplasm"))
		{
			return;
		}

		Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
		Material sourceMaterial = FindFirstSharedMaterial(renderers);
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}

		Animator[] animators = GetComponentsInChildren<Animator>(true);
		for (int j = 0; j < animators.Length; j++)
		{
			animators[j].enabled = false;
		}

		ParticleSystem particles = GetComponent<ParticleSystem>();
		if (!(bool)particles)
		{
			particles = gameObject.AddComponent<ParticleSystem>();
		}

		ParticleSystem.MainModule main = particles.main;
		main.duration = 0.12f;
		main.loop = false;
		main.playOnAwake = true;
		main.simulationSpace = ParticleSystemSimulationSpace.World;
		main.maxParticles = 56;
		main.startLifetime = new ParticleSystem.MinMaxCurve(0.38f, 0.85f);
		main.startSpeed = new ParticleSystem.MinMaxCurve(2.2f, 5.4f);
		main.startSize = new ParticleSystem.MinMaxCurve(0.1f, 0.45f);
		main.startRotation = new ParticleSystem.MinMaxCurve(0f, 6.2831855f);
		main.startColor = new Color(0.75f, 0.86f, 1f, 0.62f);

		ParticleSystem.EmissionModule emission = particles.emission;
		emission.rateOverTime = 320f;

		ParticleSystem.ShapeModule shape = particles.shape;
		shape.shapeType = ParticleSystemShapeType.Sphere;
		shape.radius = 0.04f;

		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = particles.velocityOverLifetime;
		velocityOverLifetime.enabled = true;
		velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(1.2f, 2.5f);

		ParticleSystem.ForceOverLifetimeModule forceOverLifetime = particles.forceOverLifetime;
		forceOverLifetime.enabled = true;
		forceOverLifetime.y = new ParticleSystem.MinMaxCurve(-4.8f);

		ParticleSystemRenderer particleRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
		if ((bool)particleRenderer)
		{
			particleRenderer.enabled = true;
			particleRenderer.material = BuildParticleMaterial(sourceMaterial);
			particleRenderer.renderMode = ParticleSystemRenderMode.Billboard;
			particleRenderer.sortingOrder = 20;
		}
		particles.Play(true);
	}

	private Material FindFirstSharedMaterial(Renderer[] renderers)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			if ((bool)renderers[i].sharedMaterial)
			{
				return renderers[i].sharedMaterial;
			}
		}
		return null;
	}

	private Material BuildParticleMaterial(Material sourceMaterial)
	{
		Material result;
		if ((bool)sourceMaterial)
		{
			result = new Material(sourceMaterial);
		}
		else
		{
			Shader fallbackShader = Shader.Find("Particles/Additive");
			if (!(bool)fallbackShader)
			{
				fallbackShader = Shader.Find("Legacy Shaders/Particles/Additive");
			}
			if (!(bool)fallbackShader)
			{
				fallbackShader = Shader.Find("Diffuse");
			}
			result = new Material(fallbackShader);
		}

		Shader shader = Shader.Find("Particles/Additive");
		if (!(bool)shader)
		{
			shader = Shader.Find("Legacy Shaders/Particles/Additive");
		}
		if ((bool)shader)
		{
			result.shader = shader;
		}
		if (result.HasProperty("_Color"))
		{
			result.SetColor("_Color", new Color(1f, 1f, 1f, 0.65f));
		}
		if (result.HasProperty("_TintColor"))
		{
			result.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
		}
		return result;
	}
}
