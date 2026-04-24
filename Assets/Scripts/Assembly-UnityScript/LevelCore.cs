using System;
using UnityEngine;
using UnityScript.Lang;
using UnityStandardAssets.ImageEffects;

[Serializable]
public class LevelCore : MonoBehaviour
{
	public bool NoWEATHER;

	public bool fixedCamera;

	public Color StartColorz;

	public float SmoothCams;

	public float distance;

	public float height;

	public Vector3 startPosition;

	public bool HUD_Toggle;

	[NonSerialized]
	public static bool HUDToggle;

	[NonSerialized]
	public static float DeadZone;

	public bool ONLY_PLAYER_COLL;

	private Transform myTransform;

	public bool SaveWithExitToMainMenu;

	public Rect bounds;

	[NonSerialized]
	public static Vector4 Border;

	public float fallOutBuffer;

	public float colliderThickness;

	private Color sceneViewDisplayColor;

	public string DefaultDeadLevel;

	public bool FirstCamCorrection;

	private GameObject HUDBAR;

	private bool HUDBARact;

	private SunShafts sunShafts;

	private int DAYTIME;

	public Transform Sun;

	public Transform Moon;

	public LevelCore()
	{
		StartColorz = new Color(0.25f, 0.5f, 1f, 1f);
		SmoothCams = 7f;
		distance = 10f;
		HUD_Toggle = true;
		fallOutBuffer = 1f;
		colliderThickness = 10f;
		sceneViewDisplayColor = new Color(0.2f, 0.74f, 0.27f, 0.5f);
		HUDBARact = true;
		DAYTIME = -1;
	}

	public virtual void Awake()
	{
		if (Convert.ToInt32(Global.Var["hard"]) > 0)
		{
			Global.Var["survive"] = 1;
		}
		Cursor.visible = true;
		Global.DontEscTimer = 0;
		Global.CatchTimer = 0;
		FaceTalk.Face1 = null;
		FaceTalk.Face2 = null;
		MonoBehaviour.print("Awake CORE");
		Global.Boomerang = false;
		Global.SurfaceWaterPoint = 0f;
		Slot.DontSaveSlots = false;
		SlotItem.EscapeItem = false;
		Global.DemoMove = 0;
		Global.ShowWEATHER = !NoWEATHER;
		if (!NoWEATHER)
		{
			Global.StartColor = StartColorz;
			RenderSettings.fog = true;
			RenderSettings.fogColor = StartColorz;
		}
		for (int i = 0; i < Extensions.get_length((System.Array)Global.ToolsCount); i++)
		{
			if (Global.ToolsCount[i] < 1)
			{
				Global.ToolsCount[i] = 1;
			}
		}
		Border = new Vector4(bounds.x, bounds.y, bounds.x + bounds.width, bounds.y + bounds.height);
		Global.SaveWithExitToMainMenu = SaveWithExitToMainMenu;
		Global.RoomNPCNumber = 0;
		Global.LevelRANG = Global.RANG;
		if (Global.DAYMINUTES >= 2000)
		{
			Global.DAYMINUTES = 0;
			Global.ChangeTime(Global.DAYTIME + 1, true);
		}
		else
		{
			Global.ChangeTime(Global.DAYTIME, false);
		}
		Global.LevelEnemy = 0;
		Global.LevelGold = 0;
		Global.LevelTime = 0;
		Global.PrioritetMapSlot = null;
		Global.Prioritet = 0;
		Global.QUAKE = 0;
		Global.FACTOR = 0f;
		Global.CAM_Y = 0f;
		Global.WakeUpTime = 0;
		Global.BlockControl = 20;
		Global.HUD_ON = HUD_Toggle;
		HUDToggle = HUD_Toggle;
		myTransform = transform;
		startPosition = myTransform.position;
		GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
	}

