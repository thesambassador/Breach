// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System;

using Rotorz.Tile;

namespace Custom {

	/// <summary>
	/// Utility functions to assist with painting and erasing tile compositions.
	/// </summary>
	public static class TileCompositionUtility {

		/// <summary>
		/// Erase all grouped tiles which intersect tile composition.
		/// </summary>
		/// <param name="system">Tile system.</param>
		/// <param name="index">Index of grouped tile.</param>
		/// <param name="composition">Tile composition.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when input tile system has a value of <c>null</c>.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public static void EraseGroup(this TileSystem system, TileIndex index, TileComposition composition) {
			if (system == null)
				throw new ArgumentNullException("system");

			int eraseRows, eraseColumns;
			if (composition != null) {
				eraseRows = composition.Rows;
				eraseColumns = composition.Columns;
			}
			else {
				// No composition was specified, just erase seed tile.
				eraseRows = 1;
				eraseColumns = 1;
			}

			var compositeMap = system.GetComponent<CompositeTileMap>();

			system.BeginBulkEdit();

			// Erase all compositions that intersect specified composition.
			for (int row = 0; row < eraseRows; ++row) {
				for (int column = 0; column < eraseColumns; ++column) {
					TileIndex eraseIndex = new TileIndex(index.row + row, index.column + column);
					if (compositeMap != null) {
						var group = compositeMap.GetGroupedTileIndices(eraseIndex);
						if (group != null) {
							foreach (int flatIndex in group)
								system.EraseTile(compositeMap.TileIndexFromFlatIndex(flatIndex));
							compositeMap.UngroupTiles(eraseIndex);
						}
					}
					system.EraseTile(eraseIndex);
				}
			}

			system.EndBulkEdit();
		}

		/// <summary>
		/// Erase all tiles in group.
		/// </summary>
		/// <param name="system">Tile system.</param>
		/// <param name="index">Index of grouped tile.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when input tile system has a value of <c>null</c>.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public static void EraseGroup(this TileSystem system, TileIndex index) {
			EraseGroup(system, index, null);
		}

		/// <summary>
		/// Paint composition of tiles onto tile system.
		/// </summary>
		/// <param name="system">Tile system.</param>
		/// <param name="index">Index of tile to begin painting from (upper-left).</param>
		/// <param name="composition">Tile composition.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when input tile system or composition have a value of <c>null</c>.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public static void PaintComposition(this TileSystem system, TileIndex index, TileComposition composition) 
        {
			if (system == null)
				throw new ArgumentNullException("system");
			if (composition == null)
				throw new ArgumentNullException("composition");

			int compositionArea = composition.Rows * composition.Columns;
			if (compositionArea == 0)
				return;

			// Fetch `CompositeTileMap` from tile system, add if necessary.
			var compositeMap = system.gameObject.GetComponent<CompositeTileMap>();
			if (compositeMap == null)
				compositeMap = system.gameObject.AddComponent<CompositeTileMap>();

         
                system.BeginBulkEdit();
                system.EraseGroup(index, composition);
                for (int row = 0; row < composition.Rows; ++row)
                {
                    for (int column = 0; column < composition.Columns; ++column)
                    {
                        TileIndex paintIndex = new TileIndex(index.row + row, index.column + column);

                        // Don't bother creating group for one tile.
                        if (compositionArea > 1)
                            compositeMap.GroupTiles(index, paintIndex, composition.Name);

                        TileData compositionTile = composition[row, column];
                        if (compositionTile != null)
                        {
                            // Set tile data directly (rather than via brush) so that
                            // simple rotation is also preserved. Manual offsets are
                            // not preserved in this example, though you could add this
                            // if desired by using a custom data structure instead of
                            // simply reusing `TileData`.
                            system.SetTileFrom(index.row + row, index.column + column, compositionTile);
                            system.RefreshTile(index.row + row, index.column + column);
                        }
                        else
                        {
                            // Erase tile as per normal.
                            system.EraseTile(index.row + row, index.column + column);
                        }
                    }
                
                }

                system.EndBulkEdit();
             
		
		}


        public static Boolean IsGroupPlacable(this TileSystem system, TileIndex index)
        {
            var compositeMap = system.gameObject.GetComponent<CompositeTileMap>();
            if (compositeMap == null)
                compositeMap = system.gameObject.AddComponent<CompositeTileMap>();


            String CompName = "";
            bool VerticalRoomTooClose = false;
            bool RoomsConnecting = false;
            for (int i = 1; i < 5; i++)
            {
                CompositeTileGroup grp = compositeMap.GetGroupedTileIndices(new TileIndex(index.row - i, index.column));
                if (grp != null)
                {
                    // CompName = grp.CompositionName;
                    if (grp.CompositionName.Equals("RoomTest"))
                    {
                        VerticalRoomTooClose = true;
                    }
                }
            }

            return (!VerticalRoomTooClose || RoomsConnecting);

        }







	}

}
