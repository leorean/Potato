using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackgroundController : MonoBehaviour {

    public Sprite spriteBG;
    public Sprite spriteFG;
    SpriteRenderer spriteRendererBG;
    SpriteRenderer spriteRendererFG;
        
    // Use this for initialization
    void Awake () {

        //var spriteRenderer = new SpriteRenderer();
        //spriteRenderer.sprite = spriteBG;
        //spriteRenderers.Add(spriteRenderer);

        spriteRendererBG = GetComponentsInChildren<SpriteRenderer>().Where(x => x.name == "BG1").First();
        spriteRendererFG = GetComponentsInChildren<SpriteRenderer>().Where(x => x.name == "BG2").First();

        spriteRendererBG.sprite = spriteBG;
        spriteRendererFG.sprite = spriteFG;


        /*{
            new SpriteRenderer
            {
                sprite = sprites.ElementAt()
            }
            //sprites.Add()
            //bg1sprite = Resources.Load("BG_0", typeof(Sprite)) as Sprite;
            //bg2sprite = Resources.Load("BG_1", typeof(Sprite)) as Sprite;

            GetComponentsInChildren<SpriteRenderer>().Where(x => x.name == "BG1").First(),
            GetComponentsInChildren<SpriteRenderer>().Where(x => x.name == "BG2").First()
        };*/

        foreach (var sr in new List<SpriteRenderer> { spriteRendererBG, spriteRendererFG })
        {
            sr.transform.localScale = new Vector3(1, 1, 1);



            var width = sr.sprite.textureRect.width;
            var height = sr.sprite.textureRect.height;
            var sy = Camera.main.rect.height * Camera.main.orthographicSize * .5f;
            var sx = sy / Camera.main.rect.width;
            

            sr.transform.localScale = new Vector3(sx, sy);

            /*
            var cam = GetComponentInParent(typeof(Cinemachine.CinemachineVirtualCamera)) as Cinemachine.CinemachineVirtualCamera;
            
            var worldScreenHeight = cam.m_Lens.OrthographicSize * 1.0;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            sr.transform.localScale = new Vector3(
                (float)worldScreenWidth / width,
                (float)worldScreenHeight / height);
            */
        }
    }

    // Update is called once per frame
    void Update () {


        var mod = Camera.main.ScreenToWorldPoint(spriteRendererFG.sprite.rect.size).x;

        float newX = (transform.position.x *.5f);

        spriteRendererFG.transform.position = new Vector3(newX, spriteRendererFG.transform.position.y);
	}
}


/*
 * function ResizeSpriteToScreen() {
     var sr = GetComponent(SpriteRenderer);
     if (sr == null) return;
     
     transform.localScale = Vector3(1,1,1);
     
     var width = sr.sprite.bounds.size.x;
     var height = sr.sprite.bounds.size.y;
     
     var worldScreenHeight = Camera.main.orthographicSize * 2.0;
     var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
     
     transform.localScale.x = worldScreenWidth / width;
     transform.localScale.y = worldScreenHeight / height;
 }
 */
