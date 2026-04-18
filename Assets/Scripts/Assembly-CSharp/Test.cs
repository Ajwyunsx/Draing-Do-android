using UnityEngine;

public class Test : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer sr;

	[SerializeField]
	private tk2dSpriteCollectionData scd;

	[SerializeField]
	private int spriteIndex;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		if (sr == null)
		{
			sr = base.gameObject.AddComponent<SpriteRenderer>();
		}
	}

	private void OnValidate()
	{
		if (!(scd == null) && scd.IsValidSpriteId(spriteIndex))
		{
			Debug.Log("valid");
			tk2dSpriteDefinition tk2dSpriteDefinition2 = scd.spriteDefinitions[spriteIndex];
			Texture texture = scd.textures[0];
			Sprite sprite = Sprite.Create(texture as Texture2D, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
		}
	}
}
