
    using System;
    using UnityEngine;
    using UnityEngine.Video;
    using UnityEngine.Networking;
    using System.Collections;

    public class CanvasVideoLoader : MonoBehaviour
    {
        [SerializeField] private string apiUrl;
        [SerializeField] private string token;
        [SerializeField] private string courseID;
        
        [SerializeField] private VideoPlayer videoPlayer;

        private void Start()
        {
            StartCoroutine(LoadVideoURL());
        }

        private IEnumerator LoadVideoURL()
        {
            string filesEndpoint = $"{apiUrl}/course/{courseID}";
            UnityWebRequest request = UnityWebRequest.Get(filesEndpoint);
            request.SetRequestHeader("Authorization", "Bearer " + token);
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                string videoURL = ParseVideoURL(jsonResponse);
                videoPlayer.url = videoURL;
                videoPlayer.Play();
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }

        private string ParseVideoURL(string url)
        {
            return "https://canvas.upenn.edu/courses/1801591/pages/what-you-should-expect-from-this-course-1-45";
        }
    }
