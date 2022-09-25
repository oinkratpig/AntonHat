using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace AntonHat
{
	public static class CustomTextures
	{
		public const int PIXELS_PER_UNIT = 32;

		public static BepInEx.Logging.ManualLogSource Log;

		public static Dictionary<string, Texture2D> _textureCache = new();
		public static Dictionary<string, Sprite> _spriteCache = new();

		/* Format a path to also include the plugins folder */
		private static void FormatPath(ref string filePath)
		{
			filePath = $"BepInEx/plugins/{filePath}";

		} // end FormatPath

		/* Create and cache a new Texture2D */
		public static void CreateTexture2D(string textureName, string filePath)
		{
			FormatPath(ref filePath);

			// Key already exists
			if (_textureCache.ContainsKey(textureName))
			{
				Log.LogError($"Attempted to create cached Texture2D \"{textureName}\" more than once!");
				return;
			}

			// File does not exist
			else if (!File.Exists(filePath))
			{
				Log.LogError($"File \"{filePath}\" does not exist!");
				return;
			}

			// Create Texture2D
			Texture2D texture2D = new(0, 0);
			if (!ImageConversion.LoadImage(texture2D, File.ReadAllBytes(filePath)))
			{
				Log.LogError($"Couldn't convert image \"{filePath}\"!");
				return;
			}

			texture2D.hideFlags = HideFlags.HideAndDontSave;
			texture2D.filterMode = FilterMode.Point;

			UnityEngine.Object.DontDestroyOnLoad(texture2D);
			_textureCache.Add(textureName, texture2D);

		} // end CreateTexture2D

		/* Create and cache a new Sprite */
		public static void CreateSprite(string spriteName, string filePath, Vector2 pivot)
		{
			CreateTexture2D(spriteName, filePath);
			FormatPath(ref filePath);

			// Key already exists
			if (_spriteCache.ContainsKey(spriteName))
			{
				Log.LogError($"Attempted to create cached Sprite \"{spriteName}\" more than once!");
				return;
			}

			// File does not exist
			else if (!File.Exists(filePath))
			{
				Log.LogError($"File \"{filePath}\" does not exist!");
				return;
			}

			// Get Texture2D
			if (!_textureCache.TryGetValue(spriteName, out Texture2D texture2D))
			{
				Log.LogError($"Texture2D generated before Sprite \"{spriteName}\" does not exist.");
				return;
			}

			// Create and cache sprite
			Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), pivot, PIXELS_PER_UNIT);
			_spriteCache.Add(spriteName, sprite);

		} // end CreateSprite

		public static void CreateSprite(string spriteName, string filePath)
		{
			CreateSprite(spriteName, filePath, Vector2.zero);

		} // end CreateSprite

		/* Sets the given Sprite to a cached Sprite */
		public static void SetSprite(ref Sprite sprite, string spriteName)
		{
			if (_spriteCache.TryGetValue(spriteName, out Sprite spr))
			{
				sprite = spr;
			}
			else Log.LogError($"Sprite key \"{spriteName}\" not in cache!");

		} // end SetSprite

	} // end class CustomTextures

} // end namespace AntonHat
