using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickMove : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform frame;
    public RectTransform handle;

    private float handleRange = 130;
    private Vector3 input;

    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    public void OnDrag(PointerEventData eventData)
    {
        //RectTransform 내부의 스크린포인트를 로컬포인트로 변환해 주는 메서드입니다.
        //드래그 중인 스크린 좌표 값을 현재 RectTransform의 드래그 중인 좌표값으로 변환 후
        //Vector2 값을 localVector변수에 반환합니다. 만약 드래그되어 값이 발생한다면 true이므로
        //아래 기능을 실행합니다.

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.gameObject.GetComponent<RectTransform>(), eventData.position,
            eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < this.handleRange)
            {
                this.handle.transform.localPosition = localVector;
            }
            else   //localVector 값이 handleRange보다 크거다 같다면 값을 handleRange의 값 만큼 고정합니다.
            {
                this.handle.transform.localPosition = localVector.normalized * this.handleRange;
            }

            this.input = localVector;
        }

        this.SetJoystickColor(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.input = Vector2.zero;
        this.handle.anchoredPosition = Vector2.zero;
        //포인터에서 클릭이 떼어지면 input값과 handle의 위치를 0, 0으로 초기화합니다.

        this.SetJoystickColor(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.OnDrag(eventData);
        //포인터가 클릭되면 OnDrag 메서드가 실행됩니다.
    }

    private void SetJoystickColor(bool isOnDraged)
    {
        Color pointedFrameColor;
        Color pointedHandleColor;

        if (isOnDraged)
        {
            pointedFrameColor = new Color(255, 0, 0, 0.5f);
            pointedHandleColor = new Color(255, 0, 0, 0.6f);
        }
        else
        {
            pointedFrameColor = new Color(255, 255, 255, 0.5f);
            pointedHandleColor = new Color(255, 255, 255, 0.6f);
        }

        this.frame.gameObject.GetComponent<Image>().color = pointedFrameColor;
        this.handle.gameObject.GetComponent<Image>().color = pointedHandleColor;
    }
}
