using SimplyTrivia.Data;
using SimplyTrivia.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace SimplyTrivia
{
    /// <summary>
    /// Class used for interacting with the API to create quizzes from pre-made questions.
    /// The UnityWebRequest is used to ensure cross compatibility between different platforms.
    /// E.G. The System.Net (including WebClient class) are not available on the WebGL platform. 
    /// </summary>
    public class TriviaClient : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// This URL is the base for all requests to the API for quizzes
        /// </summary>
        private const string _urlBase = "https://opentdb.com/api.php?";

        /// <summary>
        /// This URL is used to request a new token to be used
        /// </summary>
        private const string _urlRequestToken = "https://opentdb.com/api_token.php?command=request";

        /// <summary>
        /// This URL is used to reset the current token and requires the current token to be passed in
        /// </summary>
        private const string _urlResetToken = "https://opentdb.com/api_token.php?command=reset&token=";

        /// <summary>
        /// A field to store the current session token so it can be used when constructing
        /// the URL for API requests
        /// </summary>
        private string _sessionToken = string.Empty;

        /// <summary>
        /// This event gets fired when IsOpenTriviaAvailable gets called. We return a class
        /// containing a boolean which describes whether the service was able to get JSON back from the API
        /// </summary>
        public event EventHandler<OpenTriviaAvailability> OnTriviaAvailabilityReceived;

        /// <summary>
        /// When a request for questions from the API is received we deserialize it
        /// and invoke this event if the response was successful.
        /// </summary>
        public event EventHandler<OpenTriviaQuizReceived> OnTriviaQuizReceived;

        /// <summary>
        /// This event gets fired if a request for questions results in a response code which is
        /// anything other than success.
        /// </summary>
        public event EventHandler<OpenTriviaErrorReceived> OnTriviaErrorReceived;

        /// <summary>
        /// This event gets fired when a session token is first retrieved. 
        /// (When this client gets instantiated)
        /// </summary>
        public event EventHandler<OpenTriviaSessionTokenUpdated> OnTriviaSessionTokenCreated;

        /// <summary>
        /// This event gets fired when an existing session token is reset.
        /// </summary>
        public event EventHandler<OpenTriviaSessionTokenUpdated> OnTriviaSessionTokenRefreshed;

        /// <summary>
        /// Singleton for this MonoBehaviour
        /// </summary>
        public static TriviaClient Instance { get; private set; }
        #endregion

        #region Methods_Unity
        private void Awake()
        {
			ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;

			CreateSingleton();
            GetSessionToken();
        }
        #endregion    

        #region Methods_Custom
        private void CreateSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }
        #endregion

        #region Methods_API
        /// <summary>
        /// Send a request to the API for a series of questions matching the passed
        /// parameters. Depending on the response from the server, invoke the trivia recieved or
        /// trivia error received events.
        /// </summary>
        /// <param name="quizParams">A class full of parameters used to construct the URL and send the request</param>
        public void CreateQuiz(QuizParams quizParams)
        {
            StartCoroutine(FetchQuiz(quizParams));
        }

        private IEnumerator FetchQuiz(QuizParams quizParams)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(CreateUrl(quizParams)))
            {
#if UNITY_5
				yield return webRequest.Send();
#elif UNITY_2017_1
				yield return webRequest.Send();
#elif UNITY_2017_2_OR_NEWER
				yield return webRequest.SendWebRequest();
#endif

#if UNITY_2017_OR_NEWER
				if (webRequest.isNetworkError == true || webRequest.isHttpError == true)
#else
				if (webRequest.isError == true)
#endif
				{
                    if (OnTriviaErrorReceived != null)
                    {
                        OnTriviaErrorReceived(this, new OpenTriviaErrorReceived(new TriviaResponse()
                        {
                            response_code = -1,
                            results = null
                        }));
                    }
                }
                else
                {
                    string json = webRequest.downloadHandler.text;
                    JsonHelper.FixJSONCharacters(ref json);
                    TriviaResponse result = JsonUtility.FromJson<TriviaResponse>(json);

                    if (result.response_code_enum == TriviaResponseCodes.SUCCESS)
                    {
                        if (OnTriviaQuizReceived != null)
                        {
                            OnTriviaQuizReceived(this, new OpenTriviaQuizReceived(result));
                        }
                    }
                    else
                    {
                        OnTriviaErrorReceived(this, new OpenTriviaErrorReceived(result));
                    }
                }
            }
        }

        /// <summary>
        /// Construct the URL for the api from the quiz parameters passed into this method.
        /// It contains values such as the amount of questions, category, type and difficulty of
        /// the desired set of questions.
        /// </summary>
        /// <param name="quizParams">Parameters used to construct the URL and send the request</param>
        /// <returns>A string representing the URL from which to download the JSON to deserialize into classes</returns>
        private string CreateUrl(QuizParams quizParams)
        {
            string url = _urlBase + "amount=" + Mathf.Clamp(quizParams.NumberOfQuestions, 1, 50);

            if (quizParams.Category != TriviaCategory.ANY)
                url += "&category=" + (int)quizParams.Category;

            if (quizParams.Difficulty != TriviaDifficulty.ANY)
                url += "&difficulty=" + quizParams.Difficulty.ToString().ToLower();

            if (quizParams.QuestionType != TriviaType.ANY)
                url += "&type=" + quizParams.QuestionType.ToString().ToLower();

            if (quizParams.Encoding != TriviaEncoding.DEFAULT)
                url += "&encode=" + quizParams.Encoding.ToString().ToLower();

            if (string.IsNullOrEmpty(_sessionToken) == false)
                url += "&token=" + _sessionToken;

            return url;
        }

        /// <summary>
        /// A threaded method which asks for one question from the API and
        /// returns true is some JSON has been returned. Returns false
        /// if the JSON was empty.
        /// </summary>
        /// <returns>True if the returned JSON is not an empty string</returns>
        public void IsOpenTriviaAvailable()
        {
            FetchOpenTriviaAvailability();
        }

        private IEnumerator FetchOpenTriviaAvailability()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_urlBase + "amount=1"))
            {
#if UNITY_5
				yield return webRequest.Send();
#elif UNITY_2017_1
				yield return webRequest.Send();
#elif UNITY_2017_2_OR_NEWER
				yield return webRequest.SendWebRequest();
#endif

#if UNITY_2017_OR_NEWER
				if (webRequest.isNetworkError == true || webRequest.isHttpError == true)
#else
				if (webRequest.isError == true)
#endif
				{
					if (OnTriviaAvailabilityReceived != null)
                    {
                        OnTriviaAvailabilityReceived(this, new OpenTriviaAvailability(false));
                    }
                }
                else
                {
                    OnTriviaAvailabilityReceived(this, new OpenTriviaAvailability(string.IsNullOrEmpty(webRequest.downloadHandler.text) == false));
                }
            }
        }
