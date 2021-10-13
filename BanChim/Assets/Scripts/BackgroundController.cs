using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : Singleton<BackgroundController>
{
    public Sprite[] sprites;
    public SpriteRenderer bgImage;

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ChangeSprites()
    {
        if(bgImage!=null&&sprites != null && sprites.Length > 0)
        {
            int randomIdx = Random.Range(0, sprites.Length);
            if (sprites[randomIdx] != null)
            {
                bgImage.sprite = sprites[randomIdx];
            }
        }
    }
    public override void Start()
    {
        ChangeSprites();
    }
    // Start is called before the first frame update

}
