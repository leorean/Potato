using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallController : MonoBehaviour {

    List<SpriteRenderer> children;
    //public Color color;

    // Use this for initialization
    void Start () {

        children = new List<SpriteRenderer>();
        foreach (var c in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            children.Add(c);
            //c.GetComponent<SpriteRenderer>().color = color;
        }
        children.Sort((p, q) => p.name.CompareTo(q.name));        
	}
	
	// Update is called once per frame
	void Update () {

        Quaternion q = transform.rotation;

        children[0].transform.rotation = q;
        children[1].transform.rotation = q;
        children[2].transform.rotation = Quaternion.Euler(0, 0, 0);
        children[3].transform.rotation = Quaternion.Euler(0, 0, 0);
        children[4].transform.rotation = Quaternion.Euler(0, 0, 0);
        children[5].transform.rotation = q;        
        /*int layer = 1;
        foreach (var c in children)
        {
            //c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, c.transform.position.z - .1f);            
            if (layer == 2 || layer == 2 || layer == 3)
            {
                c.transform.rotation = new Quaternion(
                    c.transform.rotation.x,
                    c.transform.rotation.y,
                    0,
                    c.transform.rotation.w);
            }
            else
                c.transform.rotation = q;

            layer++;
        }*/
    }
}
