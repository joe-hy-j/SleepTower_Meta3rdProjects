using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickMove : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform frame;  // 조이스틱 테두리
    public RectTransform handle;  // 조이스틱 핸들

    float handleRange = 130;  // 핸들의 범위
    Vector3 input;  // 입력 값

    public float Horizontal { get { return input.x; } }  // 좌우이동
    public float Vertical { get { return input.y; } }  // 상하이동

    public void OnDrag(PointerEventData eventData)
    {
        // RectTransform 내부의 스크린포인트를 로컬포인트로 변환해 주는 메서드
        // 드래그 중인 스크린 좌표 값을 현재 RectTransform의 드래그 중인 좌표값으로 변환 후
        // Vector2 값을 localVector변수에 반환, 만약 드래그되어 값이 발생한다면 true이므로
        // 아래 기능을 실행

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gameObject.GetComponent<RectTransform>(), eventData.position,
            eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < handleRange)
            {
                handle.transform.localPosition = localVector;
            }
            else   //localVector 값이 handleRange보다 크거다 같다면 값을 handleRange의 값 만큼 고정
            {
                handle.transform.localPosition = localVector.normalized * handleRange;
            }

            input = localVector;
        }

        SetJoystickColor(true);  // 지정된 색으로 변경
    }

    //포인터에서 클릭이 떼어지면 input값과 handle의 위치를 0, 0으로 초기화
    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        SetJoystickColor(false);  // 기본 색으로 변경
    }

    //포인터가 클릭되면 OnDrag 메서드가 실행
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // 포인터 클릭 여부에 따른 조이스틱 색상 변경
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

        frame.gameObject.GetComponent<Image>().color = pointedFrameColor;
        handle.gameObject.GetComponent<Image>().color = pointedHandleColor;
    }
}
