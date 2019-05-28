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
    
    private Transform boatParent;

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
            boatParent = this.transform.parent;
            this.transform.parent = null;
            this.transform.rotation = Quaternion.identity;
            boatObject.SetActive(false);
            tetrisObject.SetActive(true);
        }
        else{
            boatObject.SetActive(true);
            tetrisObject.SetActive(false);
            this.transform.parent = boatParent;
            this.transform.rotation = Quaternion.identity;
        }
    }
}