#endregion

#region Methods_SessionTokens
        /// <summary>
        /// Fetch a new session token from the API. This is used to ensure that duplicate questions do not occur.
        /// </summary>
        public void GetSessionToken()
        {
            StartCoroutine(FetchSessionToken());
        }

        private IEnumerator FetchSessionToken()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_urlRequestToken))
            {
#if UNITY_5
				yield return webRequest.Send();
#elif UNITY_2017_1
				yield return webRequest.Send();
#elif UNITY_2017_2_OR_NEWER
				yield return webRequest.SendWebRequest();
#endif

#if UNITY_2017_OR_NEWER
				if (webRequest.isNetworkError == true || webRequest.isHttpError == true)
#else
				if (webRequest.isError == true)
#endif
				{
					Debug.LogError("[Error] Unable to get a new session token!", this);
                }
                else
                {
                    TriviaSessionTokenReponse result = JsonUtility.FromJson<TriviaSessionTokenReponse>(webRequest.downloadHandler.text);

                    if (OnTriviaSessionTokenCreated != null)
                    {
                        OnTriviaSessionTokenCreated(this, new OpenTriviaSessionTokenUpdated(result.token));
                    }
                }
            }
        }

        /// <summary>
        /// Reset the current session token.
        /// </summary>
        public void ResetSessionToken()
        {
            StartCoroutine(FetchResetSessionToken());
        }

        private IEnumerator FetchResetSessionToken()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_urlRequestToken + _sessionToken))
            {
#if UNITY_5
				yield return webRequest.Send();
#elif UNITY_2017_1
				yield return webRequest.Send();
#elif UNITY_2017_2_OR_NEWER
				yield return webRequest.SendWebRequest();
#endif

#if UNITY_2017_OR_NEWER
				if (webRequest.isNetworkError == true || webRequest.isHttpError == true)
#else
				if (webRequest.isError == true)
#endif
				{
					Debug.LogError("[Error] Unable to reset your session token!", this);
                }
                else
                {
                    TriviaSessionTokenReponse result = JsonUtility.FromJson<TriviaSessionTokenReponse>(webRequest.downloadHandler.text);

                    if (OnTriviaSessionTokenRefreshed != null)
                    {
                        OnTriviaSessionTokenRefreshed(this, new OpenTriviaSessionTokenUpdated(result.token));
                    }
                }
            }
        }
#endregion
    }
}
