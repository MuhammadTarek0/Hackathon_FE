
public class GameEventListenerSignupData : GameEventListenerT<SignupData>
{
    [SerializeField]
    private GameEventT<SignupData> gameEvent;

    [SerializeField]
    protected UnityEvent onGameEventRaised;

    private string apiUrl = "http://127.0.0.1:8000/";


    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public override void OnGameEventRaised(SignupData value)
    {
        if (string.IsNullOrEmpty(value.username) || string.IsNullOrEmpty(value.email))
        {
            Debug.LogError("Invalid signup data received.");
            return;
        }

        // Call base method to ensure listeners are updated
        base.OnGameEventRaised(value);

        // Create welcome message text
        usernameText.text = $"Welcome, {value.username}!";

        // Send the signup data to the backend API
        SendSignupDataToBackend(value);
    }

    private void SendSignupDataToBackend(SignupData signupData)
    {
        // Convert the SignupData object to JSON
        string jsonPayload = JsonUtility.ToJson(signupData);

        // Start a coroutine to send the POST request
        StartCoroutine(PostRequest(apiUrl, jsonPayload));
    }

    private IEnumerator PostRequest(string url, string jsonPayload)
    {
        Debug.Log($"Sending data to: {url}");
        Debug.Log($"Payload: {jsonPayload}");

        using (UnityWebRequest request = new UnityWebRequest(url + "register/", "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 10;

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Signup data sent successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error sending signup data: {request.error}");
            }
        }
    }
}
