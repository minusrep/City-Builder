using System.Collections.Generic;
using System.IO;
using System.Linq;
using fastJSON;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Descriptions;
using Runtime.Descriptions.StateMachine;
using Runtime.Movement;
using Runtime.Timer;
using UnityEngine;

namespace Runtime.StateMachine
{
    public class TempScript : MonoBehaviour
    {
        private World _world;
        
        private StateMachineModel _stateMachineModel;

        private StateMachineSystem _stateMachineSystem;
        
        private StateDescriptionCollection _stateDescriptionCollection;
        
        private PointOfInterestDescriptionCollection _pointOfInterestDescriptionCollection;

        private CitizenModel _citizenModel;

        private MovementPresenter _movementPresenter;
        
        private TimerPresenter _timerPresenter;
        
        [SerializeField] private MovementView _movementView;
        
        [SerializeField] private ITimerView _timerView;
        
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
            foreach (var s in _citizenModel.Stats)
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
            var statesString = File.ReadAllText("Assets/Content/Resources/states_description.json");
            var pointString = File.ReadAllText("Assets/Content/Resources/points_of_interest_description.json");

            var statesDictionary = JSON.ToObject<Dictionary<string, object>>(statesString);
            var pointsDictionary = JSON.ToObject<Dictionary<string, object>>(pointString);
            
            _pointOfInterestDescriptionCollection = new PointOfInterestDescriptionCollection(pointsDictionary);
            
            _stateDescriptionCollection = new StateDescriptionCollection(statesDictionary);
            
            _stateMachineModel = new StateMachineModel(_stateDescriptionCollection);

            _citizenModel = new CitizenModel(0, null, "Vasek");

            _movementPresenter = new MovementPresenter(_citizenModel, _movementView, _updateService, _stateMachineModel,  _pointOfInterestDescriptionCollection);

            _timerPresenter = new TimerPresenter(_citizenModel, null, _stateMachineModel);
            
            _movementPresenter.Enable();
            
            _timerPresenter.Enable();
            
            _stateMachineSystem = new StateMachineSystem(_stateMachineModel, _world, _citizenModel);

            _updateService.OnUpdate += _stateMachineSystem.Update;
            
            Debug.Log(_pointOfInterestDescriptionCollection.Get("p_0"));
            Debug.Log(_pointOfInterestDescriptionCollection.Get("p_1"));
            Debug.Log(_stateMachineModel.CurrentState.Actions.First().Type);
            
        }

        private void Update()
        {
            _citizenModel.Stats["hungry"] = hungry;
            _citizenModel.Stats["energy"] = energy;
        }
    }
}