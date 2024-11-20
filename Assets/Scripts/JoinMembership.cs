using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinMembership : MonoBehaviour
{

    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField checkpasswordInput;
    [SerializeField] private TMP_InputField emailInput;

    [SerializeField] private GameObject popUp = null;
    [SerializeField] private TextMeshProUGUI popUpText1 = null;
    [SerializeField] private TextMeshProUGUI popUpText2 = null;
    [SerializeField] private TextMeshProUGUI popUpText3 = null;

    [SerializeField] private Button JoinButton;
    [SerializeField] private Button CheckButton;


    //public Color wrongColor = Color.red;

    //private string username = null;
    //private string password = null;
    //private string checkpassword = null;
    //private string email = null;

    private TextMeshProUGUI game;

    private const string loginUri = "http://127.0.0.1/joinmembership.php";

    private void Awake()
    {
        JoinButton.interactable = false;
        popUpText1.enabled = false;
        popUpText2.enabled = false;
        popUpText3.enabled = false;
    }

    private void Start()
    {
        CheckButton.onClick.AddListener(() =>
        {
            
            StartCoroutine(JoinCoroutine(usernameInput.text, emailInput.text, passwordInput.text, checkpasswordInput.text));

        });

        JoinButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Jagabee3");
        });

    }

    //private void Update()
    //{
    //    pwCheck();
    //}

    private IEnumerator JoinCoroutine(
        string username,  string email,string password, string checkpassword)
    {

    

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginEmail", email);
        form.AddField("loginPass", password);
        form.AddField("loginCheckPass", checkpassword);

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
                string wu = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (wu == "1") // 아이디 이이 존재합니다
                {
                    Debug.Log("아이디 이미 존재 합니다");
                    
                    popUp.SetActive(true);
                    popUpText1.enabled = true;

                }
                else if (wu == "2") // 메일 이미 존재 합니다
                {
                    Debug.Log("메일 이미 존재 합니다");
                    popUp.SetActive(true);
                    popUpText2.enabled = true;
                }
                else if(wu == "3") // 비번 불일치
                {
                    Debug.Log("비번이 불일치입니다");

                    popUp.SetActive(true);
                    popUpText3.enabled = true;
                }
                else //성공할 때 -> 회원가입
                {
                    JoinButton.interactable = true;

                }

            }
        }
    }



    //private void pwCheck()
    //{
    //    if(passwordInput.text != checkpasswordInput.text)
    //    {
    //        checkpasswordInput.textComponent.color = wrongColor;
    //        //= Image.sprite.Color.red;
    //        popup[0].SetActive(true);
    //        JoinButton.interactable = false;
    //    }
    //    else
    //    {
    //        JoinButton.interactable = true;
    //    }
    //}
}
