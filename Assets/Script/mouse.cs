using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public Camera myCam;
    private LayerMask _layerMask = 6; // >> 8; //This sets the object to ignore layer 8 which is a custom layer;

    void GetMouseInfo()
    {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            if (hit.collider.transform.name == name)//This detects the colliders transform if you just use hit.transform it gets the root parent at least thats what I found.
            {
                Debug.Log("Mouse is over " + name + ".");
             }
        }

    }

    void Update()
    {
        GetMouseInfo();
    }

}
