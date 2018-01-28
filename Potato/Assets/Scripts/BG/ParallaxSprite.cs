using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class ParallaxSprite : MonoBehaviour {


	private float tileSize;
	private Vector3 startPosition;
	private Vector2 current;

	// Use this for initialization
	public void Init (float tileSize) {
		startPosition = transform.localPosition;
		this.tileSize = tileSize;
		if (tileSize == 0)
			tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		current = Vector2.zero;
	}
	
	// Update is called once per frame
	public void Move (Vector2 velocity) {
		current += velocity;
		float newX = Mathf.Repeat(current.x, tileSize);
        float newY = current.y;
        transform.localPosition = startPosition + new Vector3 (newX, newY, 0);
	}
}
