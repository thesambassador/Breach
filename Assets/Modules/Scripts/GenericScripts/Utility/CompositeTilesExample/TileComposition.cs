// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;

using Rotorz.Tile;

namespace Custom {

	/// <summary>
	/// An object which defines a composition of tiles allowing them to be
	/// painted as a group.
	/// </summary>
	[System.Serializable]
	public sealed class TileComposition {

		/// <summary>
		/// Initialize new <see cref="TileComposition"/> instance.
		/// </summary>
		/// <param name="name">Name of definition.</param>
		/// <param name="rows">Count of rows in tile composition.</param>
		/// <param name="columns">Count of columns in tile composition.</param>
		/// <seealso cref="Resize(int, int)"/>
		public TileComposition(string name, int rows, int columns) {
			_name = name;
			Resize(rows, columns);
		}

		/// <summary>
		/// Gets or sets name of composition.
		/// </summary>
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		/// <summary>
		/// Gets count of rows in composition.
		/// </summary>
		public int Rows {
			get { return _rows; }
		}
		/// <summary>
		/// Gets count of columns in composition.
		/// </summary>
		public int Columns {
			get { return _columns; }
		}

		[SerializeField]
		private string _name;
		[SerializeField]
		private int _rows;
		[SerializeField]
		private int _columns;
		[SerializeField]
		private TileData[] _map;

		/// <summary>
		/// Gets <see cref="TileData"/> which represents tile at specific location
		/// within tile composition map.
		/// </summary>
		/// <param name="row">Zero-based index of row.</param>
		/// <param name="column">Zero-based index of column.</param>
		/// <returns>
		/// The <see cref="TileData"/> instance or a value of <c>null</c> indicating
		/// that cell should be empty. A value of <c>null</c> is also returned when
		/// associated brush reference is for some reason missing (i.e. has been deleted).
		/// </returns>
		public TileData this[int row, int column] {
			get {
				TileData tile = _map[row * _columns + column];
				return (tile != null && !tile.Empty && tile.brush != null)
					? tile
					: null;
			}
			set { _map[row * _columns + column] = value; }
		}

		/// <summary>
		/// Resize and clear tile composition.
		/// </summary>
		/// <param name="rows">Count of rows in tile composition.</param>
		/// <param name="columns">Count of columns in tile composition.</param>
		public void Resize(int rows, int columns) {
			if (rows == _rows && columns == _columns) {
				// Length of array has not actually changed, just clear!
				Clear();
			}
			else {
				_rows = rows;
				_columns = columns;
				_map = new TileData[rows * columns];
			}
		}

		/// <summary>
		/// Clear tile composition.
		/// </summary>
		public void Clear() {
			for (int i = 0; i < _map.Length; ++i)
				_map[i] = null;
		}

	}

}
