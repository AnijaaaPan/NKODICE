using UnityEngine;
using System;
using System.Collections;

public class GlobalCoroutine : MonoBehaviour
{

    public static void Go(IEnumerator coroutine)
    {
        GameObject obj = new GameObject();     // �R���[�`�����s�p�I�u�W�F�N�g�쐬
        obj.name = "GlobalCoroutine";

        GlobalCoroutine component = obj.AddComponent<GlobalCoroutine>();
        if (component != null)
        {
            component.StartCoroutine(component.Do(coroutine));
        }
    }

    IEnumerator Do(IEnumerator src)
    {
        while (src.MoveNext())
        {               // �R���[�`���̏I����҂�
            yield return null;
        }

        Destroy(this.gameObject);              // �R���[�`�����s�p�I�u�W�F�N�g��j��
    }
}
