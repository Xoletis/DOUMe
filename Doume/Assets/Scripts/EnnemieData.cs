using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ennemie", menuName = "My Game/Ennemie")]
public class EnnemieData : ScriptableObject
{
    [Tooltip("Nom de l'ennemie")]
    public string nom;
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
}
