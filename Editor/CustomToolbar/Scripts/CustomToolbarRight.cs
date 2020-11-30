﻿using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender {

	[InitializeOnLoad]
	public static class CustomToolbarRight {
		
		private static CustomToolbarSetting setting;
		
		private static GUIContent recompileBtn;
		private static GUIContent reserializeSelectedBtn;
		private static GUIContent reserializeAllBtn;
		private static int selectedFramerate = 60;

		static CustomToolbarRight() {
			setting = CustomToolbarSetting.GetOrCreateSetting();
			
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);

			recompileBtn = EditorGUIUtility.IconContent("WaitSpin05");
			recompileBtn.tooltip = "Recompile";

			reserializeSelectedBtn = EditorGUIUtility.IconContent("Refresh");
			reserializeSelectedBtn.tooltip = "Reserialize Selected Assets";

			reserializeAllBtn = EditorGUIUtility.IconContent("P4_Updating");
			reserializeAllBtn.tooltip = "Reserialize All Assets";
		}

		static void OnToolbarGUI() {
			
			EditorGUILayout.LabelField("Time", GUILayout.Width(30));
			Time.timeScale = EditorGUILayout.Slider("", Time.timeScale, 0f, 10f, GUILayout.Width(150));
			GUILayout.Space(10);

			if (setting != null && setting.limitFPS)
			{
				EditorGUILayout.LabelField("FPS", GUILayout.Width(30));
				selectedFramerate = EditorGUILayout.IntSlider("", selectedFramerate, setting.minFPS, setting.maxFPS,
					GUILayout.Width(150));
				if (EditorApplication.isPlaying && selectedFramerate != Application.targetFrameRate)
				{
					Application.targetFrameRate = selectedFramerate;
				}

				GUILayout.Space(10);
			}
			else
			{
				GUILayout.Space(150 + 10 + 30 + 6);
			}

			DrawRecompileButton();
			DrawReserializeSelected();
			DrawReserializeAll();
		}

		static void DrawRecompileButton() {
			if (GUILayout.Button(recompileBtn, ToolbarStyles.commandButtonStyle)) {
				UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
				Debug.Log("Recompile");
			}
		}

		static void DrawReserializeSelected() {
			if (GUILayout.Button(reserializeSelectedBtn, ToolbarStyles.commandButtonStyle)) {
				ForceReserializeAssetsUtils.ForceReserializeSelectedAssets();
			}
		}

		static void DrawReserializeAll() {
			if (GUILayout.Button(reserializeAllBtn, ToolbarStyles.commandButtonStyle)) {
				ForceReserializeAssetsUtils.ForceReserializeAllAssets();
			}
		}
	}
}