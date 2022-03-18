using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class Inicializador : MonoBehaviour
{
    public Lista lista;

    public Clase clase;

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=3&category=15"));

        // A non-existing page.

    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
            lista = JsonUtility.FromJson<Lista>(webRequest.downloadHandler.text);

            Debug.Log(lista.response_code);
            Debug.Log(lista.results[0].difficulty);
            Debug.Log(lista.results[0].category);
            Debug.Log(lista.results[0].type);
            Debug.Log(lista.results[0].question);
        }
    }
}
