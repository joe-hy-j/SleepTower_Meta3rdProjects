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
            // ���� ������ �о����
            content = File.ReadAllText(path);
            Debug.Log(content); // �ֿܼ� ���� ���
            return content;
        }
        else
        {
            Debug.LogError("������ �������� �ʽ��ϴ�: " + path);
            return "";
        }
    }

    public void WriteFile(string s)
    {
        if (File.Exists(path))
        {
            // ���� ������ �о����
            File.WriteAllText(path, s);
            Debug.Log(s); // �ֿܼ� ���� ���
        }
        else
        {
            Debug.LogError("������ �������� �ʽ��ϴ�: " + path);
        }
    }

}
