using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image joysticks;
    private Image joystick;
    private Vector2 inputVector; 

    // Start is called before the first frame update
    void Start()
    {
        joystick = GetComponent<Image>();
        
        joysticks = GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick.rectTransform,ped.position,ped.pressEventCamera,out pos))
        {
            pos.x = (pos.x / joystick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystick.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joysticks.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystick.rectTransform.sizeDelta.x / 2), inputVector.y * (joystick.rectTransform.sizeDelta.y / 2));


        }
    }

    public float Horizontal()
    {
        if(inputVector.x != 0)
        {
            return inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if(inputVector.y != 0)
        {
            return inputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
