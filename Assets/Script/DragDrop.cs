using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;
    
    public GameObject magePrefab;

    public int cost;

    private RectTransform rectTransform;

    private Vector2 pos;

    private GameObject gameManager;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        pos = rectTransform.anchoredPosition;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    public void OnPointerDown(PointerEventData eventData){
        
    }

    public void OnDrag(PointerEventData eventData){
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager gm = gameManager.GetComponent<GameManager>();
        GameObject cursor = GameObject.FindGameObjectWithTag("Cursor");
        if(cursor.GetComponent<Renderer>().enabled == true){
            if(gm.affordable(cost)){
                gm.spend(cost);
                GameObject mage = Instantiate(magePrefab, (Vector2)cursor.transform.position, Quaternion.Euler(0, 0, 0));
                cursor.GetComponent<carrevert>().GetNode().GetComponent<node>().SetMage(mage);
            }else{
                //message not enough gold
            }
        }else{
            //message not a valid spot
        }
        rectTransform.anchoredPosition = pos;
    }

}
