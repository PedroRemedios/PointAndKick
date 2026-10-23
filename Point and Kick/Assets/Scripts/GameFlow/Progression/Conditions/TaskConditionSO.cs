using System;
using GameFlow.Questing;
using UnityEngine;

namespace GameFlow.Progression.Conditions {
    [CreateAssetMenu(fileName = "TaskCondition", menuName = "Scriptable Objects/Questing/Task Condition", order = 3)]
    public class TaskConditionSO : ScriptableObject {
        [Header("Required Task")] public TaskSO task;
        [Header("Required State")] public TaskState taskState;
        
        public delegate void ConditionVerified();
        public event ConditionVerified OnConditionVerified;

        private void OnEnable() {
            task.OnTaskStateChanged += TaskStateChanged;
        }

        private void OnDisable() {
            task.OnTaskStateChanged -= TaskStateChanged;
        }

        private void TaskStateChanged(TaskSO newTask) {
            if (task == newTask && taskState == newTask.taskState) {
                OnConditionVerified?.Invoke();
            }
        }
    }
}