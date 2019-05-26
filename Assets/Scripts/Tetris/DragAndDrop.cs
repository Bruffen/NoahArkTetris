using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private AnimalWrapper wrapper;
    private bool selected = false;
    public bool onBoat = false;
    public GameObject objBoat;

    void Awake()
    {
        wrapper = this.GetComponent<AnimalWrapper>();        
    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("peepo");
            selected = true;
            wrapper.Toogle();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("peepo");
            selected = true;
            wrapper.Toogle();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*if(onBoat)
        {
            Vector2 mousePos = eventData.position;
            Inventory inventory = objBoat.GetComponent<Inventory>();

            inventory.AddItem(eventData, this.gameObject.transform);

        }*/
    }

    private void Update()
    {
        if(selected)
        {
            Vector2 cursosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = cursosPos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            selected = false;

            if (onBoat)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Inventory inventory = objBoat.GetComponent<Inventory>();

                inventory.AddItem(Input.mousePosition, this.gameObject.transform);

            }
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
