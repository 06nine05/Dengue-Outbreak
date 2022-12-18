using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private PlayerInteract interaction;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickButtonAttack()
    {
        _playerController.Attack();
        attack.ButtonAttack();
    } 

    public void ClickButtonInteract()
    {
        _playerController.Interact();
    }
}
