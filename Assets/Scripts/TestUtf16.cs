using System;
using System.Collections;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class TestUtf16 : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(LoadText());
        }

        IEnumerator LoadText()
        {
            var file = "file://" + Path.Combine(Application.streamingAssetsPath, "hero.txt");
#if UNITY_ANDROID
            file = "jar:file://" + Path.Combine(Application.streamingAssetsPath, "hero.txt");
#endif
            var request = UnityWebRequest.Get(file);
            yield return request.SendWebRequest();

            GetComponent<TextMeshProUGUI>().text = Encoding.Unicode.GetString(request.downloadHandler.data);

        }
        
    }
}