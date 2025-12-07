using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fastJSON;
using Runtime.StateMachine.Descriptions;
using UnityEngine;

namespace Runtime.StateMachine
{
    public class TempScript : MonoBehaviour
    {
        private StateMachineModel _stateMachineModel;

        private StateMachinePresenter _stateMachinePresenter;
        
        private StateMachineSystem _stateMachineSystem;
        
        private StateMachineView _stateMachineView;
        
        private StateDescriptionCollection _stateDescriptionCollection;
        
        private PointOfInterestDescriptionCollection _pointOfInterestDescriptionCollection;

        private TempModel _tempModel;
        
        [SerializeField] private UpdateService _updateService;

        [SerializeField] private float hungry;
        [SerializeField] private float energy;
        [SerializeField] private Vector2 position;
        
        private Vector2 scroll;

        private void OnGUI()
        {
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 18;
            labelStyle.normal.textColor = Color.white;

            GUIStyle currentStateStyle = new GUIStyle(GUI.skin.label);
            currentStateStyle.fontSize = 18;
            currentStateStyle.normal.textColor = Color.green;

            GUILayout.BeginArea(new Rect(10, 10, 500, Screen.height - 20));
            scroll = GUILayout.BeginScrollView(scroll, GUILayout.Width(500), GUILayout.Height(Screen.height - 20));

            GUILayout.Label("Stats:", labelStyle);
            foreach (var s in _tempModel.Stats)
                GUILayout.Label(s.Key + ": " + s.Value, labelStyle);

            GUILayout.Space(15);

            GUILayout.Label("Current State:", labelStyle);
            GUILayout.Label(_stateMachineModel.CurrentStateName, currentStateStyle);

            GUILayout.Space(15);
            GUILayout.Label("States:", labelStyle);

            foreach (var state in _stateDescriptionCollection.States)
            {
                bool isCurrent = state.Key == _stateMachineModel.CurrentStateName;
                GUILayout.Label("▪ " + state.Key, isCurrent ? currentStateStyle : labelStyle);

                if (state.Value != null)
                {
                    foreach (var t in state.Value.Transitions)
                    {
                        GUILayout.Label("  → " + t.ToState, labelStyle);
                    }
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
        
        private void Start()
        {
            var statesString = File.ReadAllText("Assets/Resources/states_description.json");
            var pointString = File.ReadAllText("Assets/Resources/points_of_interest_description.json");

            var statesDictionary = JSON.ToObject<Dictionary<string, object>>(statesString);
            var pointsDictionary = JSON.ToObject<Dictionary<string, object>>(pointString);

            _stateDescriptionCollection = new StateDescriptionCollection();
            
            _stateDescriptionCollection.Deserialize(statesDictionary);
            
            _pointOfInterestDescriptionCollection = new PointOfInterestDescriptionCollection();
            
            _pointOfInterestDescriptionCollection.Deserialize(pointsDictionary);

            _stateMachineModel = new StateMachineModel(_stateDescriptionCollection);
            
            _stateMachineView =  new StateMachineView();
            
            _stateMachinePresenter = new StateMachinePresenter(_stateMachineModel, _stateMachineView);

            _tempModel = new TempModel();
            
            _stateMachineSystem = new StateMachineSystem(_stateMachineModel, _tempModel);

            _updateService.OnUpdate += _stateMachineSystem.Update;
            
            _stateMachineModel.OnChange += OnChangeState;
            
            Debug.Log(_pointOfInterestDescriptionCollection.Get("p_0"));
            Debug.Log(_pointOfInterestDescriptionCollection.Get("p_1"));
            Debug.Log(_stateMachineModel.CurrentState.Actions.First().Type);
            
        }

        private void OnChangeState()
        {
            foreach (var action in _stateMachineModel.CurrentState.Actions)
            {
                switch (action)
                {
                    case TimerActionDescription timerAction:
                    {
                        _tempModel.Stats[timerAction.Timer] = DateTimeOffset.UtcNow.AddSeconds(timerAction.Duration).ToUnixTimeSeconds();
                        
                        break;
                    }

                    case SetPointOfInterestActionDescription setPointOfInterest:
                    {
                        _tempModel.Stats["point_of_interest"] = _pointOfInterestDescriptionCollection.Get(setPointOfInterest.PointOfInterest);
                        
                        break;
                    }
                }
            }
        }

        private void Update()
        {
            _tempModel.Stats["hungry"] = hungry;
            _tempModel.Stats["energy"] = energy;
            _tempModel.Stats["position"] = position;
        }
    }

    public class TempModel : ISystemModel
    {
        public Dictionary<string, object> Stats { get; }

        public TempModel()
        {
            Stats = new Dictionary<string, object>()
            {
                {"hungry", 100},
                {"energy", 50},
                {"position", Vector2.zero},
                {"point_of_interest", Vector2.zero}
            };
        }
    }
}