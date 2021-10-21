using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtCamera : MonoBehaviour
{
    [Tooltip("Cam�ra du joueur")]
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Vector3 lookVector = player.transform.position - transform.position;    //Vecteur qui renvoie la direction de la cam�ra
        Quaternion rot = Quaternion.LookRotation(lookVector);                   //Application de la rotation sur une variable pour faire face � la cam�ra
        rot *= Quaternion.Euler(90, 0, 0);                                      //Application d'une rotation de 90� pour avoir la plane en face
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);      //Application finale de la rotation qui permet de faire face � la cam�ra
    }
}