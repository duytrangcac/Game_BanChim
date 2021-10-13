using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fireRate;
    float currentFireRate;
    bool isShorted;
    public GameObject viewFinder;
    GameObject viewFinderClone;
    public void Awake()
    {
        currentFireRate = fireRate;
    }
    private void Start()
    {
        if (viewFinder)
        {
            viewFinderClone= Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
            
        }
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)&&!isShorted)
        {


            shot(mousePos);
        }
        if (isShorted)
        {
            currentFireRate -= Time.deltaTime;
            if (currentFireRate <= 0)
            {
                isShorted = false; 
                currentFireRate = fireRate;
            }

            GameGUIManager.Ins.UpdateFireRate(currentFireRate / fireRate);
        }
        if (viewFinderClone)
        {
            viewFinderClone.transform.position = new Vector3(mousePos.x,mousePos.y,0f);
        }
    }
    void shot(Vector3 mousePos)
    {
        isShorted = true;
        Vector3 shootDir = Camera.main.transform.position - mousePos;
        shootDir.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);
        if (hits != null && hits.Length>0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider &&(Vector3.Distance((Vector2)hit.collider.transform.position,(Vector2)mousePos)<=0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();
                    if (bird)
                    {
                        bird.Die();
                    }
                }
            }
        }
        AudioController.Ins.playSound(AudioController.Ins.shooting);

    }
}
