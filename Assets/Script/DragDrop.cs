using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas; //Canvas sur lequel les objet sont déplacés
    
    public GameObject magePrefab;   //Prefab du mage à invoquer

    public int cost;    //Coût du mage à invoquer

    private RectTransform rectTransform;    //Transform pour l'objet actuel

    private Vector2 pos;    //Position de départ de l'objet actuel

    private GameObject gameManager; //GameManager

    private void Awake(){   //Méthode appelée à la création de l'objet
        rectTransform = GetComponent<RectTransform>();  //Initialisation du Trandform
        pos = rectTransform.anchoredPosition;   //Initialisation de la position
        gameManager = GameObject.FindGameObjectWithTag("GameManager");  //Initialisation du GameManager
    }
    public void OnPointerDown(PointerEventData eventData){} //Méthode non utilisée pour le moment

    public void OnDrag(PointerEventData eventData){ //Pendant le déplacement
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //Déplacement de l'objet
    }

    public void OnBeginDrag(PointerEventData eventData){}   //Méthode non utilisée pour le moment

    public void OnEndDrag(PointerEventData eventData)   //Méthode appelée à la fin du déplacement
    {
        GameManager gm = gameManager.GetComponent<GameManager>();   //Récupération de la partie script de GameManager
        GameObject cursor = GameObject.FindGameObjectWithTag("Cursor"); //Récupération de l'objet 'Cursor'
        if(cursor.GetComponent<Renderer>().enabled == true){    //Si le carréVert est affiché (si la souris est situé au dessus d'un emplacement à construire)
            if(gm.affordable(cost)){    //Si le montant stocké est supérieur à 'cost'
                gm.spend(cost); //Dépenser le montant: 'cost'
                GameObject mage = Instantiate(magePrefab, (Vector2)cursor.transform.position, Quaternion.Euler(0, 0, 0));   //Créer un mange à la position de la node
                cursor.GetComponent<carrevert>().GetNode().GetComponent<node>().SetMage(mage);  //Associer le mage à la node sur laquelle il est
            }else{
                //message not enough gold
            }
        }else{
            //message not a valid spot
        }
        rectTransform.anchoredPosition = pos;   //Remettre l'objet à sa position initiale
    }

}
