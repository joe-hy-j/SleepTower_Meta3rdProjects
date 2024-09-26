using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputOutputFileManager : MonoBehaviour
{
    public string path;

    [SerializeField]
    string content;

    static InputOutputFileManager instance;

    public static InputOutputFileManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            go.AddComponent<InputOutputFileManager>();
        }
        return instance;
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void SetPath(string path)
    {
        this.path = path;
    }

    private void Start()
    {
        ReadFile();
    }
    public string ReadFile()
    {
        if (File.Exists(path))
        {
            // 파일 내용을 읽어오기
            content = File.ReadAllText(path);
            Debug.Log(content); // 콘솔에 내용 출력
            return content;
        }
        else
        {
            Debug.LogError("파일이 존재하지 않습니다: " + path);
            return "";
        }
    }

    public void WriteFile(string s)
    {
        if (File.Exists(path))
        {
            // 파일 내용을 읽어오기
            File.WriteAllText(path, s);
            Debug.Log(s); // 콘솔에 내용 출력
        }
        else
        {
            Debug.LogError("파일이 존재하지 않습니다: " + path);
        }
    }

}
