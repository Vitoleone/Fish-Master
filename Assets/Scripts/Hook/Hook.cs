using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System;

public class Hook : MonoBehaviour
{
    public Transform hookedTransform;

    private Camera mainCam;
    private Collider2D myCollider;

    private int length;
    private int strength;
    private int fishCount;

    private bool canMove = true;

    //List<Fish>

    private Tweener cameraTween;

    private void Awake()
    {
        mainCam = Camera.main;
        myCollider = GetComponent<Collider2D>();
        //List<Fish>
    }

    
    void Update()
    {
        if (canMove && Input.GetMouseButton(0))
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 myPosition = transform.position;
            myPosition.x = mousePos.x;
            transform.position = myPosition;
        }
    }
    public void StartFishing()
    {
        length = -50;
        strength = 3;
        fishCount = 0;
        float time = (-length) * 0.1f;

        cameraTween = mainCam.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate
        {
            if(mainCam.transform.position.y <= -11)
            {
                transform.SetParent(mainCam.transform);
            }
        }).OnComplete(delegate
        {
            myCollider.enabled = true;
            cameraTween = mainCam.transform.DOMoveY(0, time * 5, false).OnUpdate(
                delegate
                {
                    if(mainCam.transform.position.y >= -25f)
                    {
                        StopFishing();
                    }
                });
        });

        myCollider.enabled = false;
        canMove = true;
        
    }

    void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCam.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if (mainCam.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 6;
            myCollider.enabled = true;
            int num = 0;
            //Clear the fishes thats we have.

        });
    }
}
