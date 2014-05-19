// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;

using System.Collections.Generic;

namespace Custom {

	/// <summary>
	/// Represents an asset which manages a collection of <see cref="TileComposition"/>
	/// definitions which can be used to paint groups of tiles.
	/// </summary>
	//public class TileCompositionManager : ScriptableObject {
        public class TileCompositionManager : ScriptableObject {

		[SerializeField, HideInInspector]
		private List<TileComposition> _compositions = new List<TileComposition>();

		/// <summary>
		/// Gets the collection of tile compositions.
		/// </summary>
		public IList<TileComposition> Compositions {
			get { return _compositions; }
		}

	}

}
