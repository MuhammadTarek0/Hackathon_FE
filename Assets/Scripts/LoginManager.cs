
    using UnityEngine;
    using System.Collections;
    using TMPro;
    using UnityEngine.Networking;
    using System.Text;

    public class LoginManager : MonoBehaviour
    {
        [Header("Game Events")]
        [SerializeField] private GameEvent showLoadingGameEvent;
        [SerializeField] private GameEvent hideLoadingGameEvent;
        [Header("References")]
        [SerializeField] private TMP_InputField usernameField;
        [SerializeField] private TMP_InputField passwordField;
        [Header("API")]
        [SerializeField] private string apiUrl;

        private Coroutine loginCoroutine;

        public void Login()
        {
            if (loginCoroutine is not null) return;
            
            loginCoroutine = StartCoroutine(TryLogin());
        }

        private IEnumerator TryLogin()
        {
            // Create JSON payload
            string jsonPayload = JsonUtility.ToJson(new LoginData
            {
                username = usernameField.text,
                password = passwordField.text
            });

            // Create a UnityWebRequest with JSON payload
            using UnityWebRequest www = new UnityWebRequest(apiUrl + "token/", "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload)),
                downloadHandler = new DownloadHandlerBuffer()
            };

            // Set content type to application/json
            www.SetRequestHeader("Content-Type", "application/json");

            showLoadingGameEvent.Raise();
            // Send request
            yield return www.SendWebRequest();
            hideLoadingGameEvent.Raise();

            // Handle response
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login successful: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error: {www.error}");
            }

            loginCoroutine = null;
        }
    }
