using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGrab : MonoBehaviour
{


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                if (hit.rigidbody != null && hit.collider.tag == "Item")
                {
                    hit.collider.gameObject.GetComponent<DragAndDrop>().GetGrabbed();
                }
            }
        }
    }
}
