// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;

using System;
using System.Collections.Generic;

using Rotorz.Tile;
using System.Collections;

namespace Custom {

	/// <summary>
	/// Maintains map of composite tiles making it possible to identify grouped
	/// tiles. Aside from providing the ability to erase entire groups of tiles,
	/// this can also be utilized within user scripts if desired.
	/// </summary>
	/// <example>
	/// <para>Fetch all tiles in group by providing the index of a grouped tile:</para>
	/// <code language="csharp"><![CDATA[
	/// var pickIndex = new TileIndex(1, 2);
	/// var compositeMap = system.GetComponent<CompositeTileMap>();
	/// var group = compositeMap.GetGroupedTileIndices(pickIndex);
	/// if (group != null) {
	///     foreach (int flatIndex in group) {
	///         TileIndex index = compositeMap.TileIndexFromFlatIndex(flatIndex);
	///         TileData tile = system.GetTile(index);
	///         if (tile != null) {
	///             // Filled tile within group.
	///         }
	///         else {
	///             // Empty tile within group.
	///         }
	///     }
	/// }
	/// ]]></code>
	/// <code language="unityscript"><![CDATA[
	/// var pickIndex:TileIndex = new TileIndex(1, 2);
	/// var compositeMap:CompositeTileMap = system.GetComponent.<CompositeTileMap>();
	/// var group:List.<int> = compositeMap.GetGroupedTileIndices(pickIndex);
	/// if (group != null) {
	///     for (var flatIndex:int in group) {
	///         var index:TileIndex = compositeMap.TileIndexFromFlatIndex(flatIndex);
	///         var tile:TileData = system.GetTile(index);
	///         if (tile != null) {
	///             // Filled tile within group.
	///         }
	///         else {
	///             // Empty tile within group.
	///         }
	///     }
	/// }
	/// ]]></code>
	/// </example>
	public sealed class CompositeTileMap : MonoBehaviour {

		[SerializeField]
		private bool _destroyOnAwake;
		[SerializeField]
		private bool _stripOnBuild;

		/// <summary>
		/// Gets or sets a value indicating whether this component should be
		/// destroyed upon receiving "Awake" message.
		/// </summary>
		public bool DestroyOnAwake {
			get { return _destroyOnAwake; }
			set { _destroyOnAwake = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this component should be
		/// stripped from tile system upon being built. Stripping this component
		/// helps to reduce memory overhead.
		/// </summary>
		public bool StripOnBuild {
			get { return _stripOnBuild; }
			set { _stripOnBuild = value; }
		}

		private TileSystem _system;

		/// <summary>
		/// Gets cached reference to associated tile system component.
		/// </summary>
		private TileSystem TileSystem {
			get {
				if (_system == null)
					_system = GetComponent<TileSystem>();
				return _system;
			}
		}

		#region Messages and Events

		private void Awake() {
			if (DestroyOnAwake)
				Destroy(this);
		}

		#endregion

		#region Tile Compositions

		private Dictionary<int,CompositeTileGroup  > _groups = new Dictionary<int, CompositeTileGroup>();
        
		/// <summary>
		/// Convert tile index (row, column) into flat index.
		/// </summary>
		/// <param name="index">Tile index.</param>
		/// <returns>
		/// Zero-based index of tile within tile system.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public int FlatIndexFromTileIndex(TileIndex index) {
			if ((int)index.row >= TileSystem.rows || (int)index.column >= TileSystem.columns)
				throw new ArgumentOutOfRangeException("index");

			return index.row * TileSystem.columns + index.column;
		}

		/// <summary>
		/// Convert flat zero-based index to tile index (row, column).
		/// </summary>
		/// <param name="index">Zero-based index of tile within tile system.</param>
		/// <returns>
		/// Tile index.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public TileIndex TileIndexFromFlatIndex(int index) {
			int rows = TileSystem.rows;
			int columns = TileSystem.columns;

			if ((int)index >= rows * columns)
				throw new ArgumentOutOfRangeException("index");

			TileIndex tileIndex;
			tileIndex.row = index / columns;
			tileIndex.column = index % columns;
			return tileIndex;
		}

		/// <summary>
		/// Get list of grouped tile indices.
		/// </summary>
		/// <param name="index">Index of a grouped tile.</param>
		/// <returns>
		/// List of tile indices when specified index is part of a group; otherwise
		/// a value of <c>null</c>.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
        public CompositeTileGroup GetGroupedTileIndices(TileIndex index)
        {
            CompositeTileGroup group;
			_groups.TryGetValue(FlatIndexFromTileIndex(index), out group);
			return group;
		}

