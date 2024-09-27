using UnityEngine;
public enum ToastLength
{
    /// <summary> �� 2.5�� </summary>
    Short,
    /// <summary> �� 4�� </summary>
    Long
};
public class ToastExample : MonoBehaviour
{

    public static ToastExample instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
#if UNITY_ANDROID

    private AndroidJavaClass _unityPlayer;
    private AndroidJavaObject _unityActivity;
    private AndroidJavaClass _toastClass;

    private void Start()
    {
        _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        _unityActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        _toastClass = new AndroidJavaClass("android.widget.Toast");
    }
#endif

    /// <summary> �ȵ���̵� �佺Ʈ �޽��� ǥ���ϱ� </summary>
    [System.Diagnostics.Conditional("UNITY_ANDROID")]
    public void ShowToastMessage(string message, ToastLength length = ToastLength.Short)
    {
#if UNITY_ANDROID
        if (_unityActivity != null)
        {
            _unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = _toastClass.CallStatic<AndroidJavaObject>("makeText", _unityActivity, message, (int)length);
                toastObject.Call("show");
            }));
        }
#endif
    }
}
