using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    [SerializeField] private TMP_InputField emailInput = null;
    [SerializeField] private TMP_InputField passwordInput = null;

    [SerializeField] private Button LoginButton = null;
    [SerializeField] private Button JoinButton = null;

   
    //private string password = null;
    //private string email = null;


    private const string loginUri = "http://127.0.0.1/login.php";

    //private void Awake()
    //{
    //   //Button loginButton = GetComponent<Button>();
    //}

    private void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
            StartCoroutine(LoginCoroutine(emailInput.text, passwordInput.text));

        });
        JoinButton.onClick.AddListener(() =>
        {
            SceneChanged();
        });
        //email = emailInput.GetComponent<InputField>().text;
        //password = passwordInput.GetComponent<InputField>().text;
    }

    private IEnumerator LoginCoroutine(
        string email, string password)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("loginEmail", email);
        form.AddField("loginPass", password);

        // WWW
        // Sync, Async
        using (UnityWebRequest www =
            UnityWebRequest.Post(loginUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result ==
                UnityWebRequest.Result.ConnectionError ||
                www.result ==
                UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                SceneManager.LoadScene("Jagabee3");
            }
        }
    }

    private void SceneChanged()
    {
        SceneManager.LoadScene("Jagabee2");
    }


}
