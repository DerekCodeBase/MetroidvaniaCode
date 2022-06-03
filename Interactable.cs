using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour

{
public bool talks;
public string message;
public bool inventory;
public bool openable;
public bool locked;
public string itemType;

public GameObject itemNeeded;

public Animator anim;

public void DoInteraction()
{
    gameObject.SetActive (false);
}

public void Open()
{
    anim.SetBool ("open", true);
}

public void Talk()
{
    Debug.Log (message);
}

}