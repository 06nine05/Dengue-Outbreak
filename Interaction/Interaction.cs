using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Button interactionButton;

    private PlayerInteract interact;
    private Button attackButton;
    private bool isTrigger;

    // Start is called before the first frame update
    void Start()
    {
        interact = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        attackButton = GameObject.FindGameObjectWithTag("ButAttack").GetComponent<Button>();
        isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackButton.gameObject.SetActive(false);
            interactionButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && interact.IsInteract && !isTrigger)
        {
            StartCoroutine(Destroy());
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackButton.gameObject.SetActive(true);
            interactionButton.gameObject.SetActive(false);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        interactionButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}