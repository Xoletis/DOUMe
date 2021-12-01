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
    [Tooltip("L'ennemie est un ennemie à distance")]
    public bool isRangeEnnemie;
    public int droopRate = 70;

    public GameObject bullet; 
}

//Modifier l'insepcteur pour cahcer les données non utiles
[CustomEditor(typeof(EnnemieData))]
public class MyScriptEditor : Editor
{

    public override void OnInspectorGUI()
    {
        var ennemieData = target as EnnemieData;

        serializedObject.Update();
        ennemieData.health = EditorGUILayout.FloatField("Health : ", ennemieData.health);
        ennemieData.damage = EditorGUILayout.FloatField("Damage : ", ennemieData.damage);
        ennemieData.speed = EditorGUILayout.FloatField("Speed : ", ennemieData.speed);
        ennemieData.viewArea = EditorGUILayout.FloatField("Zone to view Player : ", ennemieData.viewArea);
        ennemieData.attackCouldown = EditorGUILayout.FloatField("Time enter attack in s : ", ennemieData.attackCouldown);
        ennemieData.attackRange = EditorGUILayout.FloatField("Distance to Attack : ", ennemieData.attackRange);
        ennemieData.droopRate = EditorGUILayout.IntField("Chance to drop item (%) : ", ennemieData.droopRate);
        ennemieData.isRangeEnnemie = GUILayout.Toggle(ennemieData.isRangeEnnemie, " : IsRangeEnnemie");

        if (ennemieData.isRangeEnnemie)
        {
            ennemieData.bullet = (GameObject)EditorGUILayout.ObjectField("Bullet : ", ennemieData.bullet, typeof(GameObject), false);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
