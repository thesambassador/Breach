// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;
using UnityEditor;

using Rotorz.Tile;

namespace Custom {

	/// <summary>
	/// Utility functions provide access to composite tile definitions and supporting
	/// definition by extracting tiles from a given tile system.
	/// </summary>
	public static class TileCompositionEditorUtility {

		/// <summary>
		/// Path of composite definition manager asset relative to project directory.
		/// </summary>
		private const string ManagerAssetPath = "Assets/Composition Definition Manager.asset";

		private static TileCompositionManager s_Manager;

		/// <summary>
		/// Gets composite tile definition manager instance; always returns valid instance.
		/// </summary>
		public static TileCompositionManager Manager {
			get {
				if (s_Manager == null) {
					s_Manager = AssetDatabase.LoadAssetAtPath(ManagerAssetPath, typeof(TileCompositionManager)) as TileCompositionManager;
					if (s_Manager == null) {
						s_Manager = ScriptableObject.CreateInstance<TileCompositionManager>();
						AssetDatabase.CreateAsset(s_Manager, ManagerAssetPath);
						AssetDatabase.ImportAsset(ManagerAssetPath);
					}
				}
				return s_Manager;
			}
		}

		/// <summary>
		/// Find minimum and maximum indices marking boundary of painted tiles
		/// within specified tile system.
		/// </summary>
		/// <param name="system">Tile system.</param>
		/// <param name="minimum">Output index of minimum tile in system.</param>
		/// <param name="maximum">Output index of maximum tile in system.</param>
		/// <returns>
		/// A value of <c>true</c> if valid bounds were found; otherwise a value
		/// of <c>false</c>. Minimum and maximum bounds are invalid if no tiles
		/// are found.
		/// </returns>
		private static bool FindTileBounds(TileSystem system, out TileIndex minimum, out TileIndex maximum) {
			minimum = new TileIndex(int.MaxValue, int.MaxValue);
			maximum = TileIndex.zero;

			bool valid = false;

			for (int row = 0; row < system.rows; ++row) {
				for (int column = 0; column < system.columns; ++column) {
					if (system.GetTile(row, column) != null) {
						valid = true;

						minimum.row = Mathf.Min(minimum.row, row);
						minimum.column = Mathf.Min(minimum.column, column);
						maximum.row = Mathf.Max(maximum.row, row);
						maximum.column = Mathf.Max(maximum.column, column);
					}
				}
			}

			if (!valid)
				maximum = minimum = TileIndex.invalid;

			return valid;
		}

		/// <summary>
		/// Define new composition from tiles on specified tile system.
		/// </summary>
		/// <param name="manager">Composition tile definition manager to amend.</param>
		/// <param name="name">Name for new definition (doesn't need to be unique, but it helps!).</param>
		/// <param name="system">Tile system which contains template.</param>
		/// <returns>
		/// New <see cref="TileComposition"/> instance when input tile system
		/// contains at least one tile; otherwise a value of <c>null</c>.
		/// </returns>
		public static TileComposition DefineCompositionFromTileSystem(this TileCompositionManager manager, string name, TileSystem system) {
			TileIndex minimum, maximum;
			if (!FindTileBounds(system, out minimum, out maximum))
				return null;

			int rowCount = Mathf.Max(0, maximum.row - minimum.row + 1);
			int columnCount = Mathf.Max(0, maximum.column - minimum.column + 1);

			var composition = new TileComposition(name, rowCount, columnCount);
			for (int row = minimum.row; row <= maximum.row; ++row) {
				for (int column = minimum.column; column <= maximum.column; ++column) {
					var tile = system.GetTile(row, column);

					// Copy data from source tile for composition.
					TileData compositionTile = new TileData();
					if (tile != null && tile.brush != null) {
						compositionTile.SetFrom(tile);
						compositionTile.gameObject = null;
					}

					composition[row - minimum.row, column - minimum.column] = compositionTile;
				}
			}

			Undo.RecordObject(manager, "Add Definition");
			manager.Compositions.Add(composition);
			EditorUtility.SetDirty(manager);

			return composition;
		}

	}

}