		/// <summary>
		/// Group specified tiles.
		/// </summary>
		/// <remarks>
		/// <para>The way in which input tiles are grouped varies depending upon
		/// whether they are already grouped or not:</para>
		/// <list type="bullet">
		/// <item>New group is created if neither input is already grouped.</item>
		/// <item>Ungrouped input is added to group of other input.</item>
		/// <item>Groups of inputs are merged.</item>
		/// </list>
		/// </remarks>
		/// <param name="index1">Index of first tile to group.</param>
		/// <param name="index2">Index of second tile to group.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when one or both of the specified indices are outside bounds
		/// of tile system.
		/// </exception>
		public void GroupTiles(TileIndex index1, TileIndex index2,String CompName) {
			int flatIndex1 = FlatIndexFromTileIndex(index1);
			int flatIndex2 = FlatIndexFromTileIndex(index2);

            CompositeTileGroup group1, group2;
			_groups.TryGetValue(flatIndex1, out group1);
			_groups.TryGetValue(flatIndex2, out group2);

			// Swap groups so that we are always adding to group1.
			if (group1 == null) {
				group1 = group2;
				group2 = null;
			}
			else if (group1 == group2) {
				// These tiles are already grouped!
				return;
			}

			if (flatIndex1 == flatIndex2) {
				// A group of one tile!
                group1 = new CompositeTileGroup(CompName);
				group1.Add(flatIndex1);
				_groups[flatIndex1] = group1;
			}
			else if (group1 == null) {
				// Neither of these tiles are already grouped.
                group1 = new CompositeTileGroup(CompName);
				group1.Add(flatIndex1);
				group1.Add(flatIndex2);
				_groups[flatIndex1] = group1;
				_groups[flatIndex2] = group1;
			}
			else if (group2 == null) {
				// Just need to add second index to first group.
				group1.Add(flatIndex2);
				_groups[flatIndex2] = group1;
			}
			else {
				// We need to merge two separate groups!
				for (int i = 0; i < group2.Count; ++i) {
					flatIndex2 = group2[i];
					group1.Add(flatIndex2);
					_groups[flatIndex2] = group1;
				}
			}
		}

		/// <summary>
		/// Ungroup all tiles from group of input tile. Nothing happens if input
		/// tile is not actually grouped.
		/// </summary>
		/// <param name="index">Index of tile.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public void UngroupTiles(TileIndex index) {
            CompositeTileGroup group = GetGroupedTileIndices(index);
			if (group == null)
				return;

			int count = group.Count;
			while (--count >= 0) {
				int clearTileIndex = group[count];
				_groups.Remove(clearTileIndex);
			}
		}

		/// <summary>
		/// Place input tile within its very own group. Nothing happens if input
		/// tile is already grouped.
		/// </summary>
		/// <param name="index">Index of tile.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public void GroupIndividualTile(TileIndex index) {
			//GroupTiles(index, index);
		}

		/// <summary>
		/// Remove individual tile from its group. Nothing happens if input tile
		/// is not actually grouped.
		/// </summary>
		/// <param name="index">Index of tile.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public void UngroupIndividualTile(TileIndex index) {
			int flatIndex = FlatIndexFromTileIndex(index);

            CompositeTileGroup group;
			_groups.TryGetValue(flatIndex, out group);
			if (group == null)
				return;

			group.Remove(flatIndex);
			if (group.Count == 1)
				_groups[flatIndex] = null;
		}
		
		/// <summary>
		/// Determine whether tile is part of a group.
		/// </summary>
		/// <param name="index">Index of tile.</param>
		/// <returns>
		/// A value of <c>true</c> if input tile is grouped with zero or more
		/// other tiles; otherwise a value of <c>false</c>.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Thrown when specified index is outside bounds of tile system.
		/// </exception>
		public bool IsGrouped(TileIndex index) {
			return GetGroupedTileIndices(index) != null;
		}

		/// <summary>
		/// Ungroup all tiles.
		/// </summary>
		public void UngroupAll() {
			_groups.Clear();
		}

		#endregion

	}


    public class CompositeTileGroup : IEnumerator<int>
   {
       public List<int> Data = new List<int>();
       private int curIndex;
       private int curInt;
       private String _CompositionName;//name of composite tile map used to create this group(room,etc)


       public String CompositionName { get { return _CompositionName; } set { _CompositionName = value; } }

       public CompositeTileGroup(String CompName)
       {
             curIndex=-1;
             CompositionName = CompName;
       }

       public IEnumerator<int> GetEnumerator()
       {
           return Data.GetEnumerator();
       }

       public void Add(int someint)
       {
           Data.Add(someint);
       }

       public void Remove(int someint)
       {
           Data.Remove(someint);
       }


       public int Count { get { return Data.Count; } }

       public int this[int i]
       {
           get
           {
               return Data[i];
           }
           set
           {
               Data[i] = value;
           } 
       }
         
 
       public bool MoveNext()
       {
           //Avoids going beyond the end of the collection. 
           if (++curIndex >= Data.Count)
           {
               return false;
           }
           else
           {
               // Set current box to next item in collection.
               curInt = Data[curIndex];
           }
           return true;
       }

       public void Reset()
       {
           curIndex = -1;
       }

       public int Current { get { return curInt; } }


       object IEnumerator.Current
       {
           get { return Current; }
       }

       public void Dispose()
       {
           throw new NotImplementedException();
       }

   }





}
