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

    private void OnMouseEnter() {
        Debug.Log("enter");
    }
    private void OnMouseExit() {
        Debug.Log("left");
    }

    private void OnMouseDown()
    {
        Debug.Log("this");
        if (Input.GetMouseButtonDown(0))
        {
            CameraZoom.Instance.enabled = false;
            this.wrapper.tetrisObject.transform.localScale = Vector3.one * CameraZoom.Instance.tetrisScaleValue;
            AudioManager.Instance.Play("Drag");
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
            CameraZoom.Instance.enabled = true;
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
            AudioManager.Instance.Play("Drop");
            selected = false;

            if (onBoat)
            {
                Inventory inventory = objBoat.GetComponent<Inventory>();
                inventory.AddItem(Input.mousePosition, this.gameObject.transform);
                Destroy(this.gameObject);
            }

            dnd = null;

        }
    }
}
