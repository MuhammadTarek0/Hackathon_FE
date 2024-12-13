
    using UnityEngine;
    using System.Collections;
    using TMPro;
    using UnityEngine.Networking;
    using System.Text;

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

            // Send request
            yield return www.SendWebRequest();

            // Handle response
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login successful: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error: {www.error}");
            }
        }
        // Helper class for JSON payload
        [System.Serializable]
        private class LoginData
        {
            public string username;
            public string password;
        }
    }
