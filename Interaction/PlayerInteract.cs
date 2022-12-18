using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool IsInteract = false;

    public void ButtonInteract()
    {
        IsInteract = true;
        StartCoroutine(ResetInteractBool());
    }

    IEnumerator ResetInteractBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsInteract = false;
    }
}
