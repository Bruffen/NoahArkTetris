using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{

    public Image timer;

    public float startTime;
    public float currentTime;

    public bool timeOver = false;

    Inventory inventory;

    void Start()
    {
        inventory = this.transform.GetChild(0).GetComponent<Inventory>();
        currentTime = startTime;
        timer.fillAmount = currentTime / startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeOver)
        {
            currentTime -= Time.deltaTime;
            timer.fillAmount = currentTime / startTime;

            if (currentTime <= 0)
                timeOver = true;
        }
        else
        {
            foreach (Animal a in inventory.animals)
                GameManager.Instance.AddScore(a.scoreValue);
            BoatSpawner.activeBoatNum--;
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
