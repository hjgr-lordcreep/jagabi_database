using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.Networking;
using UnityEngine.UI;

public class JoinMembership : MonoBehaviour
{

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private InputField checkpasswordInput;
    [SerializeField] private InputField emailInput;

    private string username = null;
    private string password = null;
    private string checkpassword = null;
    private string email = null;


    private const string loginUri = "http://127.0.0.1/login.php";

    private void Awake()
    {
        username = usernameInput.GetComponent<InputField>().text;
        password = passwordInput.GetComponent<InputField>().text;
        checkpassword = checkpasswordInput.GetComponent<InputField>().text;
        email = emailInput.GetComponent<InputField>().text;
    }

    private void Start()
    {
        Connect();
    }

    private IEnumerator LoginCoroutine(
        string username, string password, string checkpassword, string email)
    {

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginCheckPass", checkpassword);
        form.AddField("loginEmail", email);

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
            }
        }
    }

    public void Connect()
    {

        StartCoroutine(LoginCoroutine(username, password, checkpassword, email));


    }
}
