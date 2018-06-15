using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Luis_Manager : Singleton<Luis_Manager>, IDictationHandler
{
    [System.Serializable] //this class represents the LUIS response
    public class AnalysedQuery
    {
        public TopScoringIntentData topScoringIntent;
        public EntityData[] entities;
        public string query;
    }

    // This class contains the Intent LUIS determines 
    // to be the most likely
    [System.Serializable]
    public class TopScoringIntentData
    {
        public string intent;
        public float score;
    }

    // This class contains data for an Entity
    [System.Serializable]
    public class EntityData
    {
        public string entity;
        public string type;
        public int startIndex;
        public int endIndex;
        public float score;
    }

    //Substitute the value of luis Endpoint with your own End Point
    string luisEndpoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/e2924829-8f07-410c-b71b-3db87f761d4e?subscription-key=2c9efc466726498f94f3ecc20f45de1d&verbose=true&timezoneOffset=0&q=";

    private bool submittedRequest;

    /// <summary>
    /// Call LUIS to submit a dictation result.
    /// </summary>
    public IEnumerator SubmitRequestToLuis(string dictationResult)
    {
        if (string.IsNullOrEmpty(dictationResult))
        {
            StartCoroutine(DictationInputManager.StartRecording());
            yield break;
        }

        submittedRequest = true;
        string queryString = string.Concat(Uri.EscapeDataString(dictationResult));

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(luisEndpoint + queryString))
        {
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityWebRequest.SendWebRequest();

            long responseCode = unityWebRequest.responseCode;

            if (responseCode != 200)
            {
                Debug.LogError(unityWebRequest.responseCode + " " + unityWebRequest.downloadHandler.text);
            }

            AnalysedQuery analysedQuery = JsonUtility.FromJson<AnalysedQuery>(unityWebRequest.downloadHandler.text);

            //analyse the elements of the response 
            AnalyseResponseElements(analysedQuery);
        }

        submittedRequest = false;
    }

    private void Update()
    {
        if (!DictationInputManager.IsListening && !submittedRequest)
        {
            StartCoroutine(DictationInputManager.StartRecording());
        }
    }

    private void AnalyseResponseElements(AnalysedQuery aQuery)
    {
        //string topIntent = aQuery.topScoringIntent.intent;

        // Create a dictionary of entities associated with their type
        Dictionary<string, string> entityDic = new Dictionary<string, string>();

        foreach (EntityData ed in aQuery.entities)
        {
            if (!entityDic.ContainsKey(ed.type))
            {
                entityDic.Add(ed.type, ed.entity);
            }
        }

        // Depending on the topmost recognised intent, read the entities name
        switch (aQuery.topScoringIntent.intent)
        {
            case "ChangeObjectState":
                string stateOfTarget = null;
                string state = null;

                foreach (var pair in entityDic)
                {
                    if (pair.Key == "target")
                    {
                        stateOfTarget = pair.Value;
                    }
                    else if (pair.Key == "booleanState")
                    {
                        state = pair.Value;
                    }
                }

                Luis_Handler.Instance.ChangeObjectState(stateOfTarget, state);
                break;
        }
    }

    public void OnDictationHypothesis(DictationEventData eventData)
    {
    }

    public void OnDictationResult(DictationEventData eventData)
    {
        Debug.Log(eventData.DictationResult);
        StartCoroutine(DictationInputManager.StopRecording());
        StartCoroutine(SubmitRequestToLuis(eventData.DictationResult));
        eventData.Use();
    }

    public void OnDictationComplete(DictationEventData eventData)
    {
    }

    public void OnDictationError(DictationEventData eventData)
    {
    }
}

