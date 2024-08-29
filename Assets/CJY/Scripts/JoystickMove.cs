using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickMove : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform frame;  // ���̽�ƽ �׵θ�
    public RectTransform handle;  // ���̽�ƽ �ڵ�

    float handleRange = 130;  // �ڵ��� ����
    Vector3 input;  // �Է� ��

    public float Horizontal { get { return input.x; } }  // �¿��̵�
    public float Vertical { get { return input.y; } }  // �����̵�

    public void OnDrag(PointerEventData eventData)
    {
        // RectTransform ������ ��ũ������Ʈ�� ��������Ʈ�� ��ȯ�� �ִ� �޼���
        // �巡�� ���� ��ũ�� ��ǥ ���� ���� RectTransform�� �巡�� ���� ��ǥ������ ��ȯ ��
        // Vector2 ���� localVector������ ��ȯ, ���� �巡�׵Ǿ� ���� �߻��Ѵٸ� true�̹Ƿ�
        // �Ʒ� ����� ����

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gameObject.GetComponent<RectTransform>(), eventData.position,
            eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < handleRange)
            {
                handle.transform.localPosition = localVector;
            }
            else   //localVector ���� handleRange���� ũ�Ŵ� ���ٸ� ���� handleRange�� �� ��ŭ ����
            {
                handle.transform.localPosition = localVector.normalized * handleRange;
            }

            input = localVector;
        }

        SetJoystickColor(true);  // ������ ������ ����
    }

    //�����Ϳ��� Ŭ���� �������� input���� handle�� ��ġ�� 0, 0���� �ʱ�ȭ
    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        SetJoystickColor(false);  // �⺻ ������ ����
    }

    //�����Ͱ� Ŭ���Ǹ� OnDrag �޼��尡 ����
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // ������ Ŭ�� ���ο� ���� ���̽�ƽ ���� ����
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
