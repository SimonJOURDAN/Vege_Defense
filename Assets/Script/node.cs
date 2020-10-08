using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{

    private GameObject mage;

    void OnMouseEnter(){
        GameObject cursor = GameObject.FindGameObjectWithTag("Cursor");
        Transform cursorTransform = cursor.transform;
        cursorTransform.Translate(transform.position - cursorTransform.position);

        cursor.GetComponent<carrevert>().SetNode(this);

        GameObject magecursor = GameObject.FindGameObjectWithTag("Contour");
        Transform magecursorTransform = magecursor.transform;
        magecursorTransform.Translate(transform.position - magecursorTransform.position);
    }

    void OnMouseOver(){
        if (mage != null)
        {
            GameObject.FindGameObjectWithTag("Cursor").GetComponent<Renderer>().enabled = false;
            GameObject.FindGameObjectWithTag("Contour").GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Cursor").GetComponent<Renderer>().enabled = true;
            GameObject.FindGameObjectWithTag("Contour").GetComponent<Renderer>().enabled = false;
        }
    }

    void OnMouseExit(){
        GameObject.FindGameObjectWithTag("Cursor").GetComponent<Renderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Contour").GetComponent<Renderer>().enabled = false;
    }

    void OnMouseDown(){
        if(mage != null){
            Debug.Log("upgrade");
        }
    }

    public void SetMage(GameObject m){
        mage = m;
    }

    public GameObject GetMage(){
        return mage;
    }

}
