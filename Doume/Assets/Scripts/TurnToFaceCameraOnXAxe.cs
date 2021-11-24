using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToFaceCameraOnXAxe : MonoBehaviour
{
    [Tooltip("Caméra du joueur")]
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Vector3 lookVector = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);    //Vecteur qui renvoie la direction de la caméra
        Quaternion rot = Quaternion.LookRotation(lookVector);                   //Application de la rotation sur une variable pour faire face à la caméra
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);      //Application finale de la rotation qui permet de faire face à la caméra
    }
}
