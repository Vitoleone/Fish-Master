using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
}
