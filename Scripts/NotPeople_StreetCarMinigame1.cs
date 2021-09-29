using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotPeople_StreetCarMinigame1 : MonoBehaviour
{
    public bool isOnStreetCar = false;
    public bool isMove = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BodyCar"))
        {
            isOnStreetCar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BodyCar"))
        {
            isOnStreetCar = false;
        }
    }
}
