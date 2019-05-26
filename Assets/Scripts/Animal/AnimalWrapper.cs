using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWrapper : MonoBehaviour
{
    public GameObject boatObject;
    public GameObject tetrisObject;
    private Animator boatAnimator;
    public Animal animal;
    private bool isTetris;

    public void Init(Animal animal){
        this.animal = animal;
        boatAnimator = boatObject.GetComponent<Animator>();

        boatAnimator.runtimeAnimatorController = animal.boatAnimator;
        tetrisObject.GetComponent<SpriteRenderer>().sprite = animal.tetrisSprite;

        isTetris = false;
    }

    public void Toogle(){
        isTetris = !isTetris;
        if(isTetris){
            //AudioManager.Instance.Play("");
            boatObject.SetActive(false);
            tetrisObject.SetActive(true);
        }
        else{
            boatObject.SetActive(true);
            tetrisObject.SetActive(false);
        }
    }
}
