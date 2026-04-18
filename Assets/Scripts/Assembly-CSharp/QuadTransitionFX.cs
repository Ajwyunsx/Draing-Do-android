using UnityEngine;

public class QuadTransitionFX
{
	private GameObject _go;

	private Renderer _renderer;

	public Vector3 position;

	private MaterialPropertyBlock _mpb;

	protected float FadeInDuration;

	protected float FadeOffsetDuration;

	protected float FadeOutDuration;

	protected float FadeTransitionDuration;

	protected float LifeTime;

	private Vector4 _rampUv;

	private float _rampUwidth;

	private float _rampUmin;

	private float _rampVheight;

	private float _rampVmin;

	private Color _currentRampColor;

	private static int _RampUV_ID = Shader.PropertyToID("_RampUV");

	private static int _LifeColor_ID = Shader.PropertyToID("_LifeColor");

	public QuadTransitionFX(GameObject proto, Vector3 pos)
	{
		_go = Object.Instantiate(proto);
		_renderer = _go.GetComponent<Renderer>();
		position = pos;
		_go.transform.position = position;
		_mpb = new MaterialPropertyBlock();
		_rampUmin = 0f;
		_rampUwidth = 1f;
		_rampVmin = 0f;
		_rampVheight = 1f;
		FadeInDuration = 0.4f;
		FadeOffsetDuration = 0f;
		FadeOutDuration = 1f;
		FadeTransitionDuration = 0.2f;
		LifeTime = Random.value * (FadeInDuration + FadeOutDuration + FadeOffsetDuration);
		_currentRampColor = new Color(0f, 1f, 1f, 1f);
		updateLife();
	}

	private void updateLife()
	{
		float num = FadeInDuration + FadeOffsetDuration;
		float r = ((!(LifeTime > FadeInDuration)) ? (LifeTime / FadeInDuration) : 1f);
		float g = ((!(LifeTime < num)) ? ((LifeTime - num) / FadeOutDuration) : 0f);
		float num2 = FadeInDuration + FadeOffsetDuration * 0.5f;
		float b = Mathf.Clamp((LifeTime - num2) / FadeTransitionDuration, 0f, 1f);
		_currentRampColor.r = r;
		_currentRampColor.g = g;
		_currentRampColor.b = b;
		_rampUv = new Vector4(_rampUmin, _rampUwidth, _rampVmin, _rampVheight);
		_mpb.Clear();
		_mpb.SetVector(_RampUV_ID, _rampUv);
		_mpb.SetVector(_LifeColor_ID, _currentRampColor);
		_renderer.SetPropertyBlock(_mpb);
	}

	public void Update()
	{
		LifeTime += Time.deltaTime;
		if (LifeTime > FadeInDuration + FadeOutDuration + FadeOffsetDuration)
		{
			LifeTime = 0f;
		}
		updateLife();
	}
}
