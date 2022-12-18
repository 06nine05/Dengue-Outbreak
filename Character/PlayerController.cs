using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour 
{
    public float moveSpeed;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource walkingAudioSource;

    private PlayerInteract interaction;
    private Vector3 moveVector;
    private CharacterController ch_controller;
    private Animator ch_animator;
    private JoystickController jController;
    private bool isMoving;
    private bool stopMoveSound;


    void Start()
    {
        stopMoveSound = false;
        interaction = GetComponent<PlayerInteract>();
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        jController = GameObject.FindGameObjectWithTag("Joystick").GetComponent<JoystickController>();
    }

    void Update()
    {
        if (isMoving)
        {
            if (!walkingAudioSource.isPlaying&&!stopMoveSound)
            {
                SoundManager.Instance.Play(walkingAudioSource, SoundManager.Sound.PlayerMove);
            }
        }

        else
        {
            SoundManager.Instance.StopPlaying(walkingAudioSource);
        }

        CharacterMove();
    }

    public void StopWalkSound()
    {
        stopMoveSound = true;
        SoundManager.Instance.StopPlaying(walkingAudioSource);
    }

    public void Attack()
    {
        //if(!ch_animator.GetBool("Move"))
        //{
            ch_animator.SetTrigger("Attack");

            SoundManager.Instance.Play(audioSource, SoundManager.Sound.PlayerAttack);
        //}
    }

    public void Interact()
    {
        if (!ch_animator.GetBool("Move"))
        {
            interaction.ButtonInteract();

            ch_animator.SetTrigger("Interact");

            //SoundManager.Instance.Play(audioSource, SoundManager.Sound.PlayerInteract);
        }
    }

    public void Die()
    {
        ch_animator.SetTrigger("Die");

        //SoundManager.Instance.Play(audioSource, SoundManager.Sound.PlayerDead);
    }

    private void CharacterMove()
    {
        moveVector = Vector3.zero;
        moveVector.x = jController.Horizontal() * moveSpeed;
        moveVector.z = jController.Vertical() * moveSpeed;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ch_animator.SetBool("Move", true);
            isMoving = true;
        }
        else
        {
            ch_animator.SetBool("Move", false);
            isMoving = false;
        }

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, moveSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        ch_controller.Move(moveVector * Time.deltaTime);

        if (!ch_controller.isGrounded) 
        {
            ch_controller.Move(new Vector3(0,-9.8f,0) * Time.deltaTime);
        }
    }

}
