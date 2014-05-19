// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;
using UnityEditor;

using Rotorz.Tile;
using Rotorz.Tile.Editor;

namespace Custom {

	/// <summary>
	/// Custom tool which paints previously composed tile compositions onto the active
	/// tile system. This tool also includes erase functionality.
	/// </summary>
	[InitializeOnLoad]
	public class CompositeTileTool : PaintTool {

		static CompositeTileTool() {
			ToolManager.Instance.RegisterTool<CompositeTileTool>();
		}

		/// <summary>
		/// Gets or sets the active tile composition. A value of <c>null</c> indicates
		/// that erase function should be used.
		/// </summary>
		public static TileComposition ActiveComposition { get; set; }

		#region Tool Information

		public override string Name {
			get { return "custom.composite.paint"; }
		}

		public override string Label {
			get { return ">C<"; }
		}

		public override Texture2D IconNormal {
			get { return null; }
		}

		public override Texture2D IconActive {
			get { return null; }
		}

		#endregion

		#region Tool Interaction

		public override void OnTool(ToolEvent e, IToolContext context) {
			if (e.type == EventType.MouseDown) {
				context.TileSystem.BeginBulkEdit();
				OnPaint(e, context);
				context.TileSystem.EndBulkEdit();
			}
		}

		protected override void OnPaint(ToolEvent e, IToolContext context) {
			var system = context.TileSystem;

			TileIndex paintIndex = new TileIndex(e.row, e.column);

			if (e.leftButton) {
				// Paint with brush!
				system.PaintComposition(paintIndex, ActiveComposition);
			}
			else {
				// Erase!
				var compositionMap = system.GetComponent<CompositeTileMap>();
				if (compositionMap == null)
					system.EraseTile(paintIndex);
				else
					system.EraseGroup(paintIndex, ActiveComposition);
			}
		}

		#endregion

		#region Tool Options Interface

		public override void OnToolOptionsGUI() {
			DrawActiveCompositionPopup("Composition");
		}

		/// <summary>
		/// Draw popup control allowing selection of active tile composition.
		/// </summary>
		/// <param name="label">Prefix label for popup control.</param>
		private void DrawActiveCompositionPopup(string label) {
			GUIStyle style = EditorStyles.popup;
			Rect position = GUILayoutUtility.GetRect(GUIContent.none, style);

			position = EditorGUI.PrefixLabel(position, new GUIContent(label));

			string activeContent = ActiveComposition != null
				? ActiveComposition.Name
				: "(Erase)";

			position.width -= 31;
			if (GUI.Button(position, activeContent, style))
				ShowActiveCompositionPopup(position);

			position.x = position.xMax + 3;
			position.width = 28;
			if (GUI.Button(position, "..."))
				Selection.objects = new Object[] { TileCompositionEditorUtility.Manager };
		}

		/// <summary>
		/// Show popup menu allowing selection from defined tile compositions.
		/// </summary>
		/// <param name="position">Position of popup button in in tool palette.</param>
		private void ShowActiveCompositionPopup(Rect position) {
			var popup = new GenericMenu();

			popup.AddItem(new GUIContent("(Erase)"), ActiveComposition == null, SetActiveComposition, -1);
			popup.AddSeparator("");

			var compositions = TileCompositionEditorUtility.Manager.Compositions;
			for (int i = 0; i < compositions.Count; ++i) {
				var composition = compositions[i];
				popup.AddItem(new GUIContent(composition.Name), composition == ActiveComposition, SetActiveComposition, i);
			}

			popup.DropDown(position);
		}

		/// <summary>
		/// Set active tile composition from the given index. This method handles user selection
		/// from popup menu (<see cref="ShowActiveCompositionPopup(Rect)"/>).
		/// </summary>
		/// <param name="index">Zero-based index of tile composition of a value of -1 for "(Erase)".</param>
		private void SetActiveComposition(object index) {
			int i = (int)index;
			if (i == -1)
				ActiveComposition = null;
			else
				ActiveComposition = TileCompositionEditorUtility.Manager.Compositions[i];
		}

		#endregion

		#region Scene View

		private static Vector3[] s_SquareNozzleVerts = new Vector3[4];

		protected override void DrawNozzleIndicator(TileSystem system, int row, int column, Vector3 position, BrushNozzle nozzle, int radius) {
			if (ActiveComposition != null) {
				//
				// Draw custom indicator to highlight area which will be filled by template.
				//
				Matrix4x4 restoreMatrix = Handles.matrix;
				Handles.matrix = system.transform.localToWorldMatrix;

				Handles.color = Color.white;

				s_SquareNozzleVerts[0] = new Vector3(column, -row, 0);
				s_SquareNozzleVerts[1] = new Vector3(column + ActiveComposition.Columns, -row, 0);
				s_SquareNozzleVerts[2] = new Vector3(column + ActiveComposition.Columns, -row - ActiveComposition.Rows, 0);
				s_SquareNozzleVerts[3] = new Vector3(column, -row - ActiveComposition.Rows, 0);

				Handles.DrawSolidRectangleWithOutline(s_SquareNozzleVerts, new Color(1f, 0f, 0f, 0.07f), new Color(1f, 0f, 0f, 0.55f));

				Handles.matrix = restoreMatrix;
			}
			else {
				base.DrawNozzleIndicator(system, row, column, position, nozzle, 0);
			}
		}

		#endregion

	}

}