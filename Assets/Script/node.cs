using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{

    private GameObject mage;    //Mage lié à la node

    void OnMouseEnter(){    //Méthode appelée quand la souris arrive sur l'objet
        GameObject cursor = GameObject.FindGameObjectWithTag("Cursor"); //Récupération du carréVert
        Transform cursorTransform = cursor.transform;   //Récupération du transform du carréVert
        cursorTransform.Translate(transform.position - cursorTransform.position);   //Déplacement du carréVert

        cursor.GetComponent<carrevert>().SetNode(this); //Assignement de la node au carréVert

        GameObject magecursor = GameObject.FindGameObjectWithTag("Contour");    //Récupération du contour
        Transform magecursorTransform = magecursor.transform;   //Récupération du transform du contour
        magecursorTransform.Translate(transform.position - magecursorTransform.position);   //Déplacement du contour
    }

    void OnMouseOver(){ //Méthode appelée quand la souris survole l'objet
        if (mage != null)   //Si le mage est initialisé
        {
            GameObject.FindGameObjectWithTag("Cursor").GetComponent<Renderer>().enabled = false;    //Ne pas afficher le carréVert
            GameObject.FindGameObjectWithTag("Contour").GetComponent<Renderer>().enabled = true;    //Afficher le contour
        }
        else
        {
            GameObject.FindGameObjectWithTag("Cursor").GetComponent<Renderer>().enabled = true;     //Afficher le carréVert
            GameObject.FindGameObjectWithTag("Contour").GetComponent<Renderer>().enabled = false;   //Ne pas afficher le contour
        }
    }

    void OnMouseExit(){
        GameObject.FindGameObjectWithTag("Cursor").GetComponent<Renderer>().enabled = false;    //Ne pas afficher le carréVert 
        GameObject.FindGameObjectWithTag("Contour").GetComponent<Renderer>().enabled = false;   //Ne pas afficher le contour
    }

    void OnMouseDown(){ //Méthode appelée si l'utilisateur clique sur la souris
        if(mage != null){   //Si le mage est initialisé
            //Upgrade
        }
    }

    public void SetMage(GameObject m){  //Setter pour le mage
        mage = m;
    }

    public GameObject GetMage(){    //Getter pour le mage
        return mage;
    }

}
