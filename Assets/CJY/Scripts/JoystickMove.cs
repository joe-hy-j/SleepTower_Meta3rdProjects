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
        //RectTransform ������ ��ũ������Ʈ�� ��������Ʈ�� ��ȯ�� �ִ� �޼����Դϴ�.
        //�巡�� ���� ��ũ�� ��ǥ ���� ���� RectTransform�� �巡�� ���� ��ǥ������ ��ȯ ��
        //Vector2 ���� localVector������ ��ȯ�մϴ�. ���� �巡�׵Ǿ� ���� �߻��Ѵٸ� true�̹Ƿ�
        //�Ʒ� ����� �����մϴ�.

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.gameObject.GetComponent<RectTransform>(), eventData.position,
            eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < this.handleRange)
            {
                this.handle.transform.localPosition = localVector;
            }
            else   //localVector ���� handleRange���� ũ�Ŵ� ���ٸ� ���� handleRange�� �� ��ŭ �����մϴ�.
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
        //�����Ϳ��� Ŭ���� �������� input���� handle�� ��ġ�� 0, 0���� �ʱ�ȭ�մϴ�.

        this.SetJoystickColor(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.OnDrag(eventData);
        //�����Ͱ� Ŭ���Ǹ� OnDrag �޼��尡 ����˴ϴ�.
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
