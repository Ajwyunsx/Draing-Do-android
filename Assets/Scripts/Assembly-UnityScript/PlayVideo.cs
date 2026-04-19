using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public class PlayVideo : MonoBehaviour
{
	private Renderer cachedRenderer;

	private Material runtimeMaterial;

	private VideoPlayer videoPlayer;

	private Texture fallbackTexture;

	private GameObject backdropObject;

	private bool closeRequested;

	private bool prepared;

	private bool playbackStarted;

	private string videoName = "Daring Do Animation Test 1";

	private Joystick jumpTouchPad;

	private Joystick actionTouchPad;

	private Joystick rollTouchPad;

	private bool oldJumpTouchState;

	private bool oldActionTouchState;

	private bool oldRollTouchState;

	private void CheckTouchPads()
	{
		if (!Global.MobilePlatform)
		{
			return;
		}
		MobileInputBridge.EnsureDefaultControls();
		if (!(bool)jumpTouchPad)
		{
			jumpTouchPad = MobileInputBridge.FindJoystick("JumpJoystick");
		}
		if (!(bool)actionTouchPad)
		{
			actionTouchPad = MobileInputBridge.FindJoystick("ActionJoystick");
		}
		if (!(bool)rollTouchPad)
		{
			rollTouchPad = MobileInputBridge.FindJoystick("RollJoystick");
		}
	}

	private bool TouchAdvanceDown()
	{
		CheckTouchPads();
		bool flag = MobileInputBridge.IsTouchDown(actionTouchPad, ref oldActionTouchState);
		bool flag2 = MobileInputBridge.IsTouchDown(jumpTouchPad, ref oldJumpTouchState);
		bool flag3 = MobileInputBridge.IsTouchDown(rollTouchPad, ref oldRollTouchState);
		return flag || flag2 || flag3;
	}

	public virtual void Awake()
	{
		cachedRenderer = GetComponent<Renderer>();
		if ((bool)cachedRenderer)
		{
			runtimeMaterial = cachedRenderer.material;
		}
		fallbackTexture = LoadData.TEX("blackTexture") ?? Texture2D.blackTexture;
		if ((bool)runtimeMaterial && (bool)fallbackTexture)
		{
			runtimeMaterial.mainTexture = fallbackTexture;
		}
		CheckTouchPads();
		CreateBackdrop();
		StartVideoPlayback();
	}

	public virtual void Update()
	{
		bool flag = TouchAdvanceDown();
		if (!closeRequested && prepared && playbackStarted && !(videoPlayer == null) && !videoPlayer.isPlaying)
		{
			closeRequested = true;
		}
		if (Input.GetButtonDown("Escape") || Input.GetMouseButtonDown(1) || flag)
		{
			closeRequested = true;
		}
		if (closeRequested)
		{
			CloseVideo();
		}
	}

	public virtual void Main()
	{
	}

	private void StartVideoPlayback()
	{
		videoPlayer = GetComponent<VideoPlayer>();
		if (!(videoPlayer == null))
		{
			UnityEngine.Object.Destroy(videoPlayer);
		}
		videoPlayer = gameObject.AddComponent<VideoPlayer>();
		videoPlayer.playOnAwake = false;
		videoPlayer.waitForFirstFrame = true;
		videoPlayer.skipOnDrop = true;
		videoPlayer.isLooping = false;
		videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
		videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
		videoPlayer.targetMaterialProperty = "_MainTex";
		videoPlayer.targetMaterialRenderer = cachedRenderer;
		videoPlayer.prepareCompleted += HandlePrepared;
		videoPlayer.started += HandleStarted;
		videoPlayer.loopPointReached += HandleLoopPointReached;
		videoPlayer.errorReceived += HandleVideoError;
		string[] array = new string[6]
		{
			Path.Combine(Application.streamingAssetsPath, videoName + ".mp4"),
			Path.Combine(Application.dataPath, "StreamingAssets", videoName + ".mp4"),
			Path.Combine(Application.dataPath, "MovieTexture", videoName + ".mp4"),
			Path.Combine(Application.streamingAssetsPath, videoName + ".ogv"),
			Path.Combine(Application.dataPath, "StreamingAssets", videoName + ".ogv"),
			Path.Combine(Application.dataPath, "MovieTexture", videoName + ".ogv")
		};
		int i = 0;
		while (i < array.Length)
		{
			string text = array[i];
			if (File.Exists(text))
			{
				videoPlayer.source = VideoSource.Url;
				videoPlayer.url = new Uri(text).AbsoluteUri;
				videoPlayer.Prepare();
				return;
			}
			i++;
		}
		VideoClip videoClip = Resources.Load<VideoClip>("Videos/" + videoName);
		if ((bool)videoClip)
		{
			videoPlayer.source = VideoSource.VideoClip;
			videoPlayer.clip = videoClip;
			videoPlayer.Prepare();
			return;
		}
		closeRequested = true;
	}

	private void CreateBackdrop()
	{
		Transform transform2 = transform.Find("Black");
		if ((bool)transform2)
		{
			transform2.gameObject.SetActive(false);
		}
		backdropObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		if ((bool)backdropObject.GetComponent<Collider>())
		{
			UnityEngine.Object.Destroy(backdropObject.GetComponent<Collider>());
		}
		backdropObject.name = "__VideoBackdrop";
		backdropObject.layer = gameObject.layer;
		backdropObject.transform.SetParent(transform, false);
		backdropObject.transform.localPosition = new Vector3(0f, 0f, 0.25f);
		backdropObject.transform.localRotation = Quaternion.identity;
		backdropObject.transform.localScale = new Vector3(40f, 40f, 1f);
		Renderer component = backdropObject.GetComponent<Renderer>();
		if (!(component == null))
		{
			Shader shader = Shader.Find("Unlit/Color");
			if (!(shader == null))
			{
				component.material = new Material(shader);
				component.material.color = Color.black;
			}
			else
			{
				component.material.color = Color.black;
			}
			component.sortingOrder = -1000;
			component.receiveShadows = false;
			component.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		}
	}

	private void HandlePrepared(VideoPlayer source)
	{
		prepared = true;
		if (!(source == null))
		{
			source.Play();
		}
	}

	private void HandleStarted(VideoPlayer source)
	{
		playbackStarted = true;
	}

	private void HandleLoopPointReached(VideoPlayer source)
	{
		closeRequested = true;
	}

	private void HandleVideoError(VideoPlayer source, string message)
	{
		MonoBehaviour.print("PlayVideo error: " + message);
		closeRequested = true;
	}

	private void CloseVideo()
	{
		closeRequested = false;
		prepared = false;
		playbackStarted = false;
		if (!(videoPlayer == null))
		{
			videoPlayer.Stop();
		}
		Time.timeScale = 1f;
		Global.Pause = false;
		Global.BlockEscape = false;
		Global.HUD_ON = LevelCore.HUDToggle;
		UnityEngine.Object.Destroy(gameObject);
	}
}
