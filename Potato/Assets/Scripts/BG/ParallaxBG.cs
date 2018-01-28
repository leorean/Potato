using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour {

	public Vector2 totalSize; // 480x240 levelcam+

	public Material spriteMaterial; // for pixel snapping
	public Sprite[] sprites; // layer sprites
	public Vector2[] speeds; // layer speeds
	public Vector2[] shifts; // [0,1] offset parallax by percentage
	public float[] margins; // distance between tiles in same layer

	private ParallaxLayer[] layers;

    private Vector3 oldPosition;

	void Start() {
		if (sprites.Length > speeds.Length) {
			var tmp = new Vector2[sprites.Length];
			speeds.CopyTo (tmp, 0);
			speeds = tmp;
		}


		if (sprites.Length > shifts.Length) {
			var tmp = new Vector2[sprites.Length];
			shifts.CopyTo (tmp, 0);
			shifts = tmp;
		}


		if (sprites.Length > margins.Length) {
			var tmp = new float[sprites.Length];
			margins.CopyTo (tmp, 0);
			margins = tmp;
		}

		layers = new ParallaxLayer[sprites.Length];

		for (int i = 0; i < sprites.Length; i++) {
			var s = sprites [i];
			GameObject go = new GameObject ("Parallax Layer " + i);
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.back * i;

			var l = go.AddComponent<ParallaxLayer> ();
			l.Init (s, speeds[i], spriteMaterial, totalSize, shifts[i], margins[i]);

			layers [i] = l;
		}

        oldPosition = GetPosition();
	}

    Vector3 GetPosition()
    {
        return GetComponentInParent<Cinemachine.CinemachineVirtualCamera>().transform.position;
    }

    public void Update()
    {
        var diff = oldPosition - GetPosition();
        Move(diff);

        oldPosition = GetPosition();
    }

    public void Move(Vector2 velocity) {
		foreach (ParallaxLayer p in layers)
			p.Move (velocity);
	}
}