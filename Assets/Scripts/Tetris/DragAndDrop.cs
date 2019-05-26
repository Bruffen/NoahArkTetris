using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mob;

public class DragAndDrop : MonoBehaviour
{
    private AnimalWrapper wrapper;
    private AIMovement movement;
    private bool selected = false;
    public bool onBoat = false;
    public GameObject objBoat;

    public static DragAndDrop dnd;

    void Awake()
    {
        wrapper = this.GetComponent<AnimalWrapper>();
        movement = this.GetComponent<AIMovement>();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("peepo");
            selected = true;
            wrapper.Toogle();
            dnd = this;
            movement.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && this == dnd)
        {
            Debug.Log("peepo");
            selected = true;
            wrapper.Toogle();
            dnd = null;
            movement.enabled = true;
        }
    }

    private void Update()
    {
        if (selected)
        {
            Vector2 cursosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = cursosPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            selected = false;

            if (onBoat)
            {
                Inventory inventory = objBoat.GetComponent<Inventory>();
                inventory.AddItem(Camera.main.WorldToScreenPoint(Input.mousePosition), this.gameObject.transform);
                Destroy(this.gameObject);
            }

            dnd = null;

        }
    }
}
