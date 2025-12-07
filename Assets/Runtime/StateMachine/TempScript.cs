using System.Collections.Generic;
using System.IO;
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

        private TempModel _tempModel;
        
        [SerializeField] private UpdateService _updateService;

        [SerializeField] private float hungry;
        [SerializeField] private float energy;
        
        
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
            var file = File.ReadAllText("Assets/Resources/states_description.json");

            var json = JSON.ToObject<Dictionary<string, object>>(file);

            _stateDescriptionCollection = new StateDescriptionCollection();
            
            _stateDescriptionCollection.Deserialize(json);

            _stateMachineModel = new StateMachineModel(_stateDescriptionCollection);
            
            _stateMachineView =  new StateMachineView();
            
            _stateMachinePresenter = new StateMachinePresenter(_stateMachineModel, _stateMachineView);

            _tempModel = new TempModel();
            
            _stateMachineSystem = new StateMachineSystem(_stateMachineModel, _tempModel);

            _updateService.OnUpdate += _stateMachineSystem.Update;
        }

        private void Update()
        {
            _tempModel.Stats["hungry"] = hungry;
            _tempModel.Stats["energy"] = energy;
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
            };
        }
    }
}