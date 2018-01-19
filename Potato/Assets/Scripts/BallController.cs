using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    SpriteRenderer[] children;
    
    // Use this for initialization
    void Start () {
        children = gameObject.GetComponentsInChildren<SpriteRenderer>();

        int layer = 0;
        foreach (var c in children)
        {
            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, c.transform.position.z - .1f);
            if (layer == 0 || layer == 1 || layer == 2)
            {
                c.transform.rotation = new Quaternion(
                    c.transform.rotation.x,
                    c.transform.rotation.y,
                    0,
                    c.transform.rotation.w);
            }
            layer++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
