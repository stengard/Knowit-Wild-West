// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GenericScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// KeywordManager allows you to specify keywords and methods in the Unity
    /// Inspector, instead of registering them explicitly in code.
    /// This also includes a setting to either automatically start the
    /// keyword recognizer or allow your code to start it.
    ///
    /// IMPORTANT: Please make sure to add the microphone capability in your app, in Unity under
    /// Edit -> Project Settings -> Player -> Settings for Windows Store -> Publishing Settings -> Capabilities
    /// or in your Visual Studio Package.appxmanifest capabilities.
    /// </summary>
    public partial class KeywordManager : MonoBehaviour
    {
        [System.Serializable]
        public struct KeywordAndResponse
        {
            [Tooltip("The keyword to recognize.")]
            public string Keyword;
            [Tooltip("The KeyCode to recognize.")]
            public bool NeedToLookAt;
            [Tooltip("The UnityEvent to be invoked when the keyword is recognized.")]
            public string Response;
        }

        // This enumeration gives the manager two different ways to handle the recognizer. Both will
        // set up the recognizer and add all keywords. The first causes the recognizer to start
        // immediately. The second allows the recognizer to be manually started at a later time.
        public enum RecognizerStartBehavior { AutoStart, ManualStart };

        [Tooltip("An enumeration to set whether the recognizer should start on or off.")]
        public RecognizerStartBehavior RecognizerStart;

        [Tooltip("An array of string keywords and UnityEvents, to be set in the Inspector.")]
        public KeywordAndResponse[] KeywordsAndResponses;

        private KeywordRecognizer keywordRecognizer;
        private Dictionary<string, UnityEvent> responses;

        Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

        // Use this for initialization

        void Start()
        {
            keywordRecognizer = null;

            foreach (KeywordAndResponse keywordAndResponse in KeywordsAndResponses)
            {
                keywords.Add(keywordAndResponse.Keyword, () =>
                {
                    var focusObject = GazeGestureManager.Instance.FocusedObject;
                    if (focusObject != null)
                    {
                        // Call the OnStop method on just the focused object.
                        focusObject.SendMessage(keywordAndResponse.Response);
                    }
                });
            }


            // Tell the KeywordRecognizer about our keywords.
            keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

            // Register a callback for the KeywordRecognizer and start recognizing!
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
            keywordRecognizer.Start();
        }

        private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            System.Action keywordAction;
            if (keywords.TryGetValue(args.text, out keywordAction))
            {
                keywordAction.Invoke();
            }
        }

        /// <summary>
        /// Make sure the keyword recognizer is off, then start it.
        /// Otherwise, leave it alone because it's already in the desired state.
        /// </summary>
        public void StartKeywordRecognizer()
        {
            if (keywordRecognizer != null && !keywordRecognizer.IsRunning)
            {
                keywordRecognizer.Start();
            }
        }

        /// <summary>
        /// Make sure the keyword recognizer is on, then stop it.
        /// Otherwise, leave it alone because it's already in the desired state.
        /// </summary>
        public void StopKeywordRecognizer()
        {
            if (keywordRecognizer != null && keywordRecognizer.IsRunning)
            {
                keywordRecognizer.Stop();
            }
        }
    }
}