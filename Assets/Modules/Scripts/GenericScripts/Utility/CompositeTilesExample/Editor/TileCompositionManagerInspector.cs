// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;
using UnityEditor;

using Rotorz.Tile.Editor;

// Breaks Unity 4.3 due to bug
using Custom;
//namespace Custom {

	/// <summary>
	/// Custom inspector for <see cref="TileCompositionManager"/> assets.
	/// </summary>
	[CustomEditor(typeof(TileCompositionManager))]
	sealed class TileCompositionManagerInspector : Editor {

		public override void OnInspectorGUI() {
			var manager = target as TileCompositionManager;

			for (int i = 0; i < manager.Compositions.Count; ++i) {
				if (i != 0)
					RotorzEditorGUI.SplitterLight();
				DrawCompositionEntry(i, manager.Compositions[i]);
			}

			RotorzEditorGUI.Splitter(thickness: 3);

			EditorGUILayout.LabelField("Define New Composition", EditorStyles.boldLabel);

			_newDefinitionName = EditorGUILayout.TextField("Name", _newDefinitionName);
			EditorGUILayout.LabelField("From Active System", ToolUtility.activeTileSystem != null ? ToolUtility.activeTileSystem.name : "(None)");

			EditorGUI.BeginDisabledGroup(ToolUtility.activeTileSystem == null);
			if (GUILayout.Button("Add Definition"))
				AddDefinition();
			EditorGUI.EndDisabledGroup();
		}

		/// <summary>
		/// Custom label where text is aligned to left but centered vertically.
		/// </summary>
		private static GUIStyle s_LabelAlignMiddleLeft;

		/// <summary>
		/// Draw one composition entry with a remove button.
		/// </summary>
		/// <param name="index">Zero-based index of composition within manager.</param>
		/// <param name="composition">Tile composition definition.</param>
		private void DrawCompositionEntry(int index, TileComposition composition) {
			Rect position = GUILayoutUtility.GetRect(0, 24);

			if (s_LabelAlignMiddleLeft == null) {
				s_LabelAlignMiddleLeft = new GUIStyle(EditorStyles.label);
				s_LabelAlignMiddleLeft.alignment = TextAnchor.MiddleLeft;
			}

			// Draw label.
			position.width -= 28;
			GUI.Label(position, composition.Name, s_LabelAlignMiddleLeft);

			// Draw remove button.
			position.x = position.xMax;
			position.width = 28;
			if (GUI.Button(position, "X")) {
				var manager = target as TileCompositionManager;
				Undo.RecordObject(manager, "Remove Definition");
				manager.Compositions.RemoveAt(index);
				EditorUtility.SetDirty(manager);

				// Must not proceed to process GUI since the number of definitions
				// has just decreased by one. Errors are likely to occur when attempting
				// to process remainder of GUI, so just bail early!
				GUIUtility.ExitGUI();
			}
		}

		private string _newDefinitionName = "";

		/// <summary>
		/// Extract tiles from active tile system and add new definition to manager.
		/// </summary>
		private void AddDefinition() {
			if (string.IsNullOrEmpty(_newDefinitionName)) {
				EditorUtility.DisplayDialog("Error: Cannot Add Definition", "Name must contain at least one character.", "OK");
				return;
			}
			if (ToolUtility.activeTileSystem == null) {
				EditorUtility.DisplayDialog("Error: Cannot Add Definition", "Cannot define composition since no tile system is active.", "OK");
				return;
			}

			var manager = target as TileCompositionManager;
			var composition = manager.DefineCompositionFromTileSystem(_newDefinitionName, ToolUtility.activeTileSystem);

			if (composition == null) {
				EditorUtility.DisplayDialog("Error: Cannot Add Definition", "No tiles were detected in active tile system.", "OK");
				return;
			}

			CompositeTileTool.ActiveComposition = composition;
			ToolUtility.RepaintToolPalette();

			// Clear name input ready for next definition.
			_newDefinitionName = "";
			GUIUtility.keyboardControl = 0;
		}

	}

//}