	public virtual void Start()
	{
		Global.DeadLevel = null;
		Global.DeadSpecial = null;
		Global.BlockEscape = false;
		HUDBarCreate();
		if (Global.GenerateBounds)
		{
			Global.GenerateBounds = false;
			bounds = new Rect(-3f, 0f, Global.LEVELMASS.x * 10f + 6f, Global.LEVELMASS.y * 10f + 5f);
		}
		GameObject gameObject = new GameObject("Created Boundaries");
		gameObject.tag = "Barrier";
		GameObject gameObject2 = new GameObject("Left Boundary");
		gameObject2.transform.parent = gameObject.transform;
		BoxCollider boxCollider = (BoxCollider)gameObject2.AddComponent(typeof(BoxCollider));
		boxCollider.size = new Vector3(colliderThickness, bounds.height + colliderThickness * 2f + fallOutBuffer, colliderThickness);
		boxCollider.center = new Vector3(bounds.xMin - colliderThickness * 0.5f, bounds.y + bounds.height * 0.5f - fallOutBuffer * 0.5f, 0f);
		gameObject2.layer = 1;
		if (ONLY_PLAYER_COLL)
		{
			gameObject2.layer = 19;
		}
		GameObject gameObject3 = new GameObject("Right Boundary");
		gameObject3.transform.parent = gameObject.transform;
		boxCollider = (BoxCollider)gameObject3.AddComponent(typeof(BoxCollider));
		boxCollider.size = new Vector3(colliderThickness, bounds.height + colliderThickness * 2f + fallOutBuffer, colliderThickness);
		boxCollider.center = new Vector3(bounds.xMax + colliderThickness * 0.5f, bounds.y + bounds.height * 0.5f - fallOutBuffer * 0.5f, 0f);
		gameObject3.layer = 1;
		if (ONLY_PLAYER_COLL)
		{
			gameObject3.layer = 19;
		}
		GameObject gameObject4 = new GameObject("Top Boundary");
		gameObject4.transform.parent = gameObject.transform;
		boxCollider = (BoxCollider)gameObject4.AddComponent(typeof(BoxCollider));
		boxCollider.size = new Vector3(bounds.width + colliderThickness * 2f, colliderThickness, colliderThickness);
		boxCollider.center = new Vector3(bounds.x + bounds.width * 0.5f, bounds.yMax + colliderThickness * 0.5f, 0f);
		gameObject4.layer = 1;
		if (ONLY_PLAYER_COLL)
		{
			gameObject4.layer = 19;
		}
		GameObject gameObject5 = new GameObject("Bottom Boundary (Including Fallout Buffer)");
		gameObject5.transform.parent = gameObject.transform;
		gameObject5.tag = "Danger";
		boxCollider = (BoxCollider)gameObject5.AddComponent(typeof(BoxCollider));
		boxCollider.size = new Vector3(bounds.width + colliderThickness * 2f, colliderThickness, colliderThickness);
		boxCollider.center = new Vector3(bounds.x + bounds.width * 0.5f, bounds.yMin - colliderThickness * 0.5f - fallOutBuffer, 0f);
		DeadZone = boxCollider.center.y;
		gameObject5.layer = 1;
		if (ONLY_PLAYER_COLL)
		{
			gameObject5.layer = 19;
		}
		if (!string.IsNullOrEmpty(DefaultDeadLevel) && DefaultDeadLevel != string.Empty)
		{
			Global.DeadLevel = DefaultDeadLevel;
		}
	}

