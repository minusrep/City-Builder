using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Citizens.Debugging
{
    public class CitizenDebugPresenter : IPresenter
    {
        private readonly CitizenDebugView _view;

        private readonly CitizenModel _model;

        public CitizenDebugPresenter(CitizenDebugView view, CitizenModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.OnGui += OnGui;
        }

        public void Disable()
        {
            _view.OnGui -= OnGui;
        }

        private void OnGui()
        {
            if (!Debug.isDebugBuild || _model == null) return;

            // Enable rich text for colored labels
            if (Event.current.type == EventType.Repaint)
            {
                GUI.skin.label.richText = true;
            }

            // Define styles
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                wordWrap = true,
                normal = { textColor = Color.white }
            };

            GUIStyle headerStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.yellow }
            };

            GUIStyle stateStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 13,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.cyan }
            };

            GUIStyle warningStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.red }
            };

            // Start GUI area
            float panelWidth = 420;
            GUILayout.BeginArea(new Rect(10, 10, panelWidth, Screen.height - 20));

            // Background box
            GUI.Box(new Rect(0, 0, panelWidth - 20, Screen.height - 20), "", GUI.skin.box);

            // Scroll view
            GUILayout.BeginVertical();
            GUILayout.Space(10);

            // Title
            GUILayout.Label($"=== CITIZEN #{_model.Id} ===", headerStyle);
            GUILayout.Space(10);

            // Basic Info
            GUILayout.Label($"ID: {_model.Id}", labelStyle);
            GUILayout.Label($"Position: {_model.Position}", labelStyle);

            // Current State
            GUILayout.Space(10);
            GUILayout.Label("=== STATE MACHINE ===", headerStyle);

            if (_model.StateMachine != null)
            {
                GUILayout.Label($"Current State: {_model.StateMachine.CurrentStateName}", stateStyle);

                if (_model.StateMachine.CurrentState != null)
                {
                    // Display Actions for current state
                    if (_model.StateMachine.CurrentState.Actions != null &&
                        _model.StateMachine.CurrentState.Actions.Count > 0)
                    {
                        GUILayout.Label($"Actions ({_model.StateMachine.CurrentState.Actions.Count}):", labelStyle);
                        foreach (var action in _model.StateMachine.CurrentState.Actions)
                        {
                            GUILayout.Label($"  • {action.GetType().Name}", labelStyle);
                        }
                    }

                    // Display Transitions
                    if (_model.StateMachine.CurrentState.Transitions != null &&
                        _model.StateMachine.CurrentState.Transitions.Count > 0)
                    {
                        GUILayout.Label("Transitions:", labelStyle);
                        foreach (var transition in _model.StateMachine.CurrentState.Transitions)
                        {
                            GUILayout.Label($"  → {transition.ToState}", labelStyle);
                        }
                    }
                }
            }
            else
            {
                GUILayout.Label("State Machine: NULL", warningStyle);
            }

            // Stats
            GUILayout.Space(10);
            GUILayout.Label("=== STATS ===", headerStyle);

            if (_model.Stats != null)
            {
                // Note: You might need to add a GetAllStats() method to StatModelCollection
                // For now, we'll try to get common stats
                TryDisplayStat("satiety");
                TryDisplayStat("energy");
            }
            else
            {
                GUILayout.Label("No Stats", warningStyle);
            }

            // Flags
            GUILayout.Space(10);
            GUILayout.Label("=== FLAGS ===", headerStyle);

            if (_model.Flags != null && _model.Flags.Count > 0)
            {
                foreach (var flag in _model.Flags)
                {
                    string color = flag.Value ? "lime" : "red";
                    GUILayout.Label($"<color={color}>• {flag.Key}: {flag.Value}</color>", labelStyle);
                }
            }
            else
            {
                GUILayout.Label("No Flags", labelStyle);
            }

            // Timers
            GUILayout.Space(10);
            GUILayout.Label("=== TIMERS ===", headerStyle);

            if (_model.Timers != null && _model.Timers.Count > 0)
            {
                foreach (var timer in _model.Timers)
                {
                    GUILayout.Label($"• {timer.Key}: {timer.Value}", labelStyle);
                }
            }
            else
            {
                GUILayout.Label("No Active Timers", labelStyle);
            }

            // Points of Interest
            GUILayout.Space(10);
            GUILayout.Label("=== POINTS OF INTEREST ===", headerStyle);

            if (_model.PointsOfInterest != null && _model.PointsOfInterest.Count > 0)
            {
                foreach (var poi in _model.PointsOfInterest)
                {
                    GUILayout.Label($"• {poi.Key}: {poi.Value}", labelStyle);
                }
            }
            else
            {
                GUILayout.Label("No Points of Interest", labelStyle);
            }

            // Debug Actions
            GUILayout.Space(15);
            GUILayout.Label("=== DEBUG ACTIONS ===", headerStyle);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Idle", GUILayout.Height(25)))
            {
                _model.InvokeAnimation("idle");
            }

            if (GUILayout.Button("Walk", GUILayout.Height(25)))
            {
                _model.InvokeAnimation("walk");
            }

            if (GUILayout.Button("Work", GUILayout.Height(25)))
            {
                _model.InvokeAnimation("work");
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Clear All Flags", GUILayout.Height(25)))
            {
                _model.Flags.Clear();
            }

            if (GUILayout.Button("Add Test POI", GUILayout.Height(25)))
            {
                _model.SetPointOfInterest("test_debug",
                    new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
            }

            GUILayout.Space(10);
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        
        private void TryDisplayStat(string statId)
        {
            try
            {
                var stat = _model.Stats.Get(statId);
                if (stat != null)
                {
                    float percentage = (stat.Value - stat.Stat.Min) / (stat.Stat.Max - stat.Stat.Min);
                    string color = percentage > 0.7f ? "lime" : percentage > 0.3f ? "yellow" : "red";
                    GUILayout.Label($"<color={color}>• {statId}: {stat.Value:F1} [{stat.Stat.Min}-{stat.Stat.Max}]</color>", GUI.skin.label);
                }
            }
            catch
            {
                // Stat doesn't exist, ignore
            }
        }
    }
}