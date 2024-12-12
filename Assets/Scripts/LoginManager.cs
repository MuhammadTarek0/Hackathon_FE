
    using UnityEngine;
    using System.Collections;
    using TMPro;
    using UnityEngine.Networking;

    public class LoginManager : MonoBehaviour
    {

        [SerializeField] private TMP_InputField usernameField;
        [SerializeField] private TMP_InputField passwordField;

        [SerializeField] private string apiUrl;

        private Coroutine loginCoroutine;

        public void Login()
        {
            loginCoroutine = StartCoroutine(TryLogin());
        }

        private IEnumerator TryLogin()
        {
            WWWForm form = new WWWForm();
            form.AddField("username", usernameField.text);
            form.AddField("password", passwordField.text);

            using UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
            
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login successful: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error: {www.error}");
            }
        }
    }
