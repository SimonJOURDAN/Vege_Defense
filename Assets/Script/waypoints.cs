using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake(){   //méthode appelée à la création de l'objet
        points = new Transform[transform.childCount];   //Création de la liste de points
        for(int i = 0; i <points.Length; i++){  //Pour chaque point
            points[i] = transform.GetChild(i);  //Initialisation de la liste de points
        }
    }

}
