using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {

	public Vector2 speed;
	private ParallaxSprite[] tiles;

	//l.Init (s, speeds[i], spriteMaterial, totalSize);
	public void Init (Sprite sprite, Vector2 speed, Material mat, Vector2 totalSize, Vector2 shift, float margin)
	{
		this.speed = speed;
		transform.localPosition -= sprite.bounds.extents;

		float tileSize = sprite.bounds.size.x + margin;
		float offsetX = Mathf.Clamp01 (shift.x);
        float offsetY = Mathf.Clamp01(shift.y);

        int n = (int)Mathf.Ceil(totalSize.x / tileSize) + 1;



		tiles = new ParallaxSprite[n];
		for (int i = 0; i < n; ++i) {
			GameObject go = new GameObject ("Parallax Tile " + i);
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.right * (i - 1) * tileSize + Vector3.up * offsetY;
			var sr = go.AddComponent<SpriteRenderer> ();
			sr.sprite = sprite;
			sr.sortingLayerName = "BGParallax";
			sr.material = mat;
			var sp = go.AddComponent<ParallaxSprite> ();
			sp.Init (tileSize);
			sp.Move (new Vector2 (offsetX * tileSize, offsetY * tileSize));
			tiles [i] = sp;
		}
	}
    
    public void Move(Vector2 velocity) {
		foreach (ParallaxSprite tile in tiles) {
			tile.Move (new Vector2(velocity.x * speed.x, velocity.y * speed.y));
		}
	}
}
