using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class inutile pour le moment
public class mouse : MonoBehaviour
{
    public Camera myCam; //Caméra utilisée par la suite pour déterminer la position de la souris par rapport aux autres objets
    private LayerMask _layerMask = 6;

    void GetMouseInfo() //Méthode permettant de connaitre la position de la souris
    {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition); //Rayon (demi-droite) partant de la caméra vers la souris 
        RaycastHit hit; //Point "d'impact" du rayon

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask)) //Si le rayon atteint un autre objet
        {
            Debug.Log("Mouse is over " + hit.collider.transform.name + "."); //Traitement (pour l'instant on se contente d'afficher le nom de l'objet)
        }

    }

    void Update() //Méthode appelée une fois par frame
    {
        GetMouseInfo();
    }

}
