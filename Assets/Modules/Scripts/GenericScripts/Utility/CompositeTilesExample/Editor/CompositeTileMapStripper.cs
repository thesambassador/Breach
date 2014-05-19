// Copyright (c) 2014 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;
using UnityEditor;

using Rotorz.Tile.Editor;

namespace Custom {

	/// <summary>
	/// Integrates custom post-build step into tile system build process so
	/// that <see cref="CompositeTileMap"/> components can be automatically
	/// removed when <see cref="CompositeTileMap.StripOnBuild"/> is set.
	/// </summary>
	[InitializeOnLoad]
	static class CompositeTileMapStripper {

		static CompositeTileMapStripper() {
			BuildUtility.FinalizeTileSystem += OnFinalizeTileSystem;
		}

		private static void OnFinalizeTileSystem(IBuildContext context) {
			var compositeMap = context.TileSystem.GetComponent<CompositeTileMap>();
			if (compositeMap != null && compositeMap.StripOnBuild)
				Object.DestroyImmediate(compositeMap);
		}

	}

}
