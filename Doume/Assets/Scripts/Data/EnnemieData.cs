using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Ennemie", menuName = "My Game/Ennemie")]
public class EnnemieData : ScriptableObject
{
    [Tooltip("Vie de l'ennemie")]
    public float health;
    [Tooltip("Degat de l'ennemie")]
    public float damage;
    [Tooltip("Vitesse de l'ennemie")]
    public float speed;
    [Tooltip("Distance à laquelle l'ennemie prend le joueur en chasse")]
    public float viewArea;
    [Tooltip("Distance à laquelle l'ennemie attaque le joueur")]
    public float attackRange;
    [Tooltip("Temps entre deux attaques de l'ennemie")]
    public float attackCouldown;
    public AudioClip natuarlSound;
    public AudioClip attackSound;
    [Tooltip("L'ennemie est un ennemie à distance")]
    public bool isRangeEnnemie;

    [Tooltip("Taux de spawn des ennemies vis à vis du niveaux")]
    public spawnByLevel[] spawnByLevels;

    public int droopRate = 70;

    public GameObject bullet; 
}


[System.Serializable]
public struct spawnByLevel
{
    public int level;
    public float spawnCount;
}
