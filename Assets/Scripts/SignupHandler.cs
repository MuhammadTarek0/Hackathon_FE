
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SignupHandler : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] private GameEvent showLoadingGameEvent;
    [SerializeField] private GameEvent hideLoadingGameEvent;
    [SerializeField] private GameEvent hideSignupContentGameEvent;
    [Header("References")]
    [SerializeField]
    private SignupContentHandler signupContent;
    [Header("API")]
    [SerializeField] private string apiUrl;

    private SignupContentHandler signupCanvas;
    private Coroutine signupCoroutine;

    public void CloseSignupContent()
    {
        if (!signupCanvas)
        {
            return;
        }

        Destroy(signupCanvas.gameObject, 0.1f);
    }
    public void InstantiateSignupContent()
    {
        if (signupCanvas)
        {
            return;
        }

        signupCanvas = Instantiate(signupContent);
    }

    public void TrySignup(SignupData signupData)
    {
        if (signupCoroutine is not null) return;
        signupCoroutine = StartCoroutine(TrySignupAsync(signupData));
    }

    private IEnumerator TrySignupAsync(SignupData signupData)
    {
        // Create JSON payload
        string jsonPayload = JsonUtility.ToJson(signupData);

        // Log the payload to the console
        Debug.Log($"JSON Payload: {jsonPayload}");

        // Create a UnityWebRequest with JSON payload
        using UnityWebRequest www = new UnityWebRequest(apiUrl + "register/", "POST")
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
            hideSignupContentGameEvent.Raise();
            Debug.Log("Signup successful: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Error: {www.error}");
        }
        signupCoroutine = null;
    }
}