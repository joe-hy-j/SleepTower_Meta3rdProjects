using UnityEngine;

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
    public void ShowToast(string message)
    {
#if UNITY_ANDROID
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject toast = new AndroidJavaObject("android.widget.Toast");
            AndroidJavaObject toastMessage = toast.CallStatic<AndroidJavaObject>("makeText", activity, message, 0);
            toastMessage.Call("show");
        }
#endif
    }

}
