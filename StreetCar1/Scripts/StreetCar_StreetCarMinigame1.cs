using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetCar_StreetCarMinigame1 : MonoBehaviour
{
    public Vector2 prePos, currentPos;
    public Coroutine posCoroutine;
    public bool isPlayingCoroutine = false;

    private void Start()
    {
        currentPos = transform.position;
        prePos = currentPos;
        posCoroutine = StartCoroutine(UpdatePos());

    }

    IEnumerator UpdatePos()
    {
        while (!isPlayingCoroutine)
        {
            if (!GameController_StreetCarMinigame1.instance.isDonKhach)
            {
                currentPos = transform.position;
                yield return new WaitForSeconds(0.1f);
                prePos = currentPos;
                yield return new WaitForSeconds(0.01f);
                currentPos = transform.position;
            }
            else
                yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (!GameController_StreetCarMinigame1.instance.isDonKhach)
        {
            if (prePos.x != 0)
            {
                if (currentPos.x > prePos.x)
                {
                    transform.localScale = new Vector2(0.5f, 0.5f);
                }
                if (currentPos.x < prePos.x)
                {
                    transform.localScale = new Vector2(-0.5f, 0.5f);
                }
                if (currentPos.x == prePos.x)
                {
                    return;
                }
            }

        }
    }
}