	public virtual void Update()
	{
		Transform transform = ResolveCameraTarget();
		if (!FirstCamCorrection && (bool)transform)
		{
			FirstCamCorrection = true;
			myTransform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z - distance);
		}
		if (Global.Pause)
		{
			UpdateCam();
		}
	}

	public virtual void FixedUpdate()
	{
		SunShaft();
		UpdateCam();
	}

	public virtual void UpdateCam()
	{
		Transform transform2 = ResolveCameraTarget();
		if (fixedCamera)
		{
			transform.position = startPosition;
		}
		else if ((bool)transform2)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, new Vector3(transform2.position.x, transform2.position.y + height, transform2.position.z - distance), Time.deltaTime * SmoothCams);
			Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(128f, 128f, 0.125f));
			Vector3 zero = Vector3.zero;
			zero.x = Mathf.Min(bounds.xMax - vector.x, 0f);
			zero.y = Mathf.Min(bounds.yMax - vector.y, 0f);
			myTransform.position += zero;
			Vector3 vector2 = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(-128f, -128f, 0.125f));
			zero.x = Mathf.Max(bounds.xMin - vector2.x, 0f);
			zero.y = Mathf.Max(bounds.yMin - vector2.y, 0f);
			myTransform.position += zero;
			if (!Global.Pause && Global.QUAKE > 0)
			{
				float y = myTransform.position.y + Global.CAM_Y;
				Vector3 position = myTransform.position;
				float num = (position.y = y);
				Vector3 vector3 = (myTransform.position = position);
			}
		}
	}

	private Transform ResolveCameraTarget()
	{
		if ((bool)Looker.target)
		{
			return Looker.target;
		}
		if ((bool)Global.CurrentPlayerObject)
		{
			Looker.target = Global.CurrentPlayerObject;
			return Looker.target;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			Looker.target = gameObject.transform;
		}
		return Looker.target;
	}

	public virtual void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 1f, 1f);
		Vector3 vector = new Vector3(bounds.xMin, bounds.yMax, 0f);
		Vector3 vector2 = new Vector3(bounds.xMin, bounds.yMin, 0f);
		Vector3 vector3 = new Vector3(bounds.xMax, bounds.yMax, 0f);
		Vector3 vector4 = new Vector3(bounds.xMax, bounds.yMin, 0f);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector2, vector4);
		Gizmos.DrawLine(vector4, vector3);
		Gizmos.DrawLine(vector3, vector);
	}

	public virtual void HUDBarCreate()
	{
		HUDBAR = UnityEngine.Object.Instantiate(LoadData.HUD("HUD Bar"));
		HUDBAR.transform.parent = transform;
		if ((bool)HUDBAR)
		{
			Camera mainCamera = GetComponent<Camera>();
			if (!(bool)mainCamera)
			{
				mainCamera = Camera.main;
			}
			if (!(bool)mainCamera)
			{
				return;
			}
			ConfigureHudRendering(HUDBAR, mainCamera);
			Vector3 position = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 1f));
			HUDBAR.transform.position = position;
		}
	}

	private void ConfigureHudRendering(GameObject hud, Camera mainCamera)
	{
		if (!(bool)hud || !(bool)mainCamera)
		{
			return;
		}

		int hudLayer = 31;
		int hudMask = (1 << 5) | (1 << hudLayer);
		SetLayerRecursively(hud, hudLayer);
		mainCamera.cullingMask &= ~hudMask;
		EnsureHudOverlayCamera(mainCamera, hudMask);
	}

	private void EnsureHudOverlayCamera(Camera mainCamera, int hudMask)
	{
		Transform child = mainCamera.transform.Find("HUD Overlay Camera");
		Camera hudCamera = null;
		if ((bool)child)
		{
			hudCamera = child.GetComponent<Camera>();
		}
		if (!(bool)hudCamera)
		{
			GameObject cameraObject = new GameObject("HUD Overlay Camera");
			cameraObject.transform.parent = mainCamera.transform;
			cameraObject.transform.localPosition = Vector3.zero;
			cameraObject.transform.localRotation = Quaternion.identity;
			cameraObject.transform.localScale = Vector3.one;
			hudCamera = cameraObject.AddComponent<Camera>();
		}

		hudCamera.CopyFrom(mainCamera);
		hudCamera.clearFlags = CameraClearFlags.Depth;
		hudCamera.cullingMask = hudMask;
		hudCamera.depth = mainCamera.depth + 1f;
		hudCamera.useOcclusionCulling = false;
		hudCamera.enabled = mainCamera.enabled;
	}

	private void SetLayerRecursively(GameObject obj, int layer)
	{
		if (!(bool)obj)
		{
			return;
		}

		obj.layer = layer;
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			SetLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
		}
	}

	public virtual void SunShaft()
	{
		if (!NoWEATHER && (bool)Sun && (bool)Moon && Global.DAYTIME != DAYTIME)
		{
			DAYTIME = Global.DAYTIME;
			if (!sunShafts)
			{
				sunShafts = GetComponent<SunShafts>();
			}
			if (DAYTIME <= 4 || DAYTIME >= 15)
			{
				sunShafts.sunTransform = Moon;
			}
			else
			{
				sunShafts.sunTransform = Sun;
			}
		}
	}

	public virtual void SetHeight(float h)
	{
		height = h;
	}

	public virtual void Main()
	{
	}
}
