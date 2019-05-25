using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private bool selected = false;
    public bool onBoat = false;
    public GameObject objBoat;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("peepo");
            selected = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(onBoat)
        {
            Vector2 mousePos = eventData.position;
            Inventory inventory = objBoat.GetComponent<Inventory>();

            inventory.AddItem(eventData, this.gameObject.transform);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boat")
        {
            objBoat = other.gameObject;
            onBoat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boat")
            onBoat = false;
    }
}
